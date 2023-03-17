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

        // GET: Cars/BookingCarFirstView
        public async Task<IActionResult> BookingCarFirstView()
        {

            List<SelectListItem> carGear = new()
            { new SelectListItem { Value = "Automatisk", Text = "Automatisk"},
                new SelectListItem { Value = "Manuell", Text = "Manuell"}  };

            List<SelectListItem> carFuel = new()
            {
                new SelectListItem { Value = "Bensin", Text = "Bensin" },
                new SelectListItem { Value = "Diesel", Text = "Diesel" },
                new SelectListItem { Value = "El", Text = "El" }
            };

            List<SelectListItem> carSize = new()
            {
                new SelectListItem { Value = "Liten", Text = "Liten" },
                new SelectListItem { Value = "Mellan", Text = "Mellan" },
                new SelectListItem { Value = "Stor", Text = "Stor" }
            };

            ViewBag.CarGear = carGear;
            ViewBag.Fuel = carFuel;
            ViewBag.CarSize = carSize;

            return View();
        }

        public async Task<IActionResult> FilterList()
        {
            return View();
        }

        // GET: Bookings/AvailableCars

        public async Task<IActionResult> AvailableCars(SearchCarViewModel searchCarViewModel)

        {
            var availableCarsVM = new List<AvailableCarsViewModel>();
            foreach (var c in await carRepository.GetAllAsync())
            {
                var post = new AvailableCarsViewModel();
                post.Car = c;
                post.Booking = await bookingRepository.GetByCarIdAsync(c.CarId);
                if (searchCarViewModel.Car == null && post.Booking != null)
                {
                    if ((searchCarViewModel.DatePicker.StartDate >= post.Booking.End && searchCarViewModel.DatePicker.EndDate >= post.Booking.End) ||
                         (searchCarViewModel.DatePicker.EndDate <= post.Booking.Start && searchCarViewModel.DatePicker.StartDate <= post.Booking.Start))
                    {
                        availableCarsVM.Add(post);
                    }
                }
                else if (searchCarViewModel.Car == null && post.Booking == null)
                {
                    {
                        availableCarsVM.Add(post);
                    }
                }
                else if (post.Booking != null)
                {
                    if (((searchCarViewModel.DatePicker.StartDate >= post.Booking.End && searchCarViewModel.DatePicker.EndDate >= post.Booking.End) ||
                        (searchCarViewModel.DatePicker.EndDate <= post.Booking.Start && searchCarViewModel.DatePicker.StartDate <= post.Booking.Start)) &&
                        post.Car.Gear == searchCarViewModel.Car.Gear && post.Car.FuelType == searchCarViewModel.Car.FuelType && post.Car.Size == searchCarViewModel.Car.Size)
                    {
                        availableCarsVM.Add(post);
                    }
                }
                else if (searchCarViewModel.Car != null)
                {
                    if (post.Car.Gear == searchCarViewModel.Car.Gear && post.Car.FuelType == searchCarViewModel.Car.FuelType && post.Car.Size == searchCarViewModel.Car.Size)
                    {
                        availableCarsVM.Add(post);
						
					}
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
            var model = new Booking { CarId = id, Start = DateTime.Today, End = DateTime.Today };
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
                    var bookCar =
                    await bookingRepository.GetByCarIdAsync(booking.CarId);
                    if (bookCar == null)
                    {
                        await bookingRepository.CreateAsync(booking);
                        return RedirectToAction(nameof(ConfirmedBooking));
                    }
                    if ((booking.Start >= bookCar.End && booking.End >= bookCar.End) ||
                    (booking.End <= bookCar.Start && booking.Start <= bookCar.Start))
                    {
                        await bookingRepository.CreateAsync(booking);
                        return RedirectToAction(nameof(ConfirmedBooking));
                    }
					 return View();
				
				}
         
            }
            catch
            {
				
			}
            return View();
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
				//return RedirectToAction(nameof(Index));
                return RedirectToAction("AdminLista", "Users");

            }
			return View(booking);
		}

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int id)
        {


            var d = new DetailsUserViewModel();
            d.Booking = await bookingRepository.GetByIdAsync(id);
            d.Car = await carRepository.GetByIdAsync(d.Booking.CarId);
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
                return RedirectToAction("Details", "Users", new { id = booking.UserId });
            }
            return View(booking);

		}
		// GET: Bookings/ConfirmedBooking/5
		public async Task<IActionResult> ConfirmedBooking()
		{

			return View();
		}

        // GET: Bookings/DeleteBooking/5
        public async Task<IActionResult> DeleteBooking(int id)
        {


            var d = new DetailsUserViewModel();
            d.Booking = await bookingRepository.GetByIdAsync(id);
            d.Car = await carRepository.GetByIdAsync(d.Booking.CarId);
            d.User = await userRepository.GetByIdAsync(d.Booking.UserId);



            return View(d);

        }

        // POST: Bookings/DeleteBooking/5
        [HttpPost, ActionName("DeleteBooking")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBookings(int id)
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
                return RedirectToAction("AdminLista", "Users");
            }
            return View(booking);

        }


    }
}