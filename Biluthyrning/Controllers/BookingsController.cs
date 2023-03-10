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
using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace Biluthyrning.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBooking bookingRepository;
        private readonly ICar carRepository;
        private readonly IUser userRepository;

        public BookingsController(IBooking bookingRepository, ICar carRepository, IUser userRepository)
        {
            this.bookingRepository = bookingRepository;
            this.carRepository = carRepository;
            this.userRepository = userRepository;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            return View(await bookingRepository.GetAllAsync());

        }

        // GET: Bookings/Booking
        public async Task<IActionResult> Booking()
        {
            return View();
        }

<<<<<<< HEAD
<<<<<<< Updated upstream
=======
=======
>>>>>>> d90d75417849c87f412fbef8069c9ae44f931729
        // GET: Cars/BookingCarFirstView
        public async Task<IActionResult> BookingCarFirstView()
        {
            ViewBag.Brand = new SelectList(await carRepository.GetAllAsync(), "CarId", "Brand");
            ViewBag.Color = new SelectList(await carRepository.GetAllAsync(), "CarId", "Color");
            ViewBag.CarGear = new SelectList(await carRepository.GetAllAsync(), "CarId", "Gear");
            ViewBag.Fuel = new SelectList(await carRepository.GetAllAsync(), "CarId", "FuelType");
            ViewBag.CarSize = new SelectList(await carRepository.GetAllAsync(), "CarId", "Size");

            return View();
        }

<<<<<<< HEAD

>>>>>>> Stashed changes
=======
        public async Task<IActionResult> SearchedCarToBook(string name, string brand, string color, string gear, string fuel, string size)
        {
            foreach (var car in await carRepository.GetAllAsync())
            {
                if (car.Name == name && car.Brand == brand && car.Color == color
                    && car.Gear == gear && car.FuelType == fuel && car.Size == size)
                {
                    return View(car);
                }
                else
                {
                    return NotFound();
                }
            }

            return View();
        }


>>>>>>> d90d75417849c87f412fbef8069c9ae44f931729

        public async Task<IActionResult> FilterList()
        {
            return View();
        }






        //public async Task<IActionResult> SearchedCarToBook(string name, string brand, string color, string gear, string fuel, string size)
        //{
        //    foreach (var car in await carRepository.GetAllAsync())
        //    {
        //        if (car.Gear == gear && car.FuelType == fuel && car.Size == size)
        //        {
        //            return View(car);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }

        //    return View();
        //}














        // GET: Bookings/AvailableCars
        public async Task<IActionResult> AvailableCars(SearchCarViewModel searchCarViewModel)
        {
            var availableCarsVM = new List<AvailableCarsViewModel>();
            foreach (var c in await carRepository.GetAllAsync())
            {
                var post = new AvailableCarsViewModel();
                post.Car = c;
                post.Booking = await bookingRepository.GetByIdAsync(c.CarId);
                if (post.Booking != null)
                {
<<<<<<< HEAD
<<<<<<< Updated upstream
                    if ((startDate >= post.Booking.End && endDate >= post.Booking.End) || (endDate <= post.Booking.Start && startDate <= post.Booking.Start))
=======
                    if ((searchCarViewModel.DatePicker.StartDate >= post.Booking.End && searchCarViewModel.DatePicker.EndDate >= post.Booking.End) || 
                        (searchCarViewModel.DatePicker.EndDate <= post.Booking.Start && searchCarViewModel.DatePicker.StartDate <= post.Booking.Start
                        && post.Car.Gear == c.Gear && post.Car.FuelType == c.FuelType && post.Car.Size == c.Size))
>>>>>>> Stashed changes
=======
                    if ((searchCarViewModel.DatePicker.StartDate >= post.Booking.End && searchCarViewModel.DatePicker.EndDate >= post.Booking.End) || 
                        (searchCarViewModel.DatePicker.EndDate <= post.Booking.Start && searchCarViewModel.DatePicker.StartDate <= post.Booking.Start))
>>>>>>> d90d75417849c87f412fbef8069c9ae44f931729
                    {
                        availableCarsVM.Add(post);
                    }
                }
                else
                {
                    availableCarsVM.Add(post);
                }
            }
            return View(availableCarsVM);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await bookingRepository.GetByIdAsync(id));
        }

        // GET: Bookings/Create
        public async Task<IActionResult> CreateAsync(int id)
        {
            ViewBag.Users = new SelectList(await userRepository.GetAllAsync(), "UserId", "FirstName");
            var model = new Booking { CarId = id };
            return View(model);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,UserId,Start,End")] Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await bookingRepository.CreateAsync(booking);
                    return RedirectToAction(nameof(ConfirmedBooking));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }

        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || bookingRepository.GetAllAsync == null)
            {
                return NotFound();
            }

            var booking = await bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int CarId, int UserId, DateTime Start, DateTime End, Booking booking)
        {
            if (id != booking.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var b = await bookingRepository.GetByIdAsync(id);
                    b.UserId = booking.UserId;
                    b.CarId = booking.CarId;
                    b.Start = booking.Start;
                    b.End = booking.End;
                    await bookingRepository.UpdateAsync(b);
                }
                catch (Exception)
                {
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }
       
        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
         
            
             var d = new DetailsUserViewModel();
             d.Booking =await bookingRepository.GetByIdAsync(id);
             d.Car=await carRepository.GetByIdAsync(d.Booking.CarId);
            d.User = await userRepository.GetByIdAsync(d.Booking.UserId);

           
            
            return View(d);
  
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await bookingRepository.GetByIdAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    await bookingRepository.DeleteAsync(id);
                }
                catch (Exception)
                {
                    return View();
                }
                return RedirectToAction("Details","Users",new {id=booking.UserId});
            }
            return View(booking);

        }
        // GET: Bookings/ConfirmedBooking/5
        public async Task<IActionResult> ConfirmedBooking()
        {

            return View();
        }

    }
}
