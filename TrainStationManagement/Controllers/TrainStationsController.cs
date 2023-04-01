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
    public class TrainStationsController : Controller
    {
        private readonly TrainStationManagementContext _context;

        public TrainStationsController(TrainStationManagementContext context)
        {
            _context = context;
        }

        // GET: TrainStations
        public async Task<IActionResult> Index()
        {
              return _context.TrainStation != null ? 
                          View(await _context.TrainStation.ToListAsync()) :
                          Problem("Entity set 'TrainStationManagementContext.TrainStation'  is null.");
        }

        // GET: TrainStations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainStation == null)
            {
                return NotFound();
            }

            var trainStation = await _context.TrainStation
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (trainStation == null)
            {
                return NotFound();
            }

            return View(trainStation);
        }

        // GET: TrainStations/Create
        public IActionResult Create()
        {
            ViewData["DepartureStationId"] = new SelectList(_context.TrainStation, "StationId", "StationName");
            ViewData["ArrivalStationId"] = new SelectList(_context.TrainStation, "StationId", "StationName");
            
            return View();
        }

        // POST: TrainStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StationId,StationName,StationAddress,StationLocation")] TrainStation trainStation, int DepartureStationId, int ArrivalStationId)
        {
            trainStation.DepartureTrains = new List<Train>();
            trainStation.ArrivalTrains = new List<Train>();
            if (ModelState.IsValid)
            {

                _context.Add(trainStation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(trainStation);
        }

        // GET: TrainStations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainStation == null)
            {
                return NotFound();
            }

            var trainStation = await _context.TrainStation.FindAsync(id);
            if (trainStation == null)
            {
                return NotFound();
            }
            return View(trainStation);
        }

        // POST: TrainStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StationId,StationName,StationAddress,StationLocation")] TrainStation trainStation)
        {
            if (id != trainStation.StationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainStation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainStationExists(trainStation.StationId))
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
            return View(trainStation);
        }

        // GET: TrainStations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainStation == null)
            {
                return NotFound();
            }

            var trainStation = await _context.TrainStation
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (trainStation == null)
            {
                return NotFound();
            }

            return View(trainStation);
        }

        // POST: TrainStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainStation == null)
            {
                return Problem("Entity set 'TrainStationManagementContext.TrainStation'  is null.");
            }
            var trainStation = await _context.TrainStation.FindAsync(id);
            if (trainStation != null)
            {
                _context.TrainStation.Remove(trainStation);
            }
            var train = await _context.Train.FindAsync(id);
            if (train != null)
            {
                _context.Train.Remove(train);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainStationExists(int id)
        {
          return (_context.TrainStation?.Any(e => e.StationId == id)).GetValueOrDefault();
        }
    }
}
