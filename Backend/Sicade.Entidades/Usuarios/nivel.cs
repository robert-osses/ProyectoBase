using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicade.Entidades.Auth
{
    public class nivel
    {
        public int ID { get; set; }
        public string NOMBRE { get; set; }
        public int PROPIETARIO { get; set; }
        public DateTime CREACION { get; set; }
        public int USUARIO { get; set; }
        public DateTime ACTUALIZACION { get; set; }
        public int ESTADO { get; set; }     
    }
}
