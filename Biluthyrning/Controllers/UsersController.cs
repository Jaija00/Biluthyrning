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
            ViewBag.Users = new SelectList(await userRepository.GetAllAsync(), "UserId", "FirstName");


            return View();
                      
        }

        //GET:Users/AdminLista
        public async Task<IActionResult> AdminLista()
        {
            var hej = await bookingRepository.GetAllAsync();
            var car = new List<RentedCarsViewModel>();
            foreach (var item in hej)
            {
                var c = new RentedCarsViewModel();
                c.CarId = item.Id;
                var x=carRepository.GetByIdAsync(item.CarId).Result.Name;
                c.Name = x;
                c.Start = item.Start;
                c.End = item.End;
                c.FirstName = userRepository.GetByIdAsync(item.UserId).Result.FirstName;
                c.LastName = userRepository.GetByIdAsync(item.UserId).Result.LastName;
                car.Add(c);
            }
            return View(car);
        }

        // GET: Users/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //    if (id == null || userRepository == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await userRepository
        //        .GetByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);

        //}
        //[HttpGet, ActionName("Details")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Details()
        {
            var userscar = new List<DetailsUserViewModel>();
            foreach (var item in await bookingRepository.GetAllAsync())
            {
                var c = new DetailsUserViewModel();
                c.CarId = item.CarId;
                c.Start = item.Start;
                c.End = item.End;
                c.Name = carRepository.GetByIdAsync(item.CarId).Result.Name;
                c.FirstName = userRepository.GetByIdAsync(item.UserId).Result.FirstName;
                c.LastName = userRepository.GetByIdAsync(item.UserId).Result.LastName;
                c.UserId = userRepository.GetByIdAsync(item.UserId).Result.UserId;
                c.Email = userRepository.GetByIdAsync(item.UserId).Result.Email;
                c.PhoneNumber = userRepository.GetByIdAsync(item.UserId).Result.PhoneNumber;

                userscar.Add(c);
            }
            //ViewBag.UserDetails=  (await userRepository.GetAllAsync(), "UserId", "FirstName", "LastName", "Email", "PhoneNumber");
            return View(userscar);
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
        public async Task<IActionResult> Create([Bind("UserId,Blacklist,IsAdmin,FirstName,LastName,Email,PhoneNumber")] User user)
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
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Blacklist,IsAdmin,FirstName,LastName,Email,PhoneNumber")] User user)
        {
            if (id != user.UserId)
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
