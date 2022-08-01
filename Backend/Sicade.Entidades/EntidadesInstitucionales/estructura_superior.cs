using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sicade.Entidades.ETL
{
    [Table("ESTRUCTURA_SUPERIOR")]//es necesario definir la vista como tabla para poder generar relaciones con EF
    public class estructura_superior
    {
        [Key]//es necesario determinar la "llave primaria" de la vista
        public int ID { get; set; }
        public string? DESCRIPCION { get; set; }
        public string? SIGLA { get; set; }
        public int FUERZATOTAL { get; set; }
        public int FUERZAMILITAR { get; set; }
    }
}
