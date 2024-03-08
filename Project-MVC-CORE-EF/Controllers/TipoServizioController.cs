using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_MVC_CORE_EF.Data;
using Project_MVC_CORE_EF.Models;

namespace Project_MVC_CORE_EF.Controllers
{
    public class TipoServizioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoServizioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoServizio
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipiServizi.ToListAsync());
        }

        // GET: TipoServizio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServizio = await _context.TipiServizi.FirstOrDefaultAsync(m =>
                m.IdTipoServizio == id
            );
            if (tipoServizio == null)
            {
                return NotFound();
            }

            return View(tipoServizio);
        }

        // GET: TipoServizio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoServizio/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("NomeTipoServizio")] TipoServizio tipoServizio
        )
        {
            ModelState.Remove("Servizi");

            if (ModelState.IsValid)
            {
                _context.Add(tipoServizio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoServizio);
        }

        // GET: TipoServizio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServizio = await _context.TipiServizi.FindAsync(id);
            if (tipoServizio == null)
            {
                return NotFound();
            }
            return View(tipoServizio);
        }

        // POST: TipoServizio/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("IdTipoServizio,NomeTipoServizio")] TipoServizio tipoServizio
        )
        {
            if (id != tipoServizio.IdTipoServizio)
            {
                return NotFound();
            }
            ModelState.Remove("Servizi");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoServizio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoServizioExists(tipoServizio.IdTipoServizio))
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
            return View(tipoServizio);
        }

        // GET: TipoServizio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServizio = await _context.TipiServizi.FirstOrDefaultAsync(m =>
                m.IdTipoServizio == id
            );
            if (tipoServizio == null)
            {
                return NotFound();
            }

            return View(tipoServizio);
        }

        // POST: TipoServizio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoServizio = await _context.TipiServizi.FindAsync(id);
            if (tipoServizio != null)
            {
                _context.TipiServizi.Remove(tipoServizio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoServizioExists(int id)
        {
            return _context.TipiServizi.Any(e => e.IdTipoServizio == id);
        }
    }
}
