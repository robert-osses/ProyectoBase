using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sicade.Web.Models.Auth
{
    public class UsuarioApiViewModel
    {
        public int? Rut { get; set; }
        public string Categoria { get; set; }
        public string Grado { get; set; }
        public string Apellido_Paterno { get; set; }
        public  string Apellido_Materno { get; set; }
        public string Nombres { get; set; }
        public string Nombre { get; set; }
        public string Unidad { get; set; }
        public string Telefono_Fijo { get; set; }
        public string Telefono_Movil { get; set; }
        public string Correo_Internet { get; set; }
        public string Ultimo_Ascenso { get; set; }
        public string Control { get; set; }
        public string UrlImagen { get; set; }
        public Boolean Existe { get; set; }
    }

    public class Respuesta
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public UsuarioApiViewModel data { get; set; }
    }

    public class RefreshToken
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }

}
    
