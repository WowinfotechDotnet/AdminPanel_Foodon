using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodOnAdmin.Models;

namespace FoodOnAdmin.Controllers
{
    public class RestaurantVideoController : Controller
    {
        private DB_FoodOnLinkEntities db = new DB_FoodOnLinkEntities();
        public static string connectionString = ConfigurationManager.ConnectionStrings["DB_FoodOnLink"].ConnectionString;
        public static SqlConnection con = new SqlConnection(connectionString);
        static SqlCommand cmd;
        static SqlDataAdapter sda;

        static DataTable dt;
        // GET: RestaurantVideo
        public ActionResult Index()
        {
            return View();
        }


        public class Search_Admin
        {
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public string VIDEO_NAME { get; set; }
            public string START_DATE { get; set; }
            public string END_DATE { get; set; }
            public long RES_ID { get; set; }
        }

        public JsonResult TotalRecordCount(Search_Admin tB_Admin)
        {
            int i = 0;
            try
            {
                cmd = new SqlCommand("Get_RestaurantVideoURLCount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VIDEO_NAME", tB_Admin.VIDEO_NAME);
                cmd.Parameters.AddWithValue("@RES_ID", tB_Admin.RES_ID);
                cmd.Parameters.AddWithValue("@START_DATE", tB_Admin.START_DATE);
                cmd.Parameters.AddWithValue("@END_DATE", tB_Admin.END_DATE);
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
            cmd = new SqlCommand("Panel_GetRestaurantVideoURLDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageSize", tB_Admin.PageSize);
            cmd.Parameters.AddWithValue("@PageNo", tB_Admin.PageNo - 1);
            cmd.Parameters.AddWithValue("@VIDEO_NAME", tB_Admin.VIDEO_NAME);
            cmd.Parameters.AddWithValue("@RES_ID", tB_Admin.RES_ID);
            cmd.Parameters.AddWithValue("@START_DATE", tB_Admin.START_DATE);
            cmd.Parameters.AddWithValue("@END_DATE", tB_Admin.END_DATE);

            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            con.Close();
            RestaurantVideoURLMaster rt;
            List<RestaurantVideoURLMaster> FinalreportList = new List<RestaurantVideoURLMaster>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new RestaurantVideoURLMaster();
                    try
                    {
                        rt.RES_ID = Convert.ToInt64(dt.Rows[i]["RES_ID"]);
                        rt.VID_BANNER_ID = Convert.ToInt64(dt.Rows[i]["VID_BANNER_ID"]);
                        rt.VIDEO_URL_LINK = (dt.Rows[i]["VIDEO_URL_LINK"].ToString());
                        rt.RES_NAME = (dt.Rows[i]["RES_NAME"].ToString());
                        rt.VIDEO_NAME = (dt.Rows[i]["VIDEO_NAME"].ToString());
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


        public JsonResult GetRestaurants()
        {
            var _getadmin = db.TB_Restaurant.Where(z => z.STATUS == "Active").Select(s => new { s.RES_ID, s.RES_NAME, s.MOBILE_NUMBER, s.FOOD_TYPE, s.PINCODE, s.STATUS, s.REG_DATE, s.ADDRESS, s.PASSWORD, s.CONFIRM_PASSWORD, s.LATITUDE, s.LONGITUDE, s.RES_OPEN_TIME, s.RES_CLOSE_TIME, s.DESCRIPTION, s.OWNER_NAME, s.RES_LOGO }).ToList();
            return Json(_getadmin, JsonRequestBehavior.AllowGet);
        }



        public ActionResult AddUpdateRestaurantVideoURLs(RestaurantVideoURLMaster tB_admin)
        {

            try
            {
                cmd = new SqlCommand("InsertUpdate_RestaurantVideoURL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VID_BANNER_ID", tB_admin.VID_BANNER_ID);
                cmd.Parameters.AddWithValue("@RES_ID", tB_admin.RES_ID);
                cmd.Parameters.AddWithValue("@VIDEO_NAME", tB_admin.VIDEO_NAME);
                cmd.Parameters.AddWithValue("@VIDEO_URL_LINK", tB_admin.VIDEO_URL_LINK);
                cmd.Parameters.AddWithValue("@ACTION", tB_admin.ACTION);
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
            TB_Video_Banner_Image tB_Admin = db.TB_Video_Banner_Image.Where(b => b.VID_BANNER_ID == id).SingleOrDefault();
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
    }
}