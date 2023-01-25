using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aljas_Consultation.Data;
using Aljas_Consultation.Models;
using static Aljas_Consultation.Controllers.ConsultationsController;
using Microsoft.AspNetCore.Authorization;

namespace Aljas_Consultation.Controllers
{
    public class PeriodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ConsultationsController _consultationsController;

        private int _lastAddedPeriodId;

        public PeriodsController(ApplicationDbContext context)
        {
            _context = context;
            _consultationsController = new ConsultationsController(context);
    }

        // GET: Periods
        public async Task<IActionResult> Index()
        {
              return View(await _context.Period.ToListAsync());
        }

        // GET: Periods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Period == null)
            {
                return NotFound();
            }

            var period = await _context.Period
                .FirstOrDefaultAsync(m => m.Id == id);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // GET: Periods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Periods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Period period)
        {
            if (ModelState.IsValid)
            {
                _context.Add(period);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(period);
        }

        [Authorize]
        public IActionResult AddPeriod()
        {
            return View();
        }

        // POST: Periods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPeriod(Period period)
        {
            if (ModelState.IsValid)
            {
                _context.Add(period);
                await _context.SaveChangesAsync();
                _lastAddedPeriodId = period.Id;
                return View("ShowPeriods", _context.Period.ToList());
            }
            return View(period);
        }

        public async Task<IActionResult> ShowPeriods([Bind("Name")] Period period)
        {
            return View(await _context.Period.ToListAsync());
        }

        // GET: Periods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Period == null)
            {
                return NotFound();
            }

            var period = await _context.Period.FindAsync(id);
            if (period == null)
            {
                return NotFound();
            }
            return View(period);
        }

        // POST: Periods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Period period)
        {
            if (id != period.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(period);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeriodExists(period.Id))
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
            return View(period);
        }


        [Authorize]
        public async Task<IActionResult> PeriodEdit(int? id)
        {
            if (id == null || _context.Period == null)
            {
                return NotFound();
            }

            var period = await _context.Period.FindAsync(id);
            if (period == null)
            {
                return NotFound();
            }
            return View(period);
        }

        // POST: Periods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PeriodEdit(int id, [Bind("Id,Name")] Period period)
        {
            if (id != period.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(period);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeriodExists(period.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ShowPeriods));
            }
            return View(period);
        }

        // GET: Periods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Period == null)
            {
                return NotFound();
            }

            var period = await _context.Period
                .FirstOrDefaultAsync(m => m.Id == id);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // POST: Periods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Period == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Period'  is null.");
            }
            var period = await _context.Period.FindAsync(id);
            if (period != null)
            {
                _context.Period.Remove(period);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeriodExists(int id)
        {
          return _context.Period.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<IActionResult> PeriodDelete(int? id)
        {
            if (id == null || _context.Period == null)
            {
                return NotFound();
            }

            var period = await _context.Period
                .FirstOrDefaultAsync(m => m.Id == id);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // POST: Periods/Delete/5
        [Authorize]
        [HttpPost, ActionName("PeriodDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PeriodDeleteConfirmed(int id)
        {
            if (_context.Period == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Period'  is null.");
            }
            var period = await _context.Period.FindAsync(id);
            if (period != null)
            {
                _context.Period.Remove(period);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowPeriods));
        }
    }
}
