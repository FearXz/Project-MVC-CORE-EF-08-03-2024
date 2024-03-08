using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_MVC_CORE_EF.Data;
using Project_MVC_CORE_EF.Models;

namespace Project_MVC_CORE_EF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult RichiesteAsincrone()
        {
            return View();
        }

        [Authorize]
        public IActionResult GestisciCategorie()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> FetchByCodiceFiscale(string? codiceFiscale)
        {
            try
            {
                var listaPrenotazioni = await _db
                    .Prenotazioni.Include(c => c.Cliente)
                    .Where(c => c.Cliente.CodFiscale == codiceFiscale)
                    .Select(p => new
                    {
                        p.IdPrenotazione,
                        p.DataInizioPrenotazione,
                        p.DataFinePrenotazione,
                        p.Pensione.TipoPensione,
                        Cliente = new
                        {
                            p.Cliente.NomeCliente,
                            p.Cliente.CognomeCliente,
                            p.Cliente.CodFiscale,
                            p.Cliente.Email,
                            p.Cliente.Cellulare
                        },
                        Servizi = p.Servizi.Select(s => new
                        {
                            s.IdServizio,
                            s.CostoServizio,
                            s.TipoServizio.NomeTipoServizio,
                            s.TipoServizio.IdTipoServizio
                        })
                    })
                    .ToListAsync();
                return Json(listaPrenotazioni);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle spedizioni di oggi");
                return StatusCode(500, new { message = "Errore interno del server" });
            }
        }

        [Authorize]
        public async Task<IActionResult> FetchTotalePensioniComplete()
        {
            try
            {
                var totalePensioniComplete = await _db
                    .Prenotazioni.Where(p => p.Pensione.TipoPensione == "Pensione Completa")
                    .CountAsync();
                return Json(totalePensioniComplete);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle pensioni complete");
                return StatusCode(500, new { message = "Errore interno del server" });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
