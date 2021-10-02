using Evidence_07_Mid_Monthly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evidence_07_Mid_Monthly.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        TraineeDbContext db = new TraineeDbContext();
        // GET: Course
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course c)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(c);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            return View(db.Courses.First(x => x.CourseId == id));
        }
        [HttpPost]
        public ActionResult Edit(Course ct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ct);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Courses.First(x => x.CourseId == id));
        }
        [HttpPost]

        public ActionResult Delete(Course ct)
        {
            if (ModelState.IsValid)
            {

                db.Entry(ct).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ct);
        }
    }
}