using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_MVC_CORE_EF.Models
{
    public class Servizio
    {
        [Key]
        public int IdServizio { get; set; }

        [Required]
        [ForeignKey("Prenotazione")]
        public int IdPrenotazione { get; set; }

        [Required]
        [ForeignKey("TipoServizio")]
        public int IdTipoServizio { get; set; }

        [Required]
        public double CostoServizio { get; set; }

        public virtual Prenotazione Prenotazione { get; set; }
        public virtual TipoServizio TipoServizio { get; set; }
    }
}
