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
    public class SubscriptionMasterController : Controller
    {
        private DB_FoodOnLinkEntities db = new DB_FoodOnLinkEntities();
        public static string connectionString = ConfigurationManager.ConnectionStrings["DB_FoodOnLink"].ConnectionString;
        public static SqlConnection con = new SqlConnection(connectionString);
        static SqlCommand cmd;
        static SqlDataAdapter sda;

        static DataTable dt;

        // GET: SubscriptionMaster
        public ActionResult Index()
        {
            return View();
        }

        public class Search_Admin
        {
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public string RESTAURANT_NAME { get; set; }
            public long RES_ID { get; set; }
            public long PACKAGE_ID { get; set; }
            public string START_DATE { get; set; }
            public string END_DATE { get; set; }
        }

        public JsonResult TotalRecordCount(Search_Admin tB_Admin)
        {
            int i = 0;
            try
            {
                cmd = new SqlCommand("Get_Subscription_Count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RESTAURANT_NAME", tB_Admin.RESTAURANT_NAME);
                cmd.Parameters.AddWithValue("@RES_ID", tB_Admin.RES_ID);
                cmd.Parameters.AddWithValue("@PACKAGE_ID", tB_Admin.PACKAGE_ID);
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
            cmd = new SqlCommand("Panel_GetSubscriptionList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageSize", tB_Admin.PageSize);
            cmd.Parameters.AddWithValue("@PageNo", tB_Admin.PageNo - 1);
            cmd.Parameters.AddWithValue("@RESTAURANT_NAME", tB_Admin.RESTAURANT_NAME);
            cmd.Parameters.AddWithValue("@RES_ID", tB_Admin.RES_ID);
            cmd.Parameters.AddWithValue("@PACKAGE_ID", tB_Admin.PACKAGE_ID);
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
            SubscriptionMaster rt;
            List<SubscriptionMaster> FinalreportList = new List<SubscriptionMaster>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new SubscriptionMaster();
                    try
                    {
                        rt.RES_ID = Convert.ToInt64(dt.Rows[i]["RES_ID"]);
                        rt.SUB_ID = Convert.ToInt64(dt.Rows[i]["SUB_ID"]);
                        rt.PACKAGE_ID = Convert.ToInt64(dt.Rows[i]["PACKAGE_ID"]);
                        rt.PACKAGE_VALIDITY = Convert.ToInt64(dt.Rows[i]["PACKAGE_VALIDITY"]);
                        rt.POST_COUNT = Convert.ToInt64(dt.Rows[i]["POST_COUNT"]);
                        rt.RES_NAME = (dt.Rows[i]["RES_NAME"].ToString());
                        rt.PACKAGE_NAME = (dt.Rows[i]["PACKAGE_NAME"].ToString());
                        rt.SUB_START_DATE = (dt.Rows[i]["SUB_START_DATE"].ToString());
                        rt.SUB_END_DATE = (dt.Rows[i]["SUB_END_DATE"]).ToString();
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

        public JsonResult GetAllRestaurants()
        {
            var _getadmin = db.TB_Restaurant.Where(z => z.STATUS == "Active").Select(s => new { s.RES_ID, s.RES_NAME, s.STATUS, s.REG_DATE }).ToList();
            return Json(_getadmin, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllPackages()
        {
            var _getadmin = db.TB_PackageMaster.Where(z => z.STATUS == "Active").Select(s => new { s.P_ID, s.PACKAGE_NAME, s.STATUS, s.REG_DATE }).ToList();
            return Json(_getadmin, JsonRequestBehavior.AllowGet);
        }

        public string ChangeStatus(long id)
        {
            TB_SubscriptionMaster tB_Admin = db.TB_SubscriptionMaster.Where(b => b.SUB_ID == id).SingleOrDefault();
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