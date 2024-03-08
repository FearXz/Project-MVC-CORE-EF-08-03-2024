using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MVC_CORE_EF.Data;
using Project_MVC_CORE_EF.Models;

namespace Project_MVC_CORE_EF.Controllers
{
    [Authorize]
    public class CameraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CameraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Camera
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Camere.Include(c => c.TipoCamera);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Camera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context
                .Camere.Include(c => c.TipoCamera)
                .FirstOrDefaultAsync(m => m.IdCamera == id);
            if (camera == null)
            {
                return NotFound();
            }

            return View(camera);
        }

        // GET: Camera/Create
        public IActionResult Create()
        {
            ViewData["IdTipoCamera"] = new SelectList(
                _context.TipiCamere,
                "IdTipoCamera",
                "NomeTipoCamera"
            );
            return View();
        }

        // POST: Camera/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("IdTipoCamera,NumeroCamera,CostoCamera,CameraDisponibile")] Camera camera
        )
        {
            ModelState.Remove("Prenotazioni");
            ModelState.Remove("TipoCamera");

            if (ModelState.IsValid)
            {
                _context.Add(camera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoCamera"] = new SelectList(
                _context.TipiCamere,
                "IdTipoCamera",
                "NomeTipoCamera",
                camera.IdTipoCamera
            );
            return View(camera);
        }

        // GET: Camera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context.Camere.FindAsync(id);
            if (camera == null)
            {
                return NotFound();
            }
            ViewData["IdTipoCamera"] = new SelectList(
                _context.TipiCamere,
                "IdTipoCamera",
                "NomeTipoCamera",
                camera.IdTipoCamera
            );
            return View(camera);
        }

        // POST: Camera/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("IdCamera,IdTipoCamera,NumeroCamera,CostoCamera,CameraDisponibile")] Camera camera
        )
        {
            if (id != camera.IdCamera)
            {
                return NotFound();
            }
            ModelState.Remove("Prenotazioni");
            ModelState.Remove("TipoCamera");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(camera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CameraExists(camera.IdCamera))
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
            ViewData["IdTipoCamera"] = new SelectList(
                _context.TipiCamere,
                "IdTipoCamera",
                "NomeTipoCamera",
                camera.IdTipoCamera
            );
            return View(camera);
        }

        // GET: Camera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context
                .Camere.Include(c => c.TipoCamera)
                .FirstOrDefaultAsync(m => m.IdCamera == id);
            if (camera == null)
            {
                return NotFound();
            }

            return View(camera);
        }

        // POST: Camera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camera = await _context.Camere.FindAsync(id);
            if (camera != null)
            {
                _context.Camere.Remove(camera);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CameraExists(int id)
        {
            return _context.Camere.Any(e => e.IdCamera == id);
        }
    }
}
