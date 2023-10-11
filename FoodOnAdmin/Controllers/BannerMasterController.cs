using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOnAdmin.Controllers
{
    [VerifyUserAttribute]
    public class BannerMasterController : Controller
    {
        // GET: BannerMaster
        public ActionResult Index()
        {
            return View();
        }
    }
}