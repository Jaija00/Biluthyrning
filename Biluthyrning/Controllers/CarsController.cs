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
    public class CarsController : Controller
    {
        private readonly ICar carRepository;

        public CarsController(ICar carRepository)
        {
            this.carRepository = carRepository;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return carRepository != null ?
                        View(await carRepository.GetAllAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
        }
        // GET: Cars/OurCars
        public async Task<IActionResult> OurCars()
        {
            return carRepository != null ?
                        View(await carRepository.GetAllAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
        }


        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || carRepository == null)
            {
                return NotFound();
            }

            var car = await carRepository.GetByIdAsync(id);//Det här kan väl inte bli bra?

            /* .FirstOrDefaultAsync(m => m.Id == id);*/
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            List<SelectListItem> gear = new()
            {
                new SelectListItem { Value = "Manuell", Text = "Manuell" },
                new SelectListItem { Value = "Automatisk", Text = "Automatisk" },
                
            };
            ViewBag.Gear = gear;


            List<SelectListItem> fuel = new()
            {
                new SelectListItem { Value = "Bensin", Text = "Bensin" },
                new SelectListItem { Value = "Disel", Text = "Disel" },
                new SelectListItem { Value = "Elektrisk", Text = "Elektrisk" }
            };
            ViewBag.Fuel = fuel;

            List<SelectListItem> size = new()
            {
                new SelectListItem { Value = "Liten", Text = "Liten" },
                new SelectListItem { Value = "Mellan", Text = "Mellan" },
                new SelectListItem { Value = "Stor", Text = "Stor" }
            };
            ViewBag.Size = size;

            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,Color,Gear,FuelType,Size,Price,Available")] Car car)
        {
            if (ModelState.IsValid)
            {
                await carRepository.CreateAsync(car);
                //await carRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || carRepository == null)
            {
                return NotFound();
            }

            var car = await carRepository.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,Color,Manual,FuelType,Size,Price,Available")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await carRepository.UpdateAsync(car);
                //await carRepository.SaveChangesAsync();
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!CarExists(car.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || carRepository == null)
            {
                return NotFound();
            }

            var car = await carRepository.GetByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (carRepository == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
            }
            var car = await carRepository.GetByIdAsync(id);
            if (car != null)
            {
                await carRepository.DeleteAsync(id);
            }

            //await carRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool CarExists(int id)
        //{
        //    return (carRepository.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
