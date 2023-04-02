using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TrainStationManagement.Data;
using TrainStationManagement.Models;

namespace TrainStationManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrainStationManagementContext _context;

        public HomeController(TrainStationManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var trainStationManagementContext = _context.Train.Include(t => t.ArrivalStation).Include(t => t.DepartureStation).OrderBy(t => t.DepartureTime);
            return View(await trainStationManagementContext.ToListAsync());
        }

        public async Task<IActionResult> TrainStationsView()
        {
            return _context.TrainStation != null ?
                          View(await _context.TrainStation.ToListAsync()) :
                          Problem("Entity set 'TrainStationManagementContext.TrainStation'  is null.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}