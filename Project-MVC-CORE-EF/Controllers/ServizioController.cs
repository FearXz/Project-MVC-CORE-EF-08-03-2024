using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MVC_CORE_EF.Data;
using Project_MVC_CORE_EF.Models;

namespace Project_MVC_CORE_EF.Controllers
{
    [Authorize]
    public class ServizioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServizioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servizio
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context
                .Servizi.Include(s => s.Prenotazione)
                .Include(s => s.TipoServizio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Servizio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context
                .Servizi.Include(s => s.Prenotazione)
                .Include(s => s.TipoServizio)
                .FirstOrDefaultAsync(m => m.IdServizio == id);
            if (servizio == null)
            {
                return NotFound();
            }

            return View(servizio);
        }

        // GET: Servizio/Create
        public IActionResult Create()
        {
            ViewData["IdPrenotazione"] = new SelectList(
                _context.Prenotazioni,
                "IdPrenotazione",
                "IdPrenotazione"
            );
            ViewData["IdTipoServizio"] = new SelectList(
                _context.TipiServizi,
                "IdTipoServizio",
                "NomeTipoServizio"
            );
            return View();
        }

        // POST: Servizio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("IdPrenotazione,IdTipoServizio,CostoServizio")] Servizio servizio
        )
        {
            ModelState.Remove("Prenotazione");
            ModelState.Remove("TipoServizio");

            if (ModelState.IsValid)
            {
                _context.Add(servizio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPrenotazione"] = new SelectList(
                _context.Prenotazioni,
                "IdPrenotazione",
                "IdPrenotazione",
                servizio.IdPrenotazione
            );
            ViewData["IdTipoServizio"] = new SelectList(
                _context.TipiServizi,
                "IdTipoServizio",
                "IdTipoServizio",
                servizio.IdTipoServizio
            );
            return View(servizio);
        }

        // GET: Servizio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context.Servizi.FindAsync(id);
            if (servizio == null)
            {
                return NotFound();
            }
            ViewData["IdPrenotazione"] = new SelectList(
                _context.Prenotazioni,
                "IdPrenotazione",
                "IdPrenotazione",
                servizio.IdPrenotazione
            );
            ViewData["IdTipoServizio"] = new SelectList(
                _context.TipiServizi,
                "IdTipoServizio",
                "NomeTipoServizio",
                servizio.IdTipoServizio
            );
            return View(servizio);
        }

        // POST: Servizio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("IdServizio,IdPrenotazione,IdTipoServizio,CostoServizio")] Servizio servizio
        )
        {
            if (id != servizio.IdServizio)
            {
                return NotFound();
            }
            ModelState.Remove("Prenotazione");
            ModelState.Remove("TipoServizio");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servizio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServizioExists(servizio.IdServizio))
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
            ViewData["IdPrenotazione"] = new SelectList(
                _context.Prenotazioni,
                "IdPrenotazione",
                "IdPrenotazione",
                servizio.IdPrenotazione
            );
            ViewData["IdTipoServizio"] = new SelectList(
                _context.TipiServizi,
                "IdTipoServizio",
                "IdTipoServizio",
                servizio.IdTipoServizio
            );
            return View(servizio);
        }

        // GET: Servizio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context
                .Servizi.Include(s => s.Prenotazione)
                .Include(s => s.TipoServizio)
                .FirstOrDefaultAsync(m => m.IdServizio == id);
            if (servizio == null)
            {
                return NotFound();
            }

            return View(servizio);
        }

        // POST: Servizio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servizio = await _context.Servizi.FindAsync(id);
            if (servizio != null)
            {
                _context.Servizi.Remove(servizio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(
                "Details",
                "Prenotazione",
                new { id = servizio.IdPrenotazione }
            );
        }

        private bool ServizioExists(int id)
        {
            return _context.Servizi.Any(e => e.IdServizio == id);
        }
    }
}
