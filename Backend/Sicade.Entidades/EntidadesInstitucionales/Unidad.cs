using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sicade.Entidades.ETL
{
    [Table("UNIDAD")]//es necesario definir la vista como tabla para poder generar relaciones con EF
    public class Unidad
    {
        public int ID { get; set; }
        [Key]//es necesario determinar la "llave primaria" de la vista
        public string CODIGO { get; set; }
        public string? DESCRIPCION { get; set; }
        public string? SIGLA { get; set; }
        public string? JERARQUIA { get; set; }
        public string? LONGITUD { get; set; }
        public string? LATITUD { get; set; }
        public int ESTRUCTURAID { get; set; }
        public int FUERZATOTAL { get; set; }
        public int FUERZAMILITAR { get; set; }

        [ForeignKey("ESTRUCTURAID")]
        public virtual estructura_superior ESTRUCTURA_SUPERIOR { get; set; }
    }
}
