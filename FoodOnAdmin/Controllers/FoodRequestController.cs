using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOnAdmin.Controllers
{
    [VerifyUserAttribute]
    public class FoodRequestController : Controller
    {
        // GET: FoodRequest
        public ActionResult TotalFoodRequest()
        {
            return View();
        }
        public ActionResult FoodRequest()
        {
            return View();
        }
        public ActionResult FoodRequestDetails()
        {
            return View();
        }
    }
}