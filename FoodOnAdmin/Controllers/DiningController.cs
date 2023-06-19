using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOnAdmin.Controllers
{
    public class DiningController : Controller
    {
        // GET: Dining
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DiningRequestDetails()
        {
            return View();
        }
        public ActionResult TotalDinningRequest()
        {
            return View();
        }
    }
}