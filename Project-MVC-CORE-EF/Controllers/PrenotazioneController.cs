using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MVC_CORE_EF.Data;
using Project_MVC_CORE_EF.Models;

namespace Project_MVC_CORE_EF.Controllers
{
    [Authorize]
    public class PrenotazioneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrenotazioneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prenotazione
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context
                .Prenotazioni.Include(p => p.Camera)
                .Include(p => p.Cliente)
                .Include(p => p.Pensione);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Prenotazione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context
                .Prenotazioni.Include(p => p.Camera)
                .Include(p => p.Cliente)
                .Include(p => p.Pensione)
                .Include(p => p.Servizi)
                .ThenInclude(s => s.TipoServizio)
                .FirstOrDefaultAsync(m => m.IdPrenotazione == id);

            // voglio calcolare il totale della prenotazione
            double totale = 0;
            foreach (var servizio in prenotazione.Servizi)
            {
                totale += servizio.CostoServizio;
            }
            // aggiungo il costo della camera e della pensione per il numero di giorni
            totale +=
                prenotazione.Camera.CostoCamera
                * (prenotazione.DataFinePrenotazione - prenotazione.DataInizioPrenotazione).Days;
            totale +=
                prenotazione.Pensione.CostoPensione
                * (prenotazione.DataFinePrenotazione - prenotazione.DataInizioPrenotazione).Days;

            ViewData["Totale"] = totale;
            ViewData["TotaleDaSaldare"] = totale - prenotazione.AccontoPrenotazione;

            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        // GET: Prenotazione/Create
        public IActionResult Create()
        {
            ViewData["IdCamera"] = new SelectList(_context.Camere, "IdCamera", "NumeroCamera");
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "NomeCompleto");
            ViewData["IdPensione"] = new SelectList(
                _context.Pensioni,
                "IdPensione",
                "TipoPensione"
            );
            return View();
        }

        // POST: Prenotazione/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "IdCliente,IdCamera,IdPensione,DataInizioPrenotazione,DataFinePrenotazione,AccontoPrenotazione"
            )]
                Prenotazione prenotazione
        )
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Camera");
            ModelState.Remove("Pensione");
            ModelState.Remove("Servizi");

            if (ModelState.IsValid)
            {
                _context.Add(prenotazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCamera"] = new SelectList(
                _context.Camere,
                "IdCamera",
                "IdCamera",
                prenotazione.IdCamera
            );
            ViewData["IdCliente"] = new SelectList(
                _context.Clienti,
                "IdCliente",
                "Cellulare",
                prenotazione.IdCliente
            );
            ViewData["IdPensione"] = new SelectList(
                _context.Pensioni,
                "IdPensione",
                "TipoPensione",
                prenotazione.IdPensione
            );
            return View(prenotazione);
        }

        // GET: Prenotazione/CreateService/5
        public IActionResult CreateService(int? id)
        {
            ViewData["IdPrenotazione"] = id;
            ViewData["IdTipoServizio"] = new SelectList(
                _context.TipiServizi,
                "IdTipoServizio",
                "NomeTipoServizio"
            );
            return View();
        }

        // POST: Prenotazione/CreateService
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService(
            int id,
            [Bind("IdPrenotazione,IdTipoServizio,CostoServizio")] Servizio servizio
        )
        {
            ModelState.Remove("Prenotazione");
            ModelState.Remove("TipoServizio");

            if (ModelState.IsValid)
            {
                _context.Add(servizio);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = id });
            }
            ViewData["IdPrenotazione"] = id;
            ViewData["IdTipoServizio"] = new SelectList(
                _context.TipiServizi,
                "IdTipoServizio",
                "NomeTipoServizio"
            );
            return View(servizio);
        }

        // GET: Prenotazione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione == null)
            {
                return NotFound();
            }
            ViewData["IdCamera"] = new SelectList(
                _context.Camere,
                "IdCamera",
                "IdCamera",
                prenotazione.IdCamera
            );
            ViewData["IdCliente"] = new SelectList(
                _context.Clienti,
                "IdCliente",
                "NomeCompleto",
                prenotazione.IdCliente
            );
            ViewData["IdPensione"] = new SelectList(
                _context.Pensioni,
                "IdPensione",
                "TipoPensione",
                prenotazione.IdPensione
            );
            return View(prenotazione);
        }

        // POST: Prenotazione/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind(
                "IdPrenotazione,IdCliente,IdCamera,IdPensione,DataInizioPrenotazione,DataFinePrenotazione,AccontoPrenotazione"
            )]
                Prenotazione prenotazione
        )
        {
            if (id != prenotazione.IdPrenotazione)
            {
                return NotFound();
            }
            ModelState.Remove("Cliente");
            ModelState.Remove("Camera");
            ModelState.Remove("Pensione");
            ModelState.Remove("Servizi");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prenotazione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrenotazioneExists(prenotazione.IdPrenotazione))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCamera"] = new SelectList(
                _context.Camere,
                "IdCamera",
                "IdCamera",
                prenotazione.IdCamera
            );
            ViewData["IdCliente"] = new SelectList(
                _context.Clienti,
                "IdCliente",
                "Cellulare",
                prenotazione.IdCliente
            );
            ViewData["IdPensione"] = new SelectList(
                _context.Pensioni,
                "IdPensione",
                "TipoPensione",
                prenotazione.IdPensione
            );
            return View(prenotazione);
        }

        // GET: Prenotazione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context
                .Prenotazioni.Include(p => p.Camera)
                .Include(p => p.Cliente)
                .Include(p => p.Pensione)
                .FirstOrDefaultAsync(m => m.IdPrenotazione == id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        // POST: Prenotazione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione != null)
            {
                _context.Prenotazioni.Remove(prenotazione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrenotazioneExists(int id)
        {
            return _context.Prenotazioni.Any(e => e.IdPrenotazione == id);
        }
    }
}
