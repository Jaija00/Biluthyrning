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
    public class BookingsController : Controller
    {
        private readonly IBooking bookingRepository;
        private readonly ICar carRepository;

        public BookingsController(IBooking bookingRepository, ICar carRepository)
        {
            this.bookingRepository = bookingRepository;
            this.carRepository = carRepository;
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

        public async Task<IActionResult> FilterList()
        {
            return View();
        }

        // GET: Bookings/AvailableCars
        public async Task<IActionResult> AvailableCars(DateTime startDate, DateTime endDate)
        {
            var availableCarsVM = new List<AvailableCarsViewModel>();
            foreach (var car in await carRepository.GetAllAsync())
            {
                var post = new AvailableCarsViewModel();
                post.Car = car;
                post.Booking = await bookingRepository.GetByIdAsync(car.CarId);
                if (post.Booking != null)
                {
                    if ((startDate >= post.Booking.End && endDate >= post.Booking.End) || (endDate <= post.Booking.Start && startDate <= post.Booking.Start))
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
        public IActionResult Create()
        {
            return View();
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
                    return RedirectToAction(nameof(Index));
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
            if (id == null || bookingRepository.GetByIdAsync(id) == null)
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
                return RedirectToAction(nameof(Index));
            }
            return View(booking);

        }

        //private async Task<IActionResult> BookingExists(int id)
        //{
        //    return await bookingRepository.GetByIdAsync(id);
        //}
    }
}
