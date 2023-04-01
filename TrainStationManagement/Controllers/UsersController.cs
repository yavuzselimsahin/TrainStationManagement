using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.EntityFrameworkCore;
using NuGet.LibraryModel;
using TrainStationManagement.Data;
using TrainStationManagement.Models;

namespace TrainStationManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly TrainStationManagementContext _context;

        public UsersController(TrainStationManagementContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            string userName = Request.Cookies["SESSION"];
            var user = await _context.User.FirstOrDefaultAsync(m => m.UserName == userName);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return _context.User != null ?
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'TrainStationManagementContext.User'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string userName = Request.Cookies["SESSION"];
            var usr = await _context.User.FirstOrDefaultAsync(m => m.UserName == userName);
            if (usr == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (id == null || _context.User == null)
                {
                    return NotFound();
                }

                var user = await _context.User
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }

        }
            

        // GET: Users/Create
        public IActionResult Create()
        {
            string userName = Request.Cookies["SESSION"];
            var usr =  _context.User.FirstOrDefaultAsync(m => m.UserName == userName);
            if (usr == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string userName = Request.Cookies["SESSION"];
            var usr = await _context.User.FirstOrDefaultAsync(m => m.UserName == userName);
            if (usr == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (id == null || _context.User == null)
                {
                    return NotFound();
                }

                var user = await _context.User.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
           
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string userName = Request.Cookies["SESSION"];
            var usr = await _context.User.FirstOrDefaultAsync(m => m.UserName == userName);
            if (usr == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (id == null || _context.User == null)
                {
                    return NotFound();
                }

                var user = await _context.User
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);

            }

           
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'TrainStationManagementContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            
            if (userName == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserName == userName);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (user.Password != password)
            {
                
                return RedirectToAction("Login");
                
            }
            if (user.UserName != userName)
            {
                
                return RedirectToAction("Login");
                
            }
            Response.Cookies.Append("SESSION", userName);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("UserName,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                User usr = await _context.User.FirstOrDefaultAsync(m => m.UserName == user.UserName);

                await _context.SaveChangesAsync(); ;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("SESSION");
            return RedirectToAction("Index");
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
