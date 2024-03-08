using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_MVC_CORE_EF.Data;
using Project_MVC_CORE_EF.Models;

namespace Project_MVC_CORE_EF.Controllers
{
    [Authorize]
    public class TipoCameraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoCameraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoCamera
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipiCamere.ToListAsync());
        }

        // GET: TipoCamera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCamera = await _context.TipiCamere.FirstOrDefaultAsync(m =>
                m.IdTipoCamera == id
            );
            if (tipoCamera == null)
            {
                return NotFound();
            }

            return View(tipoCamera);
        }

        // GET: TipoCamera/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoCamera/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeTipoCamera")] TipoCamera tipoCamera)
        {
            ModelState.Remove("Camere");
            if (ModelState.IsValid)
            {
                _context.Add(tipoCamera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCamera);
        }

        // GET: TipoCamera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCamera = await _context.TipiCamere.FindAsync(id);
            if (tipoCamera == null)
            {
                return NotFound();
            }
            return View(tipoCamera);
        }

        // POST: TipoCamera/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("IdTipoCamera,NomeTipoCamera")] TipoCamera tipoCamera
        )
        {
            if (id != tipoCamera.IdTipoCamera)
            {
                return NotFound();
            }
            ModelState.Remove("Camere");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCamera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCameraExists(tipoCamera.IdTipoCamera))
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
            return View(tipoCamera);
        }

        // GET: TipoCamera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCamera = await _context.TipiCamere.FirstOrDefaultAsync(m =>
                m.IdTipoCamera == id
            );
            if (tipoCamera == null)
            {
                return NotFound();
            }

            return View(tipoCamera);
        }

        // POST: TipoCamera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoCamera = await _context.TipiCamere.FindAsync(id);
            if (tipoCamera != null)
            {
                _context.TipiCamere.Remove(tipoCamera);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoCameraExists(int id)
        {
            return _context.TipiCamere.Any(e => e.IdTipoCamera == id);
        }
    }
}
