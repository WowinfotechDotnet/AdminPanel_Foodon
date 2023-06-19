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

namespace FoodOnAdmin.Controllers
{

    public class UserMasterController : Controller
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
                cmd = new SqlCommand("Get_User_Count", con);
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
            cmd = new SqlCommand("Panel_GetUser", con);
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
            UserMaster rt;
            List<UserMaster> FinalreportList = new List<UserMaster>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new UserMaster();
                    try
                    {
                        rt.USER_ID = Convert.ToInt64(dt.Rows[i]["USER_ID"]);
                        rt.USER_NAME = (dt.Rows[i]["USER_NAME"].ToString());
                        rt.MOBILE_NUMBER = (dt.Rows[i]["MOBILE_NUMBER"].ToString());
                        rt.ADDRESS = (dt.Rows[i]["ADDRESS"].ToString());
                        rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
                        rt.EMAIL_ID = (dt.Rows[i]["EMAIL_ID"]).ToString();
                        rt.PROFILE_PHOTO = (dt.Rows[i]["PROFILE_PHOTO"]).ToString();
                       
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
            var _getadmin = db.TB_AdminMaster.Where(z => z.ADMIN_ID == id).Select(s => new { s.ADMIN_ID, s.ADMIN_NAME, s.MOBILE_NO, s.ROLE_ID, s.EMAIL, s.STATUS, s.REG_DATE, s.ADDRESS, s.PASSWORD }).FirstOrDefault();
            return Json(_getadmin, JsonRequestBehavior.AllowGet);
        }



    

        public string ChangeStatus(long id)
        {
            TB_UserMaster tB_Admin = db.TB_UserMaster.Where(b => b.USER_ID == id).SingleOrDefault();
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