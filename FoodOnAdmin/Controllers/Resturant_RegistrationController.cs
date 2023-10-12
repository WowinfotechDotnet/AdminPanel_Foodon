using FoodOnAdmin.Models;
using FoodOnAdmin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Hosting;
using System.Drawing.Imaging;

namespace FoodOnAdmin.Controllers
{
    [VerifyUserAttribute]
    public class Resturant_RegistrationController : Controller
    {
        private DB_FoodOnLinkEntities db = new DB_FoodOnLinkEntities();
        public static string connectionString = ConfigurationManager.ConnectionStrings["DB_FoodOnLink"].ConnectionString;
        public static SqlConnection con = new SqlConnection(connectionString);
        static SqlCommand cmd;
        static SqlDataAdapter sda;

        static DataTable dt;

        // GET: AdminMaster
        public ActionResult Index()
        {
            return View();
        }


        public class Search_Admin
        {
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public string FARMER_NAME { get; set; }
            public string STATE_ID { get; set; }
        }

        public JsonResult TotalRecordCount(Search_Admin tB_Admin)
        {
            int i = 0;
            try
            {
                cmd = new SqlCommand("Get_Hotel_Count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FARMER_NAME", tB_Admin.FARMER_NAME);
                cmd.Parameters.AddWithValue("@STATE_ID", tB_Admin.STATE_ID);
                cmd.Connection = con;
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                i = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { success = i }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetallAdmin(Search_Admin tB_Admin)
        {
            cmd = new SqlCommand("Panel_GetHotel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ADMIN_ID", 1);
            cmd.Parameters.AddWithValue("@PageSize", tB_Admin.PageSize);
            cmd.Parameters.AddWithValue("@PageNo", tB_Admin.PageNo - 1);
            cmd.Parameters.AddWithValue("@FARMER_NAME", tB_Admin.FARMER_NAME);
            cmd.Parameters.AddWithValue("@STATE_ID", tB_Admin.STATE_ID);
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            con.Close();
            Hotel rt;
            List<Hotel> FinalreportList = new List<Hotel>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new Hotel();
                    try
                    {
                        rt.RES_ID = Convert.ToInt64(dt.Rows[i]["RES_ID"]);
                        rt.RES_NAME = (dt.Rows[i]["RES_NAME"].ToString());
                        rt.OWNER_NAME = (dt.Rows[i]["OWNER_NAME"].ToString());
                        rt.MOBILE_NUMBER = (dt.Rows[i]["MOBILE_NUMBER"].ToString());
                        rt.FOOD_TYPE = (dt.Rows[i]["FOOD_TYPE"]).ToString();
                        rt.RES_LOGO = (dt.Rows[i]["RES_LOGO"]).ToString();
                        rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
                        rt.ADDRESS = (dt.Rows[i]["ADDRESS"]).ToString();
                        rt.REG_DATE = (dt.Rows[i]["REG_DATE"]).ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                    FinalreportList.Add(rt);
                }

            }
            var _Monthlyreport = FinalreportList;
            return Json(_Monthlyreport, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetadminById(int id)
        {
            var _getadmin = db.TB_Restaurant.Where(z => z.RES_ID == id).Select(s => new { s.RES_ID, s.RES_NAME, s.MOBILE_NUMBER, s.FOOD_TYPE, s.PINCODE, s.STATUS, s.REG_DATE, s.ADDRESS, s.PASSWORD,s.CONFIRM_PASSWORD,s.LATITUDE,s.LONGITUDE,s.RES_OPEN_TIME,s.RES_CLOSE_TIME,s.DESCRIPTION,s.OWNER_NAME,s.RES_LOGO,s.Food,s.Sweets,s.Juice,s.Cafeteria,s.RES_RATING,s.RESTRO_BOOKING,s.FOOD_DELIVERY,s.CATEGORY }).FirstOrDefault();
            return Json(_getadmin, JsonRequestBehavior.AllowGet);
        }



        public ActionResult AddAdmin(Hotel tB_admin)
        {


            //try
            //{
            //    string OTP = Master.RandomString(6);

            //    if (tB_admin.RES_LOGO == "Yes")
            //    {
            //        string fileName = tB_admin.ImageName;
            //        string extension = tB_admin.ImageExtension;
            //        fileName = "Shop" + OTP + DateTime.Now.ToString("ddmmyyyy") + extension;
            //        string fileName1 = fileName;
            //        tB_admin.RES_LOGO = Master.serverurl + "/Images/" + fileName;
            //        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);

            //        if (tB_admin.RES_LOGO != string.Empty)
            //        {
            //            byte[] imageByteData = Convert.FromBase64String(tB_admin.ImageBase64Data);
            //            MemoryStream mem = new MemoryStream(imageByteData);
            //            System.Drawing.Image img = System.Drawing.Image.FromStream(mem);
            //            img.Save(HostingEnvironment.MapPath("~/Images/" + fileName1), ImageFormat.Jpeg);
            //        }
            //    }
            //    else
            //    {
            //        tB_admin.RES_LOGO = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

            try
            {
                cmd = new SqlCommand("Insert_TB_Hotel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RES_NAME", tB_admin.RES_NAME);
                cmd.Parameters.AddWithValue("@MOBILE_NUMBER", tB_admin.MOBILE_NUMBER);
                cmd.Parameters.AddWithValue("@OWNER_NAME", tB_admin.OWNER_NAME);
                cmd.Parameters.AddWithValue("@PASSWORD", tB_admin.PASSWORD);
                cmd.Parameters.AddWithValue("@ADDRESS", tB_admin.ADDRESS);
                cmd.Parameters.AddWithValue("@FOOD_TYPE", tB_admin.FOOD_TYPE);
                cmd.Parameters.AddWithValue("@CONFIRM_PASSWORD", tB_admin.CONFIRM_PASSWORD);
                cmd.Parameters.AddWithValue("@RES_LOGO", tB_admin.RES_LOGO);
                cmd.Parameters.AddWithValue("@RES_OPEN_TIME", tB_admin.RES_OPEN_TIME);
                cmd.Parameters.AddWithValue("@RES_CLOSE_TIME", tB_admin.RES_CLOSE_TIME);
                cmd.Parameters.AddWithValue("@LATITUDE", tB_admin.LATITUDE);
                cmd.Parameters.AddWithValue("@LONGITUDE", tB_admin.LONGITUDE);
                cmd.Parameters.AddWithValue("@PINCODE", tB_admin.PINCODE);
                cmd.Parameters.AddWithValue("@DESCRIPTION", tB_admin.DESCRIPTION);
                cmd.Parameters.AddWithValue("@FOODON_RATING", tB_admin.FOODON_RATING);
                cmd.Parameters.AddWithValue("@RESTRO_BOOKING", tB_admin.RESTRO_BOOKING);
                cmd.Parameters.AddWithValue("@FOOD_DELIVERY", tB_admin.FOOD_DELIVERY);
                cmd.Parameters.AddWithValue("@Food", tB_admin.Food);
                cmd.Parameters.AddWithValue("@Sweets", tB_admin.Sweets);
                cmd.Parameters.AddWithValue("@Juice", tB_admin.Juice);
                cmd.Parameters.AddWithValue("@Cafeteria", tB_admin.Cafeteria);
                cmd.Parameters.AddWithValue("@CATEGORY", tB_admin.CATEGORY);
                cmd.Connection = con;
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                if (i == -1)
                {
                    return Json(new { success = false });

                }
                else
                {
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {


            }

           return View("Index");
        }


        public ActionResult EditAdmin(Hotel tB_admin)
        {

            //try
            //{
            //    string OTP = Master.RandomString(6);
            //    if (tB_admin.RES_LOGO == "Yes")
            //    {
            //        string fileName = tB_admin.ImageName;
            //        string extension = tB_admin.ImageExtension;
            //        fileName = "Shop" + OTP + DateTime.Now.ToString("ddmmyyyy") + extension;
            //        string fileName1 = fileName;
            //        tB_admin.RES_LOGO = Master.serverurl + "/Images/" + fileName;
            //        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);

            //        if (tB_admin.RES_LOGO != string.Empty)
            //        {
            //            byte[] imageByteData = Convert.FromBase64String(tB_admin.ImageBase64Data);
            //            MemoryStream mem = new MemoryStream(imageByteData);
            //            System.Drawing.Image img = System.Drawing.Image.FromStream(mem);
            //            img.Save(HostingEnvironment.MapPath("~/Images/" + fileName1), ImageFormat.Jpeg);
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{

            //}

            try
            {
                cmd = new SqlCommand("Update_TB_Hotel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RES_NAME", tB_admin.RES_NAME);
                cmd.Parameters.AddWithValue("@MOBILE_NUMBER", tB_admin.MOBILE_NUMBER);
                cmd.Parameters.AddWithValue("@OWNER_NAME", tB_admin.OWNER_NAME);
                cmd.Parameters.AddWithValue("@PASSWORD", tB_admin.PASSWORD);
                cmd.Parameters.AddWithValue("@ADDRESS", tB_admin.ADDRESS);
                cmd.Parameters.AddWithValue("@FOOD_TYPE", tB_admin.FOOD_TYPE);
                cmd.Parameters.AddWithValue("@CONFIRM_PASSWORD", tB_admin.CONFIRM_PASSWORD);
                cmd.Parameters.AddWithValue("@RES_LOGO", tB_admin.RES_LOGO);
                cmd.Parameters.AddWithValue("@RES_OPEN_TIME", tB_admin.RES_OPEN_TIME);
                cmd.Parameters.AddWithValue("@RES_CLOSE_TIME", tB_admin.RES_CLOSE_TIME);
                cmd.Parameters.AddWithValue("@LATITUDE", tB_admin.LATITUDE);
                cmd.Parameters.AddWithValue("@LONGITUDE", tB_admin.LONGITUDE);
                cmd.Parameters.AddWithValue("@PINCODE", tB_admin.PINCODE);
                cmd.Parameters.AddWithValue("@DESCRIPTION", tB_admin.DESCRIPTION);
                cmd.Parameters.AddWithValue("@FOODON_RATING", tB_admin.FOODON_RATING);
                cmd.Parameters.AddWithValue("@RESTRO_BOOKING", tB_admin.RESTRO_BOOKING);
                cmd.Parameters.AddWithValue("@FOOD_DELIVERY", tB_admin.FOOD_DELIVERY);
                cmd.Parameters.AddWithValue("@Food", tB_admin.Food);
                cmd.Parameters.AddWithValue("@Sweets", tB_admin.Sweets);
                cmd.Parameters.AddWithValue("@Juice", tB_admin.Juice);
                cmd.Parameters.AddWithValue("@Cafeteria", tB_admin.Cafeteria);
                cmd.Parameters.AddWithValue("@CATEGORY", tB_admin.CATEGORY);
                cmd.Parameters.AddWithValue("@RES_ID", tB_admin.RES_ID);
                cmd.Connection = con;
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                if (i == -1)
                {
                    return Json(new { success = false });

                }
                else
                {
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {


            }

            return View("Index");
        }

        public string ChangeStatus(long id)
        {
            TB_Restaurant tB_Admin = db.TB_Restaurant.Where(b => b.RES_ID == id).SingleOrDefault();
            if (tB_Admin.STATUS == "Active")
            {
                tB_Admin.STATUS = "Deactive";
                db.SaveChanges();
            }
            else
            {
                tB_Admin.STATUS = "Active";
                db.SaveChanges();
            }
            return "Status change Successfully.";
        }



        public ActionResult Details(long id)
        {
            Session["SHOP_ID"] = id;
            return View();
        }


        public JsonResult GetallShopdetails()
        {
            long id = Convert.ToInt64(Session["SHOP_ID"]);
            var _getadmin = db.TB_Restaurant.Where(z => z.RES_ID == id).Select(s => new { s.RES_ID, s.RES_NAME, s.MOBILE_NUMBER, s.FOOD_TYPE, s.PINCODE, s.STATUS, s.REG_DATE, s.ADDRESS, s.PASSWORD, s.CONFIRM_PASSWORD, s.LATITUDE, s.LONGITUDE, s.RES_OPEN_TIME, s.RES_CLOSE_TIME, s.DESCRIPTION, s.OWNER_NAME, s.RES_LOGO, s.Food, s.Sweets, s.Juice, s.Cafeteria }).ToList();
            return Json(_getadmin, JsonRequestBehavior.AllowGet);
        }




        public JsonResult GetallHotelImages(string id)
        {
            long id1 = Convert.ToInt64(Session["SHOP_ID"]);
            cmd = new SqlCommand("Panel_GetHotelInformation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ADMIN_ID", id1);
            cmd.Parameters.AddWithValue("@R_TYPE", id);
          
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            con.Close();
            Hotel rt;
            List<Hotel> FinalreportList = new List<Hotel>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new Hotel();
                    try
                    {
                        rt.RI_ID = Convert.ToInt64(dt.Rows[i]["RI_ID"]);
                        rt.R_PHOTO = (dt.Rows[i]["R_PHOTO"].ToString());
                        rt.R_TYPE = (dt.Rows[i]["R_TYPE"].ToString());
                        rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
                        rt.REG_DATE = (dt.Rows[i]["REG_DATE"]).ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                    FinalreportList.Add(rt);
                }

            }
            var _Monthlyreport = FinalreportList;
            return Json(_Monthlyreport, JsonRequestBehavior.AllowGet);
        }
        
    }
}