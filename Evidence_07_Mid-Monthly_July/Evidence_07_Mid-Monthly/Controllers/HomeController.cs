using Evidence_07_Mid_Monthly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evidence_07_Mid_Monthly.Controllers
{
    public class HomeController : Controller
    {
        TraineeDbContext db = new TraineeDbContext();
        public ActionResult Index()
        {
            return View(db.Trainees.ToList());
        }
    }
}