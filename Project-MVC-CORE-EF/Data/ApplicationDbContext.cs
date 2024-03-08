using Microsoft.EntityFrameworkCore;
using Project_MVC_CORE_EF.Models;

namespace Project_MVC_CORE_EF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ADMIN> Admins { get; set; }
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Camera> Camere { get; set; }
        public DbSet<Pensione> Pensioni { get; set; }
        public DbSet<Prenotazione> Prenotazioni { get; set; }
        public DbSet<Servizio> Servizi { get; set; }
        public DbSet<TipoCamera> TipiCamere { get; set; }
        public DbSet<TipoServizio> TipiServizi { get; set; }
    }
}
