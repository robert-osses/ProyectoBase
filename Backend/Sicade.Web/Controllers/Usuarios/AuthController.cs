using Microsoft.AspNetCore.Authorization;
using Sicade.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using Sicade.Web.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Sicade.Web.Helpers;

namespace Sicade.Web.Controllers.Auth
{
    [ApiController, AllowAnonymous, Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // GET: AuthController
        private readonly DbContextAuth _authContext;
        private readonly IConfiguration _config;

        public AuthController(DbContextAuth authContext, IConfiguration config)
        {
            _authContext = authContext;
            _config = config;
        }

        [HttpPost("[action]")]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                RestClient client = new RestClient(_config.GetSection("API:Ruta").Value);
                RestRequest request = new RestRequest("ValidaIngresoUsuario?", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddQueryParameter("ENTIDAD", "DIMACOE");
                request.AddQueryParameter("SISTEMA", "Sicade");
                request.AddQueryParameter("USUARIO", model.RUT);
                request.AddQueryParameter("RUT", model.RUT);
                request.AddQueryParameter("PASS", model.PASSWORD);

                RestResponse response = client.Execute(request);
                JsonStatusModel mensaje = new JsonStatusModel();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Respuesta usu = JsonConvert.DeserializeObject<Respuesta>(response.Content);

                    if (usu.data.Control == "FALLOCLAVE")
                    {
                        Auditor.registroAuditor("Error", 404, IpAddress(), int.Parse(model.RUT), 0, "Auth/Login", usu.data.Control, JsonConvert.SerializeObject("RUT: " + model.RUT + " PASSWORD: ***"), _authContext);

                        mensaje.statusCode = 401;
                        mensaje.Message = "El usuario o clave son incorrectos";
                        return Ok(mensaje);
                    }

                    if (usu.data.Control == "NOEXISTE")
                    {
                        Auditor.registroAuditor("Error", 404, IpAddress(), int.Parse(model.RUT), 0, "Auth/Login", usu.data.Control, JsonConvert.SerializeObject("RUT: " + model.RUT + " PASSWORD: ***"), _authContext);

                        mensaje.statusCode = 404;
                        mensaje.Message = "Error el usuario no existe";
                        return Ok(mensaje);
                    }

                    if (usu.data.Control == "OK")
                    {
                        //extraigo el rol y unidad del usuario
                        var personaRolUnidad = _authContext.Persona_rol_unidad
                        .Where(a => a.RUN == usu.data.Rut && a.SISTEMAID == 10 && a.ESTADO == 1)//Sistemaid 10 es el Sicade
                        .FirstOrDefault();

                        if(personaRolUnidad == null)
                        {
                            Auditor.registroAuditor("Login", 404, IpAddress(), (int)usu.data.Rut, 0, "Sicade/Auth/Login", "Error, no tiene cuenta en el Sicade", JsonConvert.SerializeObject("RUT: " + model.RUT + " PASSWORD: ***"), _authContext);

                            return NotFound();
                        }

                        var nivel = _authContext.Rol
                            .Where(a => a.ID == personaRolUnidad.ROLID && a.SISTEMAID == 10)//Sistemaid 10 es el Sicade
                            .FirstOrDefault();


                        //creación de arreglo con las claims
                        var claims = new List<Claim>
                            {
                                //deben ser string
                                new Claim(ClaimTypes.Role, personaRolUnidad.ROLID.ToString().Trim()),//este controla los accesos del backend "[Authorize(Roles = "1,2,3")]"
                                new Claim("Rol", personaRolUnidad.ROLID.ToString().Trim()),
                                new Claim("Run", usu.data.Rut.ToString().Trim()),
                                new Claim("Nombres", usu.data.Grado.Trim() + " " + usu.data.Nombre.Trim()  + " " +  usu.data.Apellido_Paterno.Trim()),
                                new Claim("Foto", usu.data.UrlImagen),
                                new Claim("Unidad", personaRolUnidad.UNIDADCODIGO.ToString()),
                                new Claim("NivelAcceso", nivel.NIVELID.ToString()),
                                new Claim("IP", IpAddress()),//IP del cliente conectado
                            };

                        var token = GenerarToken(claims);

                        Auditor.registroAuditor("Login", 200, IpAddress(), (int)usu.data.Rut, personaRolUnidad.ROLID, "Sicade/Auth/Login", "Ok", JsonConvert.SerializeObject("RUT: " + model.RUT + " PASSWORD: ***"), _authContext);

                        return Ok(new { token });

                    }
                    else
                    {
                        mensaje.statusCode = 500;
                        mensaje.Message = "Error inesperado al ingresar";

                        Auditor.registroAuditor("Error", 500, IpAddress(), int.Parse(model.RUT), 0, "Auth/Login", mensaje.Message, JsonConvert.SerializeObject(model), _authContext);

                        return Ok(mensaje);
                    }
                }
                else
                {
                    mensaje.statusCode = 501;
                    mensaje.Message = "Error de comunicación con API";

                    Auditor.registroAuditor("Error", 501, IpAddress(), int.Parse(model.RUT), 0, "Auth/Login", mensaje.Message, JsonConvert.SerializeObject(model), _authContext);

                    return Ok(mensaje);
                }
            }
            catch (WebException ex)
            {
                JsonStatusModel mensaje = new JsonStatusModel
                {
                    statusCode = 500,
                    Message = ex.Message,
                };
                return Ok(mensaje);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private string GenerarToken(List<Claim> ArrClaims)
        {
            var token = new JwtSecurityToken
            (
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                claims: ArrClaims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value)),
                    SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(TokenExpViewModel tokenExp)
        {
            try
            {
                var Handler = new JwtSecurityTokenHandler();
                var cToken = Handler.ReadJwtToken(tokenExp.tokenExp);
                var claims = new List<Claim>(cToken.Claims);

                var token = GenerarToken(claims);

                return Ok(new { token });
            }
            catch
            {
                return BadRequest();
            }

        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
