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
    public class BookingsController : Controller
    {
        private readonly IBooking bookingRepository;

        public BookingsController(IBooking bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            return View(bookingRepository.GetAllAsync());

        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(bookingRepository.GetByIdAsync(id));
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
                    await bookingRepository.AddAsync(booking);
                    await bookingRepository.SaveChangesAsync();
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
                    await bookingRepository.SaveChangesAsync();
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
                    await bookingRepository.SaveChangesAsync();
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
