using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sicade.Web.Helpers;
using Sicade.Datos;
using Sicade.Entidades.ETL;

namespace Instruccion.Web.Controllers
{
    [ApiController, Authorize(Roles = "141"), Route("api/[controller]")]
    public class EstructurasSuperioresController : ControllerBase
    {
        private readonly DbContextAuth _authContext;
        private readonly IConfiguration _config;

        public EstructurasSuperioresController(
            DbContextAuth authContext,
            IConfiguration config
            )
        {
            _authContext = authContext;
            _config = config;
        }

        [HttpGet("getEstructurasSuperiores")]
        public async Task<ActionResult<List<estructura_superior>>> getEstructurasSuperiores()
        {
            var queryable = _authContext.EstructuraSuperior
                .OrderBy(a => a.ID)
                .AsQueryable();

            Auditor.registroAuditor(
                "get", 200, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                "EstructurasSuperiores/getEstructurasSuperiores", "Ok", "", _authContext
            );

            return await queryable.ToListAsync();
        }

        [HttpGet("getEstructuraSuperior")]
        public async Task<ActionResult<List<estructura_superior>>> getEstructuraSuperior(int id)
        {
            var queryable = _authContext.EstructuraSuperior
                .Where(es => es.ID == id)
                .OrderBy(a => a.ID)
                .AsQueryable();

            Auditor.registroAuditor(
                "get", 200, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                "EstructurasSuperiores/getEstructuraSuperior", "Ok", JsonConvert.SerializeObject("id: " + id), _authContext
            );

            return await queryable.ToListAsync();
        }
    }
}
