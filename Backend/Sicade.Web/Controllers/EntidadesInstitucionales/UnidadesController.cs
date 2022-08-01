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
    public class UnidadesController : ControllerBase
    {
        private readonly DbContextAuth _authContext;
        private readonly IConfiguration _config;

        public UnidadesController(
            DbContextAuth authContext,
            IConfiguration config
            )
        {
            _authContext = authContext;
            _config = config;
        }

        [HttpGet("getUnidades")]
        public async Task<ActionResult<List<Unidad>>> getUnidades()
        {
            var queryable = _authContext.Unidad
                .Include(e => e.ESTRUCTURA_SUPERIOR)
                .OrderBy(a => a.JERARQUIA)
                .AsQueryable();

            Auditor.registroAuditor(
                "get", 200, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                "Unidades/getUnidades", "Ok", "", _authContext
            );

            return await queryable.ToListAsync();
        }

        [HttpGet("getUnidadesId")]
        public async Task<ActionResult<List<Unidad>>> getUnidadesId([FromQuery] string id)
        {
            var queryable = _authContext.Unidad
                .Include(e => e.ESTRUCTURA_SUPERIOR)
                .Where(u => u.CODIGO == id)
                .OrderBy(a => a.JERARQUIA)
                .AsQueryable();

            Auditor.registroAuditor(
                "get", 200, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                "Unidades/getUnidadesId", "Ok", JsonConvert.SerializeObject("id: " + id), _authContext
            );

            return await queryable.ToListAsync();
        }

        [HttpGet("getUnidadesESE")]
        public async Task<ActionResult<List<Unidad>>> getUnidadesESE(int id)
        {
            var queryable = _authContext.Unidad
                .Include(e => e.ESTRUCTURA_SUPERIOR)
                .Where(a => a.ESTRUCTURAID == id)
                .OrderBy(a => a.JERARQUIA)
                .AsQueryable();

            Auditor.registroAuditor(
                "get", 200, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                "Unidades/getUnidadesESE", "Ok", JsonConvert.SerializeObject("id: " + id), _authContext
            );

            return await queryable.ToListAsync();
        }
    }
}
