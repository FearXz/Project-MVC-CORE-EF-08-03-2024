using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_MVC_CORE_EF.Models
{
    public class Camera
    {
        [Key]
        public int IdCamera { get; set; }

        [Required]
        [ForeignKey("TipoCamera")]
        public int IdTipoCamera { get; set; }

        [Required]
        public int NumeroCamera { get; set; }

        [Required]
        public double CostoCamera { get; set; }

        [Required]
        public bool CameraDisponibile { get; set; }

        public virtual TipoCamera TipoCamera { get; set; }
        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
