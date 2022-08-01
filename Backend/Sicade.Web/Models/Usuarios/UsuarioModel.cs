using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sicade.Web.Models.Auth
{
    public class UsuarioModel
    {
        public int RUN { get; set; }
        public int ROLID { get; set; }
        public string UNIDADCODIGO { get; set; }
        public int ESTADO { get; set; }
    }
}
