using System.ComponentModel.DataAnnotations;

namespace Project_MVC_CORE_EF.Models
{
    public class ADMIN
    {
        [Key]
        public int IdAdmin { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
