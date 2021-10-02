using Evidence_07_Mid_Monthly.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evidence_07_Mid_Monthly.Controllers
{
    [Authorize]
    public class TraineeController : Controller
    {
        TraineeDbContext db = new TraineeDbContext();
        public ActionResult Index()
        {
            return View(db.Trainees.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.CourseList = db.Courses.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(TraineeViewModel t)
        {
            if (ModelState.IsValid)
            {

                var f = Server.MapPath("~/ImageUploads/");
                string file = Guid.NewGuid() + Path.GetExtension(t.Picture.FileName);
                t.Picture.SaveAs(f + file);
                var pic = new Trainee { TraineeName = t.TraineeName, Email = t.Email, Picture = file, TLocation = t.TLocation, CourseId = t.CourseId };
                db.Trainees.Add(pic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseList = db.Courses.ToList();
            return View(t);
        }
        public ActionResult Edit(int id)
        {
            var data = db.Trainees.First(x => x.TraineeId == id);
            ViewBag.Pic = data.Picture;
            ViewBag.CourseList = db.Courses.ToList();
            return View(new TraineeViewModel
            {
                TraineeId = data.TraineeId,
                TraineeName = data.TraineeName,
                Email = data.Email,
                TLocation = data.TLocation,
                CourseId = data.CourseId
            });
        }
        [HttpPost]
        public ActionResult Edit(TraineeViewModel t)
        {
            if (ModelState.IsValid)
            {
                var ts = db.Trainees.First(x => x.TraineeId == t.TraineeId);
                if (t.Picture != null && t.Picture.FileName != "")
                {
                    var f = Server.MapPath("~/ImageUploads/");
                    string file = Guid.NewGuid() + Path.GetExtension(t.Picture.FileName);
                    t.Picture.SaveAs(f + file);
                    ts.Picture = file;
                }
                ts.TraineeName = t.TraineeName;
                ts.Email = t.Email;
                ts.TLocation = t.TLocation;
                ts.CourseId = t.CourseId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseList = db.Courses.ToList();
            return View(t);
        }
        public ActionResult Delete(int id)
        {
            var data = db.Trainees.First(x => x.TraineeId == id);
            ViewBag.Pic = data.Picture;
            return View(db.Trainees.First(x => x.TraineeId == id));
        }
        [HttpPost]
        public ActionResult Delete(Trainee td)
        {
            if (ModelState.IsValid)
            {

                db.Entry(td).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(td);
        }
    }
}