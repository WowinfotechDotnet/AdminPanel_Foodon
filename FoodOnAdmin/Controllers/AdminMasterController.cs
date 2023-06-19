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
   
    public class AdminMasterController : Controller
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
                cmd = new SqlCommand("Get_Admin_Count", con);
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
            cmd = new SqlCommand("Panel_GetAdmin", con);
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
            Admin rt;
            List<Admin> FinalreportList = new List<Admin>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new Admin();
                    try
                    {
                        rt.ADMIN_ID = Convert.ToInt64(dt.Rows[i]["ADMIN_ID"]);
                        rt.ADMIN_NAME = (dt.Rows[i]["ADMIN_NAME"].ToString());
                        rt.MOBILE_NO = (dt.Rows[i]["MOBILE_NO"].ToString());
                        rt.ADDRESS = (dt.Rows[i]["ADDRESS"].ToString());
                        rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
                        rt.ROLE_ID = Convert.ToInt64(dt.Rows[i]["ROLE_ID"]);
                        rt.REG_DATE = Convert.ToDateTime(dt.Rows[i]["REG_DATE"]).ToString("dd/MM/yyyy");
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

     

        public ActionResult AddAdmin(Admin tB_admin)
        {
            if (tB_admin.EMAIL == null)
            {
                tB_admin.EMAIL = "";
            }
            try
            {
                cmd = new SqlCommand("Insert_TB_Admin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ADMIN_NAME", tB_admin.ADMIN_NAME);
                cmd.Parameters.AddWithValue("@MOBILE_NO", tB_admin.MOBILE_NO);
                cmd.Parameters.AddWithValue("@PASSWORD", tB_admin.PASSWORD);
                cmd.Parameters.AddWithValue("@ADDRESS", tB_admin.ADDRESS);
                cmd.Parameters.AddWithValue("@ROLE_ID", 2);
                cmd.Parameters.AddWithValue("@EMAIL", tB_admin.EMAIL);

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


        public ActionResult EditAdmin(Admin tB_admin)
        {
            if (tB_admin.EMAIL == null)
            {
                tB_admin.EMAIL = "";
            }
            try
            {
                cmd = new SqlCommand("Update_TB_Admin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ADMIN_NAME", tB_admin.ADMIN_NAME);
                cmd.Parameters.AddWithValue("@MOBILE_NO", tB_admin.MOBILE_NO);
                cmd.Parameters.AddWithValue("@PASSWORD", tB_admin.PASSWORD);
                cmd.Parameters.AddWithValue("@ADDRESS", tB_admin.ADDRESS);
                cmd.Parameters.AddWithValue("@ROLE_ID", tB_admin.ROLE_ID);
                cmd.Parameters.AddWithValue("@EMAIL", tB_admin.EMAIL);
                cmd.Parameters.AddWithValue("@ADMIN_ID", tB_admin.ADMIN_ID);
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
            TB_AdminMaster tB_Admin = db.TB_AdminMaster.Where(b => b.ADMIN_ID == id).SingleOrDefault();
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


        public ActionResult AdminProfile(long id)
        {
            Session["id"] = id;
            return View();
        }

        public JsonResult GetAdminProfile()
        {
            long id = Convert.ToInt64(Session["id"]);
            var _Driver = db.TB_AdminMaster.Where(a => a.ADMIN_ID == id).Select(s => new { s.ADMIN_ID, s.ADMIN_NAME, s.PASSWORD, s.MOBILE_NO, s.ADDRESS, s.EMAIL, s.STATUS, s.REG_DATE, s.ROLE_ID }).FirstOrDefault();
            //Session["PASSWORD"] = _Driver.PASSWORD;
            return Json(_Driver, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult EditProfile(Admin tB_Admin)
        {
            int i = 0;
            if (tB_Admin.EMAIL == null)
            {
                tB_Admin.EMAIL = "";

            }
            try
            {
                cmd = new SqlCommand("Update_TB_Admin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ADMIN_NAME", tB_Admin.ADMIN_NAME);
                cmd.Parameters.AddWithValue("@MOBILE_NO", tB_Admin.MOBILE_NO);
                cmd.Parameters.AddWithValue("@PASSWORD", tB_Admin.PASSWORD);
                cmd.Parameters.AddWithValue("@ADDRESS", tB_Admin.ADDRESS);
                cmd.Parameters.AddWithValue("@ROLE_ID", tB_Admin.ROLE_ID);
                cmd.Parameters.AddWithValue("@EMAIL", tB_Admin.EMAIL);
                cmd.Parameters.AddWithValue("@ADMIN_ID", tB_Admin.ADMIN_ID);
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
            }
            return Json(new { success = i });
        }


      

    }
}