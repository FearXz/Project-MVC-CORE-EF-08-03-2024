using System.ComponentModel.DataAnnotations;

namespace Project_MVC_CORE_EF.Models
{
    public class TipoServizio
    {
        [Key]
        public int IdTipoServizio { get; set; }
        public string NomeTipoServizio { get; set; }

        public virtual ICollection<Servizio> Servizi { get; set; }
    }
}
