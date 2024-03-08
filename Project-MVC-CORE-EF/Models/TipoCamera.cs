using System.ComponentModel.DataAnnotations;

namespace Project_MVC_CORE_EF.Models
{
    public class TipoCamera
    {
        [Key]
        public int IdTipoCamera { get; set; }

        [Required]
        public string NomeTipoCamera { get; set; }

        public virtual ICollection<Camera> Camere { get; set; }
    }
}
