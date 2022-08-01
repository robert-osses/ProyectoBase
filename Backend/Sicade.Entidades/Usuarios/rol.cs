using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicade.Entidades.Auth
{
    public class rol
    {
        public int ID { get; set; }
        public int SISTEMAID { get; set; }
        public int NIVELID { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int PROPIETARIO { get; set; }
        public DateTime CREACION { get; set; }
        public int USUARIO { get; set; }
        public DateTime ACTUALIZACION { get; set; }
        public int ESTADO { get; set; }

        [ForeignKey("NIVELID")]
        public nivel NIVEL { get; set; }
    }
}
