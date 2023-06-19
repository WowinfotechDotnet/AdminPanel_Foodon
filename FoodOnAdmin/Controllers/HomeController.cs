using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FoodOnAdmin.Models;

namespace FoodOnAdmin.Controllers
{
    public class HomeController : Controller
    {
        private DB_FoodOnLinkEntities db = new DB_FoodOnLinkEntities();
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Login()
        {
            if (Session["ADMIN_ID"] == null)
            {
                HttpCookie loginCookie = Request.Cookies["FoodOnDetails"];
                if (loginCookie != null)
                {
                    string ADMIN_ID = loginCookie.Values["ADMIN_ID"];
                    string ADMIN_NAME = loginCookie.Values["ADMIN_NAME"];
                    string MOBILE_NUMBER = loginCookie.Values["MOBILE_NO"];

                    Session["ADMIN_ID"] = ADMIN_ID;
                    Session["ADMIN_NAME"] = ADMIN_NAME;
                    Session["MOBILE_NO"] = MOBILE_NUMBER;

                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }

        public ActionResult Logout()
        {
            Session["ADMIN_ID"] = null;
            Session["ADMIN_NAME"] = null;
            Session["MOBILE_NO"] = null;
            Session.Clear();

            //Delete GrapeMasterLoginDetails Cookie
            if (Request.Cookies["FoodOnDetails"] != null)
            {
                Response.Cookies["FoodOnDetails"].Expires = DateTime.Now.AddDays(-1);
            }

            //return Json(new { success = true });

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult Login(HomeLogin login)
        {
            try
            {
                string MOBILE_NUMBER = login.MOBILE_NO;
                string PASSWORD = login.PASSWORD;
                string ReturnUrl = "";

                var user = db.TB_AdminMaster.Where(a => a.MOBILE_NO.Equals(login.MOBILE_NO) && a.PASSWORD.Equals(login.PASSWORD)).Where(s => s.STATUS == "Active").FirstOrDefault();
                if (user != null)
                {
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        Session["ADMIN_ID"] = user.ADMIN_ID;
                        Session["ADMIN_NAME"] = user.ADMIN_NAME;
                        Session["MOBILE_NO"] = user.MOBILE_NO;

                        if (login.RememberMe)
                        {
                            if (HttpContext.Request.Cookies["FoodOnDetails"] == null)
                            {
                                HttpCookie _rememberme = new HttpCookie("FoodOnDetails");
                                _rememberme.Expires = DateTime.Now.AddDays(1);
                                _rememberme["ADMIN_ID"] = user.ADMIN_ID.ToString();
                                _rememberme["ADMIN_NAME"] = user.ADMIN_NAME.ToString();
                                _rememberme["MOBILE_NO"] = user.MOBILE_NO.ToString();
                                _rememberme.Secure = false;
                                Response.Cookies.Add(_rememberme);
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.WrongPassword = "Please enter correct mobile number or Password";
                    return View();
                }
                ModelState.Remove("Password");
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Login", "Home");
        }

    }
}