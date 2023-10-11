using FoodOnAdmin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOnAdmin.Controllers
{
    [VerifyUserAttribute]
    public class PackageController : Controller
    {
        private DB_FoodOnLinkEntities db = new DB_FoodOnLinkEntities();
        public static string connectionString = ConfigurationManager.ConnectionStrings["DB_FoodOnLink"].ConnectionString;
        public static SqlConnection con = new SqlConnection(connectionString);
        static SqlCommand cmd;
        static SqlDataAdapter sda;

        static DataTable dt;

        // GET: Package
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
                cmd = new SqlCommand("Get_Package_Count", con);
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
            cmd = new SqlCommand("Panel_GetPackageDetails", con);
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
            Package rt;
            List<Package> FinalreportList = new List<Package>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new Package();
                    try
                    {
                        rt.P_ID = Convert.ToInt64(dt.Rows[i]["P_ID"]);
                        rt.PACKAGE_NAME = (dt.Rows[i]["PACKAGE_NAME"].ToString());
                        rt.PACKAGE_DESCRIPTION = (dt.Rows[i]["PACKAGE_DESCRIPTION"].ToString());
                        rt.MRP = Convert.ToDecimal(dt.Rows[i]["MRP"]);
                        rt.OFFER_PRICE = Convert.ToDecimal(dt.Rows[i]["OFFER_PRICE"]);
                        rt.PACKAGE_VALIDITY = Convert.ToInt64(dt.Rows[i]["PACKAGE_VALIDITY"]);
                        rt.POST_COUNT = Convert.ToInt32(dt.Rows[i]["POST_COUNT"]);
                        rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
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


        public JsonResult GetPackageById(long id)
        {
            var _getadmin = db.TB_PackageMaster.Where(z => z.P_ID == id).Select(s => new { s.P_ID, s.PACKAGE_NAME,s.PACKAGE_DESCRIPTION,s.MRP,s.OFFER_PRICE,s.PACKAGE_VALIDITY,s.POST_COUNT,s.STATUS,s.REG_DATE }).FirstOrDefault();
            return Json(_getadmin, JsonRequestBehavior.AllowGet);
        }



        public ActionResult AddAdmin(Package tB_admin)
        {
            
            try
            {
                cmd = new SqlCommand("Insert_TB_PackageMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PACKAGE_NAME", tB_admin.PACKAGE_NAME);
                cmd.Parameters.AddWithValue("@PACKAGE_DESCRIPTION", tB_admin.PACKAGE_DESCRIPTION);
                cmd.Parameters.AddWithValue("@MRP", tB_admin.MRP);
                cmd.Parameters.AddWithValue("@OFFER_PRICE", tB_admin.OFFER_PRICE);
                cmd.Parameters.AddWithValue("@PACKAGE_VALIDITY", tB_admin.PACKAGE_VALIDITY);
                cmd.Parameters.AddWithValue("@POST_COUNT", tB_admin.POST_COUNT);

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


        public ActionResult EditAdmin(Package tB_admin)
        {
            try
            {
                cmd = new SqlCommand("Update_TB_PackageMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PACKAGE_NAME", tB_admin.PACKAGE_NAME);
                cmd.Parameters.AddWithValue("@PACKAGE_DESCRIPTION", tB_admin.PACKAGE_DESCRIPTION);
                cmd.Parameters.AddWithValue("@MRP", tB_admin.MRP);
                cmd.Parameters.AddWithValue("@OFFER_PRICE", tB_admin.OFFER_PRICE);
                cmd.Parameters.AddWithValue("@PACKAGE_VALIDITY", tB_admin.PACKAGE_VALIDITY);
                cmd.Parameters.AddWithValue("@POST_COUNT", tB_admin.POST_COUNT);
                cmd.Parameters.AddWithValue("@P_ID", tB_admin.P_ID);
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
            TB_PackageMaster tB_Admin = db.TB_PackageMaster.Where(b => b.P_ID == id).SingleOrDefault();
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