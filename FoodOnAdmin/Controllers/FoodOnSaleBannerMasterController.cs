using FoodOnAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Hosting;

namespace FoodOnAdmin.Controllers
{
    public class FoodOnSaleBannerMasterController : Controller
    {
        private DB_FoodOnLinkEntities db = new DB_FoodOnLinkEntities();
        public static string connectionString = ConfigurationManager.ConnectionStrings["DB_FoodOnLink"].ConnectionString;
        public static SqlConnection con = new SqlConnection(connectionString);
        static SqlCommand cmd;
        static SqlDataAdapter sda;

        static DataTable dt;
        // GET: FoodOnSaleBannerMaster
        public ActionResult Index()
        {
            return View();
        }

        public class Search_Admin
        {
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public string BANNER_NAME { get; set; }
            public int FOOD_CATEGORY_ID { get; set; }
            public string START_DATE { get; set; }
            public string END_DATE { get; set; }
        }

        public JsonResult TotalRecordCount(Search_Admin tB_Admin)
        {
            int i = 0;
            try
            {
                cmd = new SqlCommand("Get_FoodOnSaleBannerCount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BANNER_NAME", tB_Admin.BANNER_NAME);
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
            cmd = new SqlCommand("Panel_GetFoodOnSaleBannerDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageSize", tB_Admin.PageSize);
            cmd.Parameters.AddWithValue("@PageNo", tB_Admin.PageNo - 1);
            cmd.Parameters.AddWithValue("@BANNER_NAME", tB_Admin.BANNER_NAME);
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
            FoodOnSaleBannerMaster rt;
            List<FoodOnSaleBannerMaster> FinalreportList = new List<FoodOnSaleBannerMaster>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new FoodOnSaleBannerMaster();
                    try
                    {
                        rt.BANNER_ID = Convert.ToInt64(dt.Rows[i]["BANNER_ID"]);
                        rt.FOOD_CATEGORY_ID = dt.Rows[i]["FOOD_CATEGORY_ID"] is DBNull ? (int?)null : Convert.ToInt32(dt.Rows[i]["FOOD_CATEGORY_ID"]);
                        rt.BANNER_URL = (dt.Rows[i]["BANNER_URL"].ToString()); ;
                        rt.BANNER_NAME = (dt.Rows[i]["BANNER_NAME"].ToString());
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


        public ActionResult AddUpdateFoodOnSaleBanner(FoodOnSaleBannerMaster tB_admin)
        {
            try
            {
                string OTP = Master.RandomString(6);
                if (tB_admin.BANNER_URL == "Yes")
                {
                    string fileName = tB_admin.ImageName;
                    string extension = tB_admin.ImageExtension;
                    fileName = "FoodOnSaleBanner" + OTP + DateTime.Now.ToString("ddmmyyyy") + extension;
                    string fileName1 = fileName;
                    tB_admin.BANNER_URL = Master.serverurl + "/UploadedDocuments/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadedDocuments/"), fileName);

                    if (tB_admin.BANNER_URL != string.Empty)
                    {
                        byte[] imageByteData = Convert.FromBase64String(tB_admin.ImageBase64Data);
                        MemoryStream mem = new MemoryStream(imageByteData);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(mem);
                        img.Save(HostingEnvironment.MapPath("~/UploadedDocuments/" + fileName1), ImageFormat.Jpeg);
                    }
                }
                else
                {
                    tB_admin.BANNER_URL = null;
                }
            }
            catch (Exception ex)
            {
            }

            try
            {
                cmd = new SqlCommand("InsertUpdate_FoodOnSaleBannerMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BANNER_ID", tB_admin.BANNER_ID);
                cmd.Parameters.AddWithValue("@FOOD_CATEGORY_ID", tB_admin.FOOD_CATEGORY_ID);
                cmd.Parameters.AddWithValue("@BANNER_NAME", tB_admin.BANNER_NAME);
                cmd.Parameters.AddWithValue("@BANNER_URL", tB_admin.BANNER_URL);
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

        public JsonResult GetAllFoodCategory()
        {
            var _getadmin = db.TB_FOOD_CATEGORY.Where(z => z.STATUS == "Active").Select(s => new { s.CATEGORY_ID, s.CATEGORY_NAME, s.STATUS, s.REG_DATE}).ToList();
            return Json(_getadmin, JsonRequestBehavior.AllowGet);
        }


        public string ChangeStatus(long id)
        {
            TB_FoodSale_Banner tB_Admin = db.TB_FoodSale_Banner.Where(b => b.BANNER_ID == id).SingleOrDefault();
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