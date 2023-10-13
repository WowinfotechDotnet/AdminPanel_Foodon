using FoodOnAdmin.Models;
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
    [VerifyUserAttribute]
    public class ProductCategoryController : Controller
    {
        private DB_FoodOnLinkEntities db = new DB_FoodOnLinkEntities();
        public static string connectionString = ConfigurationManager.ConnectionStrings["DB_FoodOnLink"].ConnectionString;
        public static SqlConnection con = new SqlConnection(connectionString);
        static SqlCommand cmd;
        static SqlDataAdapter sda;

        static DataTable dt;
        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }



        public class Search_Admin
        {
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public string CATEGORY_NAME { get; set; }
            public string START_DATE { get; set; }
            public string END_DATE { get; set; }
        }

        public JsonResult TotalRecordCount(Search_Admin tB_Admin)
        {
            int i = 0;
            try
            {
                cmd = new SqlCommand("Get_TB_ProductCategoryCount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CATEGORY_NAME", tB_Admin.CATEGORY_NAME);
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
            cmd = new SqlCommand("Panel_GetTB_ProductCategoryList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageSize", tB_Admin.PageSize);
            cmd.Parameters.AddWithValue("@PageNo", tB_Admin.PageNo - 1);
            cmd.Parameters.AddWithValue("@CATEGORY_NAME", tB_Admin.CATEGORY_NAME);
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
            ProductCategoryMaster rt;
            List<ProductCategoryMaster> FinalreportList = new List<ProductCategoryMaster>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new ProductCategoryMaster();
                    try
                    {
                        rt.P_CAT_ID = Convert.ToInt64(dt.Rows[i]["P_CAT_ID"]);
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


        public ActionResult AddUpdateProductCategory(ProductCategoryMaster tB_admin)
        {

            try
            {
                cmd = new SqlCommand("InsertUpdate_TB_ProductCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_CAT_ID", tB_admin.P_CAT_ID);
                cmd.Parameters.AddWithValue("@CATEGORY_NAME", tB_admin.CATEGORY_NAME);
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
            TB_ProductCategory tB_Admin = db.TB_ProductCategory.Where(b => b.P_CAT_ID == id).SingleOrDefault();
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