using Sicade.Datos;
using Sicade.Entidades.Auth;

namespace Sicade.Web.Helpers
{
    public static class Auditor
    {
        public static void registroAuditor(string accion, int status, string ip, int usuario, int rolid, string EndPoint, string mensaje, string parametros, DbContextAuth _cn)
        {
            auditor newAuditor = new auditor
            {
                SISTEMAID = 10,
                ACCION = accion,
                STATUSCODE = status,
                ENDPOINT = EndPoint,
                MESSAGE = mensaje,
                PARAMETROS = parametros,
                IP = ip,
                USUARIO = usuario,
                ROLID = rolid,
                CREACION = DateTime.Now
            };

            _cn.Auditor.Add(newAuditor);
            _cn.SaveChanges();
        }
    }    
}