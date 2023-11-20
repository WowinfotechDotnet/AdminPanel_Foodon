using FoodOnAdmin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOnAdmin.Controllers
{
    public class RegistrationEnquiryController : Controller
    {
        private DB_FoodOnLinkEntities db = new DB_FoodOnLinkEntities();
        public static string connectionString = ConfigurationManager.ConnectionStrings["DB_FoodOnLink"].ConnectionString;
        public static SqlConnection con = new SqlConnection(connectionString);
        static SqlCommand cmd;
        static SqlDataAdapter sda;

        static DataTable dt;
        // GET: RegistrationEnquiry
        public ActionResult Index()
        {
            return View();
        }

        public class Search_Admin
        {
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public string RESTAURANT_NAME { get; set; }
            public string START_DATE { get; set; }
            public string END_DATE { get; set; }
        }

        public JsonResult TotalRecordCount(Search_Admin tB_Admin)
        {
            int i = 0;
            try
            {
                cmd = new SqlCommand("Get_Registration_Enquiry_Count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RESTAURANT_NAME", tB_Admin.RESTAURANT_NAME);
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
            cmd = new SqlCommand("Panel_Get_RegistrationEnquiryList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageSize", tB_Admin.PageSize);
            cmd.Parameters.AddWithValue("@PageNo", tB_Admin.PageNo - 1);
            cmd.Parameters.AddWithValue("@RESTAURANT_NAME", tB_Admin.RESTAURANT_NAME);
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
            RegistrationEnquiry rt;
            List<RegistrationEnquiry> FinalreportList = new List<RegistrationEnquiry>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new RegistrationEnquiry();
                    try
                    {
                        rt.REG_ENQ_ID = Convert.ToInt64(dt.Rows[i]["REG_ENQ_ID"]);
                        rt.RESTAURANT_NAME = (dt.Rows[i]["RESTAURANT_NAME"].ToString());
                        rt.OWNER_NAME = (dt.Rows[i]["OWNER_NAME"].ToString());
                        rt.EMAIL = (dt.Rows[i]["EMAIL"].ToString());
                        rt.MOBILE_NO = (dt.Rows[i]["MOBILE_NO"].ToString());
                        rt.FOOD_TYPE = (dt.Rows[i]["FOOD_TYPE"].ToString());
                        rt.DESCRIPTION =(dt.Rows[i]["DESCRIPTION"].ToString());
                        if(DateTime.TryParseExact(dt.Rows[i]["OPEN_TIME"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out DateTime openDateTime))
                        {
                            rt.OPEN_TIME = openDateTime.ToString("hh:mm tt");
                        }
                        if (DateTime.TryParseExact(dt.Rows[i]["CLOSE_TIME"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out DateTime closeDateTime))
                        {
                            rt.CLOSE_TIME = closeDateTime.ToString("hh:mm tt");
                        }
                        rt.ADDRESS = (dt.Rows[i]["ADDRESS"].ToString());
                        rt.PINCODE = (dt.Rows[i]["PINCODE"].ToString());
                        rt.TERM_POLICY_AGREE = (dt.Rows[i]["TERM_POLICY_AGREE"].ToString());
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