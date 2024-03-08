using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_MVC_CORE_EF.Models
{
    public class Cliente
    {
        [Key]
        [Display(Name = "Nominativo Cliente")]
        public int IdCliente { get; set; }

        [Required]
        public string NomeCliente { get; set; }

        [Required]
        public string CognomeCliente { get; set; }

        [NotMapped]
        public string NomeCompleto => $"{NomeCliente} {CognomeCliente}";

        [Required]
        public string CodFiscale { get; set; }

        [Required]
        public string Citta { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Cellulare { get; set; }

        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
