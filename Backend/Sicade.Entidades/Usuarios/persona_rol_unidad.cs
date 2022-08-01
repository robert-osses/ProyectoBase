using Sicade.Entidades.ETL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicade.Entidades.Auth
{
    public class persona_rol_unidad
    {
        public int ID { get; set; }
        public int SISTEMAID { get; set; }
        public int RUN { get; set; }
        public int ROLID { get; set; }
        public string UNIDADCODIGO { get; set; }
        public int PROPIETARIO { get; set; }
        public DateTime CREACION { get; set; }
        public int USUARIO { get; set; }
        public DateTime ACTUALIZACION { get; set; }
        public int ESTADO { get; set; }

        [ForeignKey("ROLID")]
        public rol ROL { get; set; }

        [ForeignKey("RUN")]//vista en usuario USUARIOS de personal
        public virtual personaETL PERSONA { get; set; }

        [ForeignKey("UNIDADCODIGO")]//vista en usuario USUARIOS de unidades
        public virtual Unidad UNIDAD { get; set; }
    }
}
