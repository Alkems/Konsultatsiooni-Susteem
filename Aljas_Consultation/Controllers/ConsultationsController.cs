using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aljas_Consultation.Data;
using Aljas_Consultation.Models;

namespace Aljas_Consultation.Controllers
{
    public class ConsultationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consultations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Consultation.Include(c => c.Session);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> ConsultationsByPeriod([Bind(Prefix = "id")] int PeriodId)
        {
            var applicationDbContext = _context
                .Consultation
                .Include(c => c.Session)
                .Where(r => r.PeriodId == PeriodId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Consultations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consultation == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultation
                .Include(c => c.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // GET: Consultations/Create
        public IActionResult Create()
        {
            ViewData["PeriodId"] = CreatePeriodSelectList();
            return View();
        }

        private List<SelectListItem> CreatePeriodSelectList(int? selected=null)
        {
            var selectList = new SelectList(_context.Set<Period>(), "Id", "Name", selected).ToList();
            selectList.Insert(0, new SelectListItem("Vali Periood","-1"));
            return selectList;
        }

        // POST: Consultations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Teacher,Classroom,Day,StartTime,EndTime,PeriodId")] Consultation consultation)
        {

            var period = await _context.Period
                .FirstOrDefaultAsync(m => m.Id == consultation.PeriodId);
            consultation.Session = period;
            ModelState.ClearValidationState(nameof(Consultation.Session));
            TryValidateModel(consultation);

            if (ModelState.IsValid)
            {
                _context.Add(consultation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeriodId"] = CreatePeriodSelectList(consultation.PeriodId);
            return View(consultation);
        }

        // GET: Consultations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consultation == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultation.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }
            ViewData["PeriodId"] = new SelectList(_context.Period, "Id", "Id", consultation.PeriodId);
            return View(consultation);
        }

        // POST: Consultations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Teacher,Classroom,Day,StartTime,EndTime,PeriodId")] Consultation consultation)
        {
            if (id != consultation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultationExists(consultation.Id))
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
            ViewData["PeriodId"] = new SelectList(_context.Period, "Id", "Id", consultation.PeriodId);
            return View(consultation);
        }

        // GET: Consultations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consultation == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultation
                .Include(c => c.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consultation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Consultation'  is null.");
            }
            var consultation = await _context.Consultation.FindAsync(id);
            if (consultation != null)
            {
                _context.Consultation.Remove(consultation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationExists(int id)
        {
          return _context.Consultation.Any(e => e.Id == id);
        }
    }
}
