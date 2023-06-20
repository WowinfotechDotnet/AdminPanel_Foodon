using FoodOnAdmin.Models;
using FoodOnAdmin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Hosting;

namespace FoodOnAdmin.Controllers
{
    public class DeliveryBoyMasterController : Controller
    {
        private DB_FoodOnLinkEntities db = new DB_FoodOnLinkEntities();
        public static string connectionString = ConfigurationManager.ConnectionStrings["DB_FoodOnLink"].ConnectionString;
        public static SqlConnection con = new SqlConnection(connectionString);
        static SqlCommand cmd;
        static SqlDataAdapter sda;

        static DataTable dt;

        // GET: DeliveryBoyMaster
        public ActionResult Index()
        {
            return View();
        }

        public class Search_Admin
        {
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public string FARMER_NAME { get; set; }
        }

        public JsonResult TotalRecordCount(Search_Admin tB_Employee)
        {
            int i = 0;
            try
            {
                cmd = new SqlCommand("Get_Employee_Count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FARMER_NAME", tB_Employee.FARMER_NAME);
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

        public JsonResult GetallEmployee(Search_Admin tB_Employee)
        {
            cmd = new SqlCommand("Get_AllEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ADMIN_ID", 1);
            cmd.Parameters.AddWithValue("@PageSize", tB_Employee.PageSize);
            cmd.Parameters.AddWithValue("@PageNo", tB_Employee.PageNo - 1);
            cmd.Parameters.AddWithValue("@FARMER_NAME", tB_Employee.FARMER_NAME);
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            con.Close();
            Employee rt;
            List<Employee> FinalreportList = new List<Employee>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new Employee();
                    try
                    {
                        rt.EMP_ID = Convert.ToInt64(dt.Rows[i]["EMP_ID"]);
                        rt.EMP_NAME = (dt.Rows[i]["EMP_NAME"].ToString());
                        rt.MOBILE_NO = (dt.Rows[i]["MOBILE_NO"].ToString());
                        rt.EMAIL = (dt.Rows[i]["EMAIL"].ToString());
                        rt.ADDRESS = (dt.Rows[i]["ADDRESS"].ToString());
                        rt.AreaOfWork = (dt.Rows[i]["AreaOfWork"].ToString());
                        rt.AADHAAR = (dt.Rows[i]["AADHAAR"].ToString());
                        rt.PAN = (dt.Rows[i]["PAN"].ToString());
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

        public ActionResult AddEmployee(Employee tB_Employee)
        {
            if (tB_Employee.EMAIL == null)
            {
                tB_Employee.EMAIL = "";
            }
            try
            {
                cmd = new SqlCommand("INSERT_DELIVERYBOY", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMP_NAME", tB_Employee.EMP_NAME);
                cmd.Parameters.AddWithValue("@MOBILE_NO", tB_Employee.MOBILE_NO);
                cmd.Parameters.AddWithValue("@PASSWORD", tB_Employee.PASSWORD);
                cmd.Parameters.AddWithValue("@ADDRESS", tB_Employee.ADDRESS);
                cmd.Parameters.AddWithValue("@EMAIL", tB_Employee.EMAIL);
                cmd.Parameters.AddWithValue("@VEHICAL_ID", tB_Employee.VEHICAL_ID);
                cmd.Parameters.AddWithValue("@AreaOfWork", tB_Employee.AreaOfWork);
                cmd.Parameters.AddWithValue("@AADHAAR", tB_Employee.AADHAAR);
                cmd.Parameters.AddWithValue("@PAN", tB_Employee.PAN);

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


        public ActionResult EditEmployee(Employee tB_Employee)
        {
            if (tB_Employee.EMAIL == null)
            {
                tB_Employee.EMAIL = "";
            }
            try
            {
                cmd = new SqlCommand("UPDATE_DELIVERYBOY", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMP_ID", tB_Employee.EMP_ID);
                cmd.Parameters.AddWithValue("@EMP_NAME", tB_Employee.EMP_NAME);
                cmd.Parameters.AddWithValue("@MOBILE_NO", tB_Employee.MOBILE_NO);
                cmd.Parameters.AddWithValue("@PASSWORD", tB_Employee.PASSWORD);
                cmd.Parameters.AddWithValue("@ADDRESS", tB_Employee.ADDRESS);
                cmd.Parameters.AddWithValue("@EMAIL", tB_Employee.EMAIL);
                cmd.Parameters.AddWithValue("@VEHICAL_ID", tB_Employee.VEHICAL_ID);
                cmd.Parameters.AddWithValue("@AreaOfWork", tB_Employee.AreaOfWork);
                cmd.Parameters.AddWithValue("@AADHAAR", tB_Employee.AADHAAR);
                cmd.Parameters.AddWithValue("@PAN", tB_Employee.PAN);
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

        public JsonResult GetEmployeeById(int id)
        {
            var _getEmployee = db.TB_EMPLOYEE.Where(z => z.EMP_ID == id).Select(s => new { s.EMP_ID, s.EMP_NAME, s.MOBILE_NO, s.VEHICAL_ID, s.EMAIL, s.STATUS, s.REG_DATE, s.ADDRESS, s.PASSWORD, s.AreaOfWork, s.AADHAAR, s.PAN }).FirstOrDefault();
            return Json(_getEmployee, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVehicleList()
        {
            var _getVehical = db.TB_VehicalMaster.Where(a => a.STATUS == "ACTIVE").Select(s => new { s.VEHICAL_ID, s.VEHICAL_NAME }).OrderBy(a => a.VEHICAL_NAME).ToList();
            return Json(_getVehical, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadDocument(Employee tB_Employee)
        {
            //Aadhaar Card Upload
            try
            {
                string OTP = Master.RandomString(4);
                if (tB_Employee.AADHAAR == "Yes")
                {
                    string fileName = tB_Employee.ImageName + OTP;
                    string extension = tB_Employee.ImageExtension;
                    //fileName = "Adhar" + OTP + extension;
                    string fileName1 = fileName;
                    tB_Employee.AADHAAR = Master.serverurl + "/DocunmentsUpload/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/DocunmentsUpload/"), fileName);

                    if (tB_Employee.AADHAAR != string.Empty)
                    {
                        byte[] imageByteData = Convert.FromBase64String(tB_Employee.ImageBase64Data);
                        System.IO.File.WriteAllBytes(HostingEnvironment.MapPath("~/DocunmentsUpload/" + fileName1 + extension), imageByteData);
                        tB_Employee.AADHAAR = Master.serverurl + "/DocunmentsUpload/" + fileName1 + extension;
                        
                    }
                }
                else
                {
                    if (tB_Employee.PROJECT_ACTION == "Edit")
                    {

                    }
                    else
                    {
                        tB_Employee.AADHAAR = "";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            //PAN card upload
            try
            {
                string OTP = Master.RandomString(4);
                if (tB_Employee.PAN == "Yes")
                {
                    string fileName = tB_Employee.PancardImageName + OTP;
                    string extension = tB_Employee.PancardImageExtension;
                    //fileName = "Pancard" + OTP + extension;
                    string fileName1 = fileName;
                    tB_Employee.PAN = Master.serverurl + "/DocunmentsUpload/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/DocunmentsUpload/"), fileName);

                    if (tB_Employee.PAN != string.Empty)
                    {
                        byte[] imageByteData = Convert.FromBase64String(tB_Employee.PancardImageBase64Data);
                        System.IO.File.WriteAllBytes(HostingEnvironment.MapPath("~/DocunmentsUpload/" + fileName1 + extension), imageByteData);
                        tB_Employee.PAN = Master.serverurl + "/DocunmentsUpload/" + fileName1 + extension;
                    }
                }
                else
                {
                    if (tB_Employee.PROJECT_ACTION == "Edit")
                    {

                    }
                    else
                    {
                        tB_Employee.PAN = "";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            //try
            //{
                cmd = new SqlCommand("UPLOAD_TB_EMPLOYEE_DOCUMENT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMPLOYEE_ID", tB_Employee.EMP_ID);
                cmd.Parameters.AddWithValue("@ADHARCARD_COPY", tB_Employee.AADHAAR);
                cmd.Parameters.AddWithValue("@PANCARD_COPY", tB_Employee.PAN);
                cmd.Parameters.AddWithValue("@DOCUMENT_TYPE", tB_Employee.DOCUMENT_TYPE);
                cmd.Connection = con;
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Close();
                Employee rt;
                List<Employee> FinalreportList = new List<Employee>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rt = new Employee();
                        try
                        {
                            rt.EMP_ID = Convert.ToInt64(dt.Rows[i]["EMP_ID"]);
                            rt.EMP_NAME = (dt.Rows[i]["EMP_NAME"].ToString());
                            rt.MOBILE_NO = (dt.Rows[i]["MOBILE_NO"].ToString());
                            rt.EMAIL = (dt.Rows[i]["EMAIL"].ToString());
                            rt.ADDRESS = (dt.Rows[i]["ADDRESS"].ToString());
                            rt.AreaOfWork = (dt.Rows[i]["AreaOfWork"].ToString());
                            rt.AADHAAR = (dt.Rows[i]["AADHAAR"].ToString());
                            rt.PAN = (dt.Rows[i]["PAN"].ToString());
                            rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
                            rt.REG_DATE = Convert.ToDateTime(dt.Rows[i]["REG_DATE"]).ToString("dd/MM/yyyy");
                        }
                        catch (Exception ex)
                        {
                        }
                        FinalreportList.Add(rt);
                    }

                }
            //}
            //catch (Exception ex)
            //{
            //}
            var _Monthlyreport = FinalreportList;
            return Json(_Monthlyreport, JsonRequestBehavior.AllowGet);




            //    if (con.State == System.Data.ConnectionState.Open)
            //    {
            //        con.Close();
            //    }
            //    con.Open();
            //    int i = Convert.ToInt32(cmd.ExecuteScalar());
            //    con.Close();
            //    if (i == -1)
            //    {
            //        return Json(new { success = i });
            //    }

            //    else
            //    {
            //        return Json(new { success = i }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            //return View("Index");

        }

        public string ChangeStatus(long id)
        {
            TB_EMPLOYEE tB_EMPLOYEE = db.TB_EMPLOYEE.Where(b => b.EMP_ID == id).SingleOrDefault();
            if (tB_EMPLOYEE.STATUS == "Active")
            {
                tB_EMPLOYEE.STATUS = "Deactive";
                db.SaveChanges();
            }
            else
            {
                tB_EMPLOYEE.STATUS = "Active";
                db.SaveChanges();
            }
            return "Status change Successfully.";
        }


    }
}