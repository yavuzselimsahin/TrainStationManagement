using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainStationManagement.Data;
using TrainStationManagement.Models;

namespace TrainStationManagement.Controllers
{
    public class TrainsController : Controller
    {
        private readonly TrainStationManagementContext _context;

        public TrainsController(TrainStationManagementContext context)
        {
            _context = context;
        }

        // GET: Trains
        public async Task<IActionResult> Index()
        {
            string userName = Request.Cookies["SESSION"];
            var usr = await _context.User.FirstOrDefaultAsync(m => m.UserName == userName);
            if (usr == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var trainStationManagementContext = _context.Train.Include(t => t.ArrivalStation).Include(t => t.DepartureStation);
                return View(await trainStationManagementContext.ToListAsync());
            }
            
        }

        // GET: Trains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string userName = Request.Cookies["SESSION"];
            var usr = await _context.User.FirstOrDefaultAsync(m => m.UserName == userName);
            if (usr == null)
            {
                return RedirectToAction("Login","Users");
            }
            else
            {
                if (id == null || _context.Train == null)
                {
                    return NotFound();
                }

                var train = await _context.Train
                    .Include(t => t.ArrivalStation)
                    .Include(t => t.DepartureStation)
                    .FirstOrDefaultAsync(m => m.TrainId == id);
                if (train == null)
                {
                    return NotFound();
                }

                return View(train);
            }
            
        }

        // GET: Trains/Create
        public IActionResult Create()
        {
            
            
                ViewData["ArrivalStationId"] = new SelectList(_context.TrainStation, "StationId", "StationName");
                ViewData["DepartureStationId"] = new SelectList(_context.TrainStation, "StationId", "StationName");
                return View();
            
            
        }

        // POST: Trains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainId,DepartureStationId,ArrivalStationId,DepartureTime,ArrivalTime")] Train train)
        {

            if (ModelState.IsValid)
            {
                var departureTime = train.DepartureTime;
                var arrivalTime = train.ArrivalTime;
                var departureStation = await _context.TrainStation.FindAsync(train.DepartureStationId);
                var arrivalStation = await _context.TrainStation.FindAsync(train.ArrivalStationId);
                if (((departureStation != null && arrivalStation != null) && (arrivalStation != departureStation)) && arrivalTime != departureTime)
                {
                    _context.Add(train);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your input values");
                }
            }
            ViewData["ArrivalStationId"] = new SelectList(_context.TrainStation, "StationId", "StationName", train.ArrivalStationId);
            ViewData["DepartureStationId"] = new SelectList(_context.TrainStation, "StationId", "StationName", train.DepartureStationId);
            return View(train);
        }

        // GET: Trains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string userName = Request.Cookies["SESSION"];
            var usr = await _context.User.FirstOrDefaultAsync(m => m.UserName == userName);
            if (usr == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (id == null || _context.Train == null)
                {
                    return NotFound();
                }

                var train = await _context.Train.FindAsync(id);
                if (train == null)
                {
                    return NotFound();
                }
                ViewData["ArrivalStationId"] = new SelectList(_context.TrainStation, "StationId", "StationName", train.ArrivalStationId);
                ViewData["DepartureStationId"] = new SelectList(_context.TrainStation, "StationId", "StationName", train.DepartureStationId);
                return View(train);

            }
            
        }

        // POST: Trains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainId,DepartureStationId,ArrivalStationId,DepartureTime,ArrivalTime")] Train train)
        {
            if (id != train.TrainId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(train);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainExists(train.TrainId))
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
            ViewData["ArrivalStationId"] = new SelectList(_context.TrainStation, "StationId", "StationName", train.ArrivalStationId);
            ViewData["DepartureStationId"] = new SelectList(_context.TrainStation, "StationId", "StationName", train.DepartureStationId);
            return View(train);
        }

        // GET: Trains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string userName = Request.Cookies["SESSION"];
            var usr = await _context.User.FirstOrDefaultAsync(m => m.UserName == userName);
            if (usr == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (id == null || _context.Train == null)
                {
                    return NotFound();
                }

                var train = await _context.Train
                    .Include(t => t.ArrivalStation)
                    .Include(t => t.DepartureStation)
                    .FirstOrDefaultAsync(m => m.TrainId == id);
                if (train == null)
                {
                    return NotFound();
                }

                return View(train);
            }
           
        }

        // POST: Trains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Train == null)
            {
                return Problem("Entity set 'TrainStationManagementContext.Train'  is null.");
            }
            var train = await _context.Train.FindAsync(id);
            if (train != null)
            {
                _context.Train.Remove(train);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainExists(int id)
        {
          return (_context.Train?.Any(e => e.TrainId == id)).GetValueOrDefault();
        }
    }
}
