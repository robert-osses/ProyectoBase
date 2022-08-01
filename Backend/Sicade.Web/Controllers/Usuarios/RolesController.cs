using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sicade.Web.Helpers;
using Sicade.Datos;
using Sicade.Entidades.Auth;

namespace Instruccion.Web.Controllers
{
    [ApiController, Authorize(Roles = "141"), Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly DbContextAuth _authContext;
        private readonly IConfiguration _config;

        public RolesController(
            DbContextAuth authContext,
            IConfiguration config
            )
        {
            _authContext = authContext;
            _config = config;
        }

        [HttpGet("getRoles")]
        public async Task<ActionResult<List<rol>>> getRoles()
        {
            var roles = _authContext.Rol
                .Where(r => r.SISTEMAID == 10)//sistemaid == 10 es el Sicade
                .OrderBy(r => r.DESCRIPCION)
                .AsQueryable();

            Auditor.registroAuditor(
                "get", 200, User.GetClaimValue("IP"), int.Parse(User.GetClaimValue("Run")), int.Parse(User.GetClaimValue("Rol")),
                "Roles/getRoles", "Ok", "", _authContext
            );

            return await roles.ToListAsync();
        }
    }
}
