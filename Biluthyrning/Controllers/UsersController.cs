using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biluthyrning.Data;
using Biluthyrning.Models;
using Biluthyrning.ViewModels;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace Biluthyrning.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUser userRepository;
        private readonly ICar carRepository;
        private readonly IBooking bookingRepository;

        public UsersController(IUser userRepository, ICar carRepository, IBooking bookingRepository)
        {
            this.userRepository = userRepository;
            this.carRepository = carRepository;
            this.bookingRepository = bookingRepository;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return userRepository != null ? 
                          View(await userRepository.GetAllAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        }
        // GET: Users/UserView
        public async Task<IActionResult> UserView()
        {
            ViewBag.Users = new SelectList(await userRepository.GetAllAsync(), "Id", "FirstName");


            return View();
                      
        }

        //GET:Users/AdminLista
        public async Task<IActionResult> AdminLista()
        {
            var car = new List<RentedCarsViewModel>();
            foreach (var item in await carRepository.GetAllAsync())
            {
                var c = new RentedCarsViewModel();
                c.CarId = item.Id;
                foreach (var itemb in await bookingRepository.GetAllAsync())
                {
                    c.Start = itemb.Start;
                    c.End = itemb.End;

                    foreach (var itemc in await userRepository.GetAllAsync())
                    {
                        c.FirstName = itemc.FirstName;
                        c.LastName = itemc.LastName;
                    }
                }
                car.Add(c);
            }
            return View(car);
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
        // GET: Users/DetailsViewUser/5
        public async Task<IActionResult> DetailsViewUser(int id)
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

        // GET: Users/EditViewUser/5
        public async Task<IActionResult> EditViewUser(int id)
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
                    await userRepository.UpdateAsync(user);

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        // POST: Users/EditViewUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditViewUser(int id, [Bind("Id,Blacklist,IsAdmin,FirstName,LastName,Email,PhoneNumber")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await userRepository.UpdateAsync(user);
                
                TempData["successMessage"] = "Din information har sparats";
                return RedirectToAction("EditViewUser");
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
    }
}
