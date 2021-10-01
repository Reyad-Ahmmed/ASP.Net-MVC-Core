using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Brand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile_Brand.Controllers
{
    [Authorize]
    public class BrandsController : Controller
    {
        public MobileDbContext db;
        public BrandsController(MobileDbContext db) { this.db = db; }
        public IActionResult Index()
        {
            return View(db.Brands.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand b)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(b);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b);
        }
        public IActionResult Edit(int id)
        {
            return View(db.Brands.First(x=>x.BrandId==id));
        }
        [HttpPost]
        public IActionResult Edit(Brand b)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b);
        }
        public IActionResult Delete(int id)
        {
            return View(db.Brands.Include(x=>x.Mobiles).First(x => x.BrandId == id));
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var d = new Brand { BrandId = id };
            db.Entry(d).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
