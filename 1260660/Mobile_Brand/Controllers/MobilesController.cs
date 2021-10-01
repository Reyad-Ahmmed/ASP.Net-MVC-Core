using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Brand.Models;
using Mobile_Brand.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mobile_Brand.Controllers
{
    [Authorize]
    public class MobilesController : Controller
    {
        public MobileDbContext db;
        public IHostingEnvironment env;
        public MobilesController(MobileDbContext db,IHostingEnvironment env)
        { this.db = db; this.env = env; }
        public IActionResult Index()
        {
            return View(db.Mobiles.Include(x=>x.Brand).ToList());
        }
        public IActionResult Create()
        {
            
            ViewBag.list = db.Brands.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(MobileViewModel mb)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                Mobile m = new Mobile { Model = mb.Model, Price = mb.Price, PublishDate = mb.PublishDate, Picture = "blank.jpg", BrandId = mb.BrandId };
                if (mb.Picture != null)
                {
                    var imagePath = env.WebRootPath + @"\uploads";

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(mb.Picture.FileName);
                    FileStream fs = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    mb.Picture.CopyTo(fs);
                    fs.Flush();
                    fs.Close();
                    m.Picture = fileName;
                }
                db.Mobiles.Add(m);
                db.SaveChanges();
                ViewBag.Success = true;
                ViewBag.Message = "Data saved successfully";
                return PartialView("ajaxView");
            }
            ViewBag.list = db.Brands.ToList();
            ViewBag.Success = false;
            ViewBag.Message = "Failed to save data";
            return PartialView("ajaxView");
        }
        public IActionResult Edit(int id)
        {
            var m = db.Mobiles.First(x=>x.MobileId==id);
            ViewBag.pic = m.Picture;
            ViewBag.list = db.Brands.ToList();
            return View(new MobileViewModel
            {
                MobileId=m.MobileId,
                Model = m.Model,
                PublishDate=m.PublishDate,
                Price=m.Price,
                BrandId=m.BrandId
            });
        }
        [HttpPost]
        public IActionResult Edit(MobileViewModel mb)
        {
            
            if (ModelState.IsValid)
            {
                var m = db.Mobiles.First(x => x.MobileId == mb.MobileId);
                m.Model = mb.Model;
                m.Price = mb.Price;
                m.PublishDate = mb.PublishDate;
                m.BrandId = mb.BrandId;
                if (mb.Picture != null)
                {
                    var imagePath = env.WebRootPath + @"\uploads";

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(mb.Picture.FileName);
                    FileStream fs = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    mb.Picture.CopyTo(fs);
                    fs.Flush();
                    fs.Close();
                    m.Picture = fileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.list = db.Brands.ToList();
            return View(mb);
        }
        public IActionResult Delete(int id)
        {
            return View(db.Mobiles.Include(x=>x.Brand).First(x => x.MobileId == id));
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var m = new Mobile { MobileId = id };
            db.Entry(m).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
