using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sicade.Web.Helpers;
using Sicade.Datos;
using Sicade.Entidades.Auth;
using System.Net;
using Sicade.Web.Models.Auth;

namespace Sicade.Web.Controllers.Usuarios
{
    [ApiController, Authorize(Roles = "141"), Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DbContextAuth _authContext;
        private readonly IConfiguration _config;

        public UsuariosController(
            DbContextAuth authContext,
            IConfiguration config
            )
        {
            _authContext = authContext;
            _config = config;
        }

        [HttpGet("getUsuarios")]
        public async Task<ActionResult<List<persona_rol_unidad>>> getUsuarios()
        {

            var usuarios = _authContext.Persona_rol_unidad
                .Include(p => p.PERSONA)
                .Include(u => u.UNIDAD)
                .Include(r => r.ROL)
                .Where(pru => pru.SISTEMAID == 10)
                .AsQueryable();

            Auditor.registroAuditor(
                "get", 200, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                "Usuarios/getUsuarios", "Ok", "", _authContext
            );

            return await usuarios.ToListAsync();
        }

        [HttpPut("putUsuario")]
        public async Task<IActionResult> putUsuario([FromBody] UsuarioModel model)
        {
            JsonStatusModel mensaje = new JsonStatusModel();

            if (!ModelState.IsValid)
            {
                mensaje.statusCode = 500;
                mensaje.Message = "Estructura incorrecta";

                Auditor.registroAuditor(
                    "error", mensaje.statusCode, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                    "Usuarios/putUsuario", mensaje.Message, JsonConvert.SerializeObject(model), _authContext
                );

                return Ok(mensaje);
            }
            else
            {
                var usuario = await _authContext.Persona_rol_unidad
                    .Where(pru => pru.RUN == model.RUN && pru.SISTEMAID == 10)
                    .FirstOrDefaultAsync();

                if (usuario == null)
                {
                    mensaje.statusCode = 404;
                    mensaje.Message = "Usuario no existe";

                    Auditor.registroAuditor(
                        "error", mensaje.statusCode, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                        "Usuarios/putUsuario", mensaje.Message, JsonConvert.SerializeObject(model), _authContext
                    );

                    return Ok(mensaje);
                }

                if (model.UNIDADCODIGO != null)
                {
                    usuario.UNIDADCODIGO = model.UNIDADCODIGO;
                }

                if (model.ROLID != 0)
                {
                    usuario.ROLID = model.ROLID;
                }

                try
                {
                    usuario.ESTADO = model.ESTADO;
                    usuario.USUARIO = int.Parse(User.GetClaimValue("Run"));
                    usuario.ACTUALIZACION = DateTime.Now;
                    await _authContext.SaveChangesAsync();

                    mensaje.statusCode = 200;
                    mensaje.Message = "Información guardada correctamente";
                    mensaje.Data = usuario;

                    Auditor.registroAuditor(
                        "put", mensaje.statusCode, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                        "Usuarios/putUsuario", mensaje.Message, JsonConvert.SerializeObject(model), _authContext
                    );

                    return Ok(mensaje);
                }
                catch (WebException ex)
                {
                    mensaje.statusCode = 500;
                    mensaje.Message = "Error al guardar la información";

                    Auditor.registroAuditor(
                        "error", mensaje.statusCode, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                        "Usuarios/putUsuario", ex.Message, JsonConvert.SerializeObject(model), _authContext
                    );

                    return Ok(mensaje);
                }
            }
        }

        [HttpPost("postUsuario")]
        public async Task<IActionResult> postUsuario([FromBody] UsuarioModel model)
        {
            JsonStatusModel mensaje = new JsonStatusModel();

            if (!ModelState.IsValid)
            {
                mensaje.statusCode = 500;
                mensaje.Message = "Estructura incorrecta";

                Auditor.registroAuditor(
                    "error", mensaje.statusCode, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                    "Usuarios/postUsuario", mensaje.Message, JsonConvert.SerializeObject(model), _authContext
                );

                return Ok(mensaje);
            }
            else
            {
                var usuario = await _authContext.Persona_rol_unidad
                    .Where(pru => pru.RUN == model.RUN && pru.SISTEMAID == 10)
                    .FirstOrDefaultAsync();

                if (usuario != null)
                {
                    mensaje.statusCode = 409;
                    mensaje.Message = "El usuario ya existe, por favor revise el listado de usuarios";

                    Auditor.registroAuditor(
                        "error", mensaje.statusCode, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                        "Usuarios/postUsuario", mensaje.Message, JsonConvert.SerializeObject(model), _authContext
                    );

                    return Ok(mensaje);
                }

                var persona = await _authContext.Persona
                    .Where(p => p.RUN == model.RUN)
                    .FirstOrDefaultAsync();

                if (persona == null)
                {
                    mensaje.statusCode = 404;
                    mensaje.Message = "El usuario no existe en la base de datos de personal";
                    return Ok(mensaje);
                }

                if (model.UNIDADCODIGO == null)
                {
                    mensaje.statusCode = 500;
                    mensaje.Message = "Estructura incorrecta, debe seleccionar la UNIDAD del usuario";
                    return Ok(mensaje);
                }

                if (model.ROLID == 0)
                {
                    mensaje.statusCode = 500;
                    mensaje.Message = "Estructura incorrecta, debe seleccionar un ROL";
                    return Ok(mensaje);
                }

                try
                {
                    persona_rol_unidad PersonaRolUnidad = new persona_rol_unidad
                    {
                        RUN = model.RUN,
                        SISTEMAID = 10,//Sicade
                        UNIDADCODIGO = model.UNIDADCODIGO,
                        ROLID = model.ROLID,
                        ESTADO = model.ESTADO,
                        PROPIETARIO = int.Parse(User.GetClaimValue("Run")),
                        CREACION = DateTime.Now,
                        USUARIO = int.Parse(User.GetClaimValue("Run")),
                        ACTUALIZACION = DateTime.Now,
                    };

                    _authContext.Persona_rol_unidad.Add(PersonaRolUnidad);
                    await _authContext.SaveChangesAsync();

                    mensaje.statusCode = 200;
                    mensaje.Message = "Información guardada correctamente";
                    mensaje.Data = usuario;

                    Auditor.registroAuditor(
                        "post", mensaje.statusCode, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                        "Usuarios/postUsuario", "Ok", JsonConvert.SerializeObject(model), _authContext
                    );

                    return Ok(mensaje);
                }
                catch (WebException ex)
                {
                    mensaje.statusCode = 500;
                    mensaje.Message = "Error al guardar la información";

                    Auditor.registroAuditor(
                        "error", mensaje.statusCode, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                        "Usuarios/postUsuario", ex.Message, JsonConvert.SerializeObject(model), _authContext
                    );

                    return Ok(mensaje);
                }
            }
        }
    }
}
