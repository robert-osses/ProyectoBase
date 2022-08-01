using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sicade.Web.Helpers;
using Sicade.Datos;
using Sicade.Entidades.ETL;

namespace Sicade.Web.Controllers
{
    [ApiController, Authorize(Roles = "141"), Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly DbContextAuth _authContext;
        private readonly IConfiguration _config;

        public PersonasController(
            DbContextAuth authContext,
            IConfiguration config
            )
        {
            _authContext = authContext;
            _config = config;
        }

        [HttpGet("getPersonasUnidad")]
        public async Task<ActionResult<List<personaETL>>> getPersonasUnidad([FromQuery] string codigo)
        {
            var queryable = _authContext.Persona
                .Where(p => p.UNIDADCODIGO == codigo
                    && (p.COD_CATEGORIA == 5 || p.COD_CATEGORIA == 10 || p.COD_CATEGORIA == 13 || p.COD_CATEGORIA == 60))//categorías de personal militar
                .AsQueryable();

            Auditor.registroAuditor(
                "get", 200, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                "Personas/getPersonasUnidad", "Ok", JsonConvert.SerializeObject("codigo: " + codigo), _authContext
            );

            return await queryable.ToListAsync();
        }

        [HttpGet("getPersonasRun")]
        public async Task<ActionResult<personaETL>> getPersonasRun([FromQuery] int run)
        {
            var queryable = _authContext.Persona
                .Where(p => p.RUN == run)
                .FirstOrDefaultAsync();

            Auditor.registroAuditor(
                "get", 200, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                "Personas/getPersonasRun", "Ok", JsonConvert.SerializeObject("run: " + run), _authContext
            );

            return await queryable;
        }
    }
}
