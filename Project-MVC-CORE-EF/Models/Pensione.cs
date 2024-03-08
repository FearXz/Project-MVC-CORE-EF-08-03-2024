using System.ComponentModel.DataAnnotations;

namespace Project_MVC_CORE_EF.Models
{
    public class Pensione
    {
        [Key]
        public int IdPensione { get; set; }

        [Required]
        public string TipoPensione { get; set; }

        [Required]
        public double CostoPensione { get; set; }

        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
