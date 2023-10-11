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
    [VerifyUserAttribute]
    public class FoodOnMasterVideoController : Controller
    {
        private DB_FoodOnLinkEntities db = new DB_FoodOnLinkEntities();
        public static string connectionString = ConfigurationManager.ConnectionStrings["DB_FoodOnLink"].ConnectionString;
        public static SqlConnection con = new SqlConnection(connectionString);
        static SqlCommand cmd;
        static SqlDataAdapter sda;

        static DataTable dt;
        // GET: FoodOnMasterVideo
        public ActionResult Index()
        {
            return View();
        }



        public class Search_Admin
        {
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public string VIDEO_NAME { get; set; }
            public int FOOD_CATEGORY_ID { get; set; }
            public string START_DATE { get; set; }
            public string END_DATE { get; set; }
        }

        public JsonResult TotalRecordCount(Search_Admin tB_Admin)
        {
            int i = 0;
            try
            {
                cmd = new SqlCommand("Get_FoodOnMasterVideoURLCount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VIDEO_NAME", tB_Admin.VIDEO_NAME);
                cmd.Parameters.AddWithValue("@FOOD_CATEGORY_ID", tB_Admin.FOOD_CATEGORY_ID);
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
            cmd = new SqlCommand("Panel_GetFoodOnMasterVideoURLDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageSize", tB_Admin.PageSize);
            cmd.Parameters.AddWithValue("@PageNo", tB_Admin.PageNo - 1);
            cmd.Parameters.AddWithValue("@VIDEO_NAME", tB_Admin.VIDEO_NAME);
            cmd.Parameters.AddWithValue("@FOOD_CATEGORY_ID", tB_Admin.FOOD_CATEGORY_ID);
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
            FoodOnVideoURLMaster rt;
            List<FoodOnVideoURLMaster> FinalreportList = new List<FoodOnVideoURLMaster>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new FoodOnVideoURLMaster();
                    try
                    {
                        rt.VID_BANNER_ID = Convert.ToInt64(dt.Rows[i]["VID_BANNER_ID"]);
                        rt.VIDEO_URL_LINK = (dt.Rows[i]["VIDEO_URL_LINK"].ToString());
                        rt.VIDEO_NAME = (dt.Rows[i]["VIDEO_NAME"].ToString());
                        rt.FOOD_CATEGORY_ID = dt.Rows[i]["FOOD_CATEGORY_ID"] is DBNull ? (int?)null : Convert.ToInt32(dt.Rows[i]["FOOD_CATEGORY_ID"]);
                        rt.CATEGORY_NAME = (dt.Rows[i]["CATEGORY_NAME"].ToString());
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


        public ActionResult AddUpdateFoodOnVideoURLs(FoodOnVideoURLMaster tB_admin)
        {

            try
            {
                cmd = new SqlCommand("InsertUpdate_FoodOnMasterVideoURL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VID_BANNER_ID", tB_admin.VID_BANNER_ID);
                cmd.Parameters.AddWithValue("@FOOD_CATEGORY_ID", tB_admin.FOOD_CATEGORY_ID);
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
            TB_MasterPage_Video_Banner_Image tB_Admin = db.TB_MasterPage_Video_Banner_Image.Where(b => b.VID_BANNER_ID == id).SingleOrDefault();
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