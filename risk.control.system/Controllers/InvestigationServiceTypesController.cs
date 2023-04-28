using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using risk.control.system.Data;
using risk.control.system.Models;

namespace risk.control.system.Controllers
{
    public class InvestigationServiceTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvestigationServiceTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvestigationServiceTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvestigationServiceType.Include(i => i.LineOfBusiness);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvestigationServiceTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.InvestigationServiceType == null)
            {
                return NotFound();
            }

            var investigationServiceType = await _context.InvestigationServiceType
                .Include(i => i.LineOfBusiness)
                .FirstOrDefaultAsync(m => m.InvestigationServiceTypeId == id);
            if (investigationServiceType == null)
            {
                return NotFound();
            }

            return View(investigationServiceType);
        }

        // GET: InvestigationServiceTypes/Create
        public IActionResult Create()
        {
            ViewData["LineOfBusinessId"] = new SelectList(_context.LineOfBusiness, "LineOfBusinessId", "Name");
            return View();
        }

        // POST: InvestigationServiceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( InvestigationServiceType investigationServiceType)
        {
            if (investigationServiceType is not null)
            {
                _context.Add(investigationServiceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LineOfBusinessId"] = new SelectList(_context.LineOfBusiness, "LineOfBusinessId", "Name", investigationServiceType.LineOfBusinessId);
            return View(investigationServiceType);
        }

        // GET: InvestigationServiceTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.InvestigationServiceType == null)
            {
                return NotFound();
            }

            var investigationServiceType = await _context.InvestigationServiceType.FindAsync(id);
            if (investigationServiceType == null)
            {
                return NotFound();
            }
            ViewData["LineOfBusinessId"] = new SelectList(_context.LineOfBusiness, "LineOfBusinessId", "Name", investigationServiceType.LineOfBusinessId);
            return View(investigationServiceType);
        }

        // POST: InvestigationServiceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,InvestigationServiceType investigationServiceType)
        {
            if (id != investigationServiceType.InvestigationServiceTypeId)
            {
                return NotFound();
            }

            if (investigationServiceType is not null)
            {
                try
                {
                    _context.Update(investigationServiceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestigationServiceTypeExists(investigationServiceType.InvestigationServiceTypeId))
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
            ViewData["LineOfBusinessId"] = new SelectList(_context.LineOfBusiness, "LineOfBusinessId", "Name", investigationServiceType.LineOfBusinessId);
            return View(investigationServiceType);
        }

        // GET: InvestigationServiceTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.InvestigationServiceType == null)
            {
                return NotFound();
            }

            var investigationServiceType = await _context.InvestigationServiceType
                .Include(i => i.LineOfBusiness)
                .FirstOrDefaultAsync(m => m.InvestigationServiceTypeId == id);
            if (investigationServiceType == null)
            {
                return NotFound();
            }

            return View(investigationServiceType);
        }

        // POST: InvestigationServiceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.InvestigationServiceType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InvestigationServiceType'  is null.");
            }
            var investigationServiceType = await _context.InvestigationServiceType.FindAsync(id);
            if (investigationServiceType != null)
            {
                _context.InvestigationServiceType.Remove(investigationServiceType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestigationServiceTypeExists(string id)
        {
          return (_context.InvestigationServiceType?.Any(e => e.InvestigationServiceTypeId == id)).GetValueOrDefault();
        }
    }
}
