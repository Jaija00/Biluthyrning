using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biluthyrning.Data;
using Biluthyrning.Models;

namespace Biluthyrning.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUser userRepository;

        public UsersController(ApplicationDbContext context, IUser userRepository)
        {
            _context = context;
            this.userRepository = userRepository;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return userRepository != null ? 
                          View(await userRepository.GetAllAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || userRepository == null)
            {
                return NotFound();
            }

            var user = await userRepository
                .GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Blacklist,IsAdmin,FirstName,LastName,Email,PhoneNumber")] User user)
        {
            if (ModelState.IsValid)
            {
               await userRepository.CreateAsync(user);
              
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || userRepository == null)
            {
                return NotFound();
            }

            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Blacklist,IsAdmin,FirstName,LastName,Email,PhoneNumber")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    await userRepository.UpdateAsync(user);
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
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || userRepository == null)
            {
                return NotFound();
            }

            var user = await userRepository
                .GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (userRepository == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            var user = await userRepository.GetByIdAsync(id);
            if (user != null)
            {
               await userRepository.DeleteAsync(id);
            }
            
            //await userRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
