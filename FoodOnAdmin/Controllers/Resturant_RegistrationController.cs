﻿using FoodOnAdmin.Models;
using FoodOnAdmin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Hosting;
using System.Drawing.Imaging;

namespace FoodOnAdmin.Controllers
{
    [VerifyUserAttribute]
    public class Resturant_RegistrationController : Controller
    {
        private DB_FoodOnLinkEntities db = new DB_FoodOnLinkEntities();
        public static string connectionString = ConfigurationManager.ConnectionStrings["DB_FoodOnLink"].ConnectionString;
        public static SqlConnection con = new SqlConnection(connectionString);
        static SqlCommand cmd;
        static SqlDataAdapter sda;
        static DataSet ds;
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
                cmd = new SqlCommand("Get_Hotel_Count", con);
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
            cmd = new SqlCommand("Panel_GetHotel", con);
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
            Hotel rt;
            List<Hotel> FinalreportList = new List<Hotel>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new Hotel();
                    try
                    {
                        rt.RES_ID = Convert.ToInt64(dt.Rows[i]["RES_ID"]);
                        rt.RES_NAME = (dt.Rows[i]["RES_NAME"].ToString());
                        rt.OWNER_NAME = (dt.Rows[i]["OWNER_NAME"].ToString());
                        rt.MOBILE_NUMBER = (dt.Rows[i]["MOBILE_NUMBER"].ToString());
                        rt.FOOD_TYPE = (dt.Rows[i]["FOOD_TYPE"]).ToString();
                        rt.RES_LOGO = (dt.Rows[i]["RES_LOGO"]).ToString();
                        rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
                        rt.ADDRESS = (dt.Rows[i]["ADDRESS"]).ToString();
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
            var _getadmin = db.TB_Restaurant.Where(z => z.RES_ID == id).Select(s => new { s.RES_ID, s.RES_NAME, s.MOBILE_NUMBER, s.FOOD_TYPE, s.PINCODE, s.STATUS, s.REG_DATE, s.ADDRESS, s.PASSWORD, s.CONFIRM_PASSWORD, s.LATITUDE, s.LONGITUDE, s.RES_OPEN_TIME, s.RES_CLOSE_TIME, s.DESCRIPTION, s.OWNER_NAME, s.RES_LOGO, s.Food, s.Sweets, s.Juice, s.Cafeteria, s.RES_RATING, s.RESTRO_BOOKING, s.FOOD_DELIVERY, s.CATEGORY }).FirstOrDefault();
            return Json(_getadmin, JsonRequestBehavior.AllowGet);
        }



        public ActionResult AddAdmin(Hotel tB_admin)
        {


            //try
            //{
            //    string OTP = Master.RandomString(6);

            //    if (tB_admin.RES_LOGO == "Yes")
            //    {
            //        string fileName = tB_admin.ImageName;
            //        string extension = tB_admin.ImageExtension;
            //        fileName = "Shop" + OTP + DateTime.Now.ToString("ddmmyyyy") + extension;
            //        string fileName1 = fileName;
            //        tB_admin.RES_LOGO = Master.serverurl + "/Images/" + fileName;
            //        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);

            //        if (tB_admin.RES_LOGO != string.Empty)
            //        {
            //            byte[] imageByteData = Convert.FromBase64String(tB_admin.ImageBase64Data);
            //            MemoryStream mem = new MemoryStream(imageByteData);
            //            System.Drawing.Image img = System.Drawing.Image.FromStream(mem);
            //            img.Save(HostingEnvironment.MapPath("~/Images/" + fileName1), ImageFormat.Jpeg);
            //        }
            //    }
            //    else
            //    {
            //        tB_admin.RES_LOGO = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

            try
            {
                cmd = new SqlCommand("Insert_TB_Hotel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RES_NAME", tB_admin.RES_NAME);
                cmd.Parameters.AddWithValue("@MOBILE_NUMBER", tB_admin.MOBILE_NUMBER);
                cmd.Parameters.AddWithValue("@OWNER_NAME", tB_admin.OWNER_NAME);
                cmd.Parameters.AddWithValue("@PASSWORD", tB_admin.PASSWORD);
                cmd.Parameters.AddWithValue("@ADDRESS", tB_admin.ADDRESS);
                cmd.Parameters.AddWithValue("@FOOD_TYPE", tB_admin.FOOD_TYPE);
                cmd.Parameters.AddWithValue("@CONFIRM_PASSWORD", tB_admin.CONFIRM_PASSWORD);
                cmd.Parameters.AddWithValue("@RES_LOGO", tB_admin.RES_LOGO);
                cmd.Parameters.AddWithValue("@RES_OPEN_TIME", tB_admin.RES_OPEN_TIME);
                cmd.Parameters.AddWithValue("@RES_CLOSE_TIME", tB_admin.RES_CLOSE_TIME);
                cmd.Parameters.AddWithValue("@LATITUDE", tB_admin.LATITUDE);
                cmd.Parameters.AddWithValue("@LONGITUDE", tB_admin.LONGITUDE);
                cmd.Parameters.AddWithValue("@PINCODE", tB_admin.PINCODE);
                cmd.Parameters.AddWithValue("@DESCRIPTION", tB_admin.DESCRIPTION);
                cmd.Parameters.AddWithValue("@FOODON_RATING", tB_admin.FOODON_RATING);
                cmd.Parameters.AddWithValue("@RESTRO_BOOKING", tB_admin.RESTRO_BOOKING);
                cmd.Parameters.AddWithValue("@FOOD_DELIVERY", tB_admin.FOOD_DELIVERY);
                cmd.Parameters.AddWithValue("@Food", tB_admin.Food);
                cmd.Parameters.AddWithValue("@Sweets", tB_admin.Sweets);
                cmd.Parameters.AddWithValue("@Juice", tB_admin.Juice);
                cmd.Parameters.AddWithValue("@Cafeteria", tB_admin.Cafeteria);
                cmd.Parameters.AddWithValue("@CATEGORY", tB_admin.CATEGORY);
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


        public ActionResult EditAdmin(Hotel tB_admin)
        {

            //try
            //{
            //    string OTP = Master.RandomString(6);
            //    if (tB_admin.RES_LOGO == "Yes")
            //    {
            //        string fileName = tB_admin.ImageName;
            //        string extension = tB_admin.ImageExtension;
            //        fileName = "Shop" + OTP + DateTime.Now.ToString("ddmmyyyy") + extension;
            //        string fileName1 = fileName;
            //        tB_admin.RES_LOGO = Master.serverurl + "/Images/" + fileName;
            //        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);

            //        if (tB_admin.RES_LOGO != string.Empty)
            //        {
            //            byte[] imageByteData = Convert.FromBase64String(tB_admin.ImageBase64Data);
            //            MemoryStream mem = new MemoryStream(imageByteData);
            //            System.Drawing.Image img = System.Drawing.Image.FromStream(mem);
            //            img.Save(HostingEnvironment.MapPath("~/Images/" + fileName1), ImageFormat.Jpeg);
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{

            //}

            try
            {
                cmd = new SqlCommand("Update_TB_Hotel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RES_NAME", tB_admin.RES_NAME);
                cmd.Parameters.AddWithValue("@MOBILE_NUMBER", tB_admin.MOBILE_NUMBER);
                cmd.Parameters.AddWithValue("@OWNER_NAME", tB_admin.OWNER_NAME);
                cmd.Parameters.AddWithValue("@PASSWORD", tB_admin.PASSWORD);
                cmd.Parameters.AddWithValue("@ADDRESS", tB_admin.ADDRESS);
                cmd.Parameters.AddWithValue("@FOOD_TYPE", tB_admin.FOOD_TYPE);
                cmd.Parameters.AddWithValue("@CONFIRM_PASSWORD", tB_admin.CONFIRM_PASSWORD);
                cmd.Parameters.AddWithValue("@RES_LOGO", tB_admin.RES_LOGO);
                cmd.Parameters.AddWithValue("@RES_OPEN_TIME", tB_admin.RES_OPEN_TIME);
                cmd.Parameters.AddWithValue("@RES_CLOSE_TIME", tB_admin.RES_CLOSE_TIME);
                cmd.Parameters.AddWithValue("@LATITUDE", tB_admin.LATITUDE);
                cmd.Parameters.AddWithValue("@LONGITUDE", tB_admin.LONGITUDE);
                cmd.Parameters.AddWithValue("@PINCODE", tB_admin.PINCODE);
                cmd.Parameters.AddWithValue("@DESCRIPTION", tB_admin.DESCRIPTION);
                cmd.Parameters.AddWithValue("@FOODON_RATING", tB_admin.FOODON_RATING);
                cmd.Parameters.AddWithValue("@RESTRO_BOOKING", tB_admin.RESTRO_BOOKING);
                cmd.Parameters.AddWithValue("@FOOD_DELIVERY", tB_admin.FOOD_DELIVERY);
                cmd.Parameters.AddWithValue("@Food", tB_admin.Food);
                cmd.Parameters.AddWithValue("@Sweets", tB_admin.Sweets);
                cmd.Parameters.AddWithValue("@Juice", tB_admin.Juice);
                cmd.Parameters.AddWithValue("@Cafeteria", tB_admin.Cafeteria);
                cmd.Parameters.AddWithValue("@CATEGORY", tB_admin.CATEGORY);
                cmd.Parameters.AddWithValue("@RES_ID", tB_admin.RES_ID);
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
            TB_Restaurant tB_Admin = db.TB_Restaurant.Where(b => b.RES_ID == id).SingleOrDefault();
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



        public ActionResult Details(long id)
        {
            Session["SHOP_ID"] = id;
            return View();
        }


        public JsonResult GetallShopdetails()
        {
            long id = Convert.ToInt64(Session["SHOP_ID"]);
            var _getadmin = db.TB_Restaurant.Where(z => z.RES_ID == id).Select(s => new { s.RES_ID, s.RES_NAME, s.MOBILE_NUMBER, s.FOOD_TYPE, s.PINCODE, s.STATUS, s.REG_DATE, s.ADDRESS, s.PASSWORD, s.CONFIRM_PASSWORD, s.LATITUDE, s.LONGITUDE, s.RES_OPEN_TIME, s.RES_CLOSE_TIME, s.DESCRIPTION, s.OWNER_NAME, s.RES_LOGO, s.Food, s.Sweets, s.Juice, s.Cafeteria }).ToList();
            return Json(_getadmin, JsonRequestBehavior.AllowGet);
        }




        public JsonResult GetallHotelImages(string id)
        {
            long id1 = Convert.ToInt64(Session["SHOP_ID"]);
            cmd = new SqlCommand("Panel_GetHotelInformation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ADMIN_ID", id1);
            cmd.Parameters.AddWithValue("@R_TYPE", id);

            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            con.Close();
            Hotel rt;
            List<Hotel> FinalreportList = new List<Hotel>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new Hotel();
                    try
                    {
                        rt.RI_ID = Convert.ToInt64(dt.Rows[i]["RI_ID"]);
                        rt.R_PHOTO = (dt.Rows[i]["R_PHOTO"].ToString());
                        rt.R_TYPE = (dt.Rows[i]["R_TYPE"].ToString());
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

        #region Food Request

        public class Search_AdminRequest
        {
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public string CUSTOMER_NAME { get; set; }
            public string STATE_ID { get; set; }
            public string START_DATE { get; set; }
            public string END_DATE { get; set; }
        }

        public JsonResult TotalFoodRequestCount(Search_AdminRequest tB_Admin)
        {
            int i = 0;
            try
            {
                long resId = Convert.ToInt64(Session["SHOP_ID"]);
                cmd = new SqlCommand("Get_TotalFoodRequest_Count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CUSTOMER_NAME", tB_Admin.CUSTOMER_NAME);
                cmd.Parameters.AddWithValue("@RES_ID", resId);
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


        public JsonResult GetAllTotalFoodRequest(Search_AdminRequest tB_Admin)
        {
            try
            {
                long resId = Convert.ToInt64(Session["SHOP_ID"]);
                cmd = new SqlCommand("Get_TotalFoodRequest_Panel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageSize", tB_Admin.PageSize);
                cmd.Parameters.AddWithValue("@PageNo", tB_Admin.PageNo - 1);
                cmd.Parameters.AddWithValue("@CUSTOMER_NAME", tB_Admin.CUSTOMER_NAME);
                cmd.Parameters.AddWithValue("@RES_ID", resId);
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
                FoodRequestMaster rt;

                List<FoodRequestMaster> FinalreportList = new List<FoodRequestMaster>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rt = new FoodRequestMaster();
                        try
                        {
                            rt.ORDER_ID = Convert.ToInt64(dt.Rows[i]["ORDER_ID"]);
                            rt.USER_ID = Convert.ToInt64(dt.Rows[i]["USER_ID"]);
                            rt.ORDER_TYPE = (dt.Rows[i]["ORDER_TYPE"].ToString());
                            rt.USER_NAME = (dt.Rows[i]["USER_NAME"].ToString());
                            rt.MOBILE_NUMBER = (dt.Rows[i]["MOBILE_NUMBER"].ToString());
                            rt.NOTE = (dt.Rows[i]["NOTE"].ToString());
                            rt.TOTAL_AMOUNT = dt.Rows[i]["TOTAL_AMOUNT"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["TOTAL_AMOUNT"]);
                            rt.TOTAL_QUANTITY = dt.Rows[i]["TOTAL_QUANTITY"] is DBNull ? (long?)null : Convert.ToInt64(dt.Rows[i]["TOTAL_QUANTITY"]);
                            rt.ORDER_REMARK = (dt.Rows[i]["ORDER_REMARK"]).ToString();
                            rt.ORDER_RATING = dt.Rows[i]["ORDER_RATING"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["ORDER_RATING"]);
                            rt.GST = dt.Rows[i]["GST"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["GST"]);
                            rt.DISCOUNT = dt.Rows[i]["DISCOUNT"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["DISCOUNT"]);
                            rt.DELIVERY_CHARGES = dt.Rows[i]["DELIVERY_CHARGES"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["DELIVERY_CHARGES"]);
                            rt.ORDER_TIME = Convert.ToDateTime(dt.Rows[i]["REG_DATE"]).ToString("hh:mm tt");
                            rt.ADDRESS = (dt.Rows[i]["ADDRESS"]).ToString();
                            rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
                            rt.PAYMENT_STATUS = (dt.Rows[i]["PAYMENT_STATUS"]).ToString();
                            rt.DELIVERY_BOY_STATUS = (dt.Rows[i]["DELIVERY_BOY_STATUS"]).ToString();
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
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult FoodRequestDetails(long id)
        {
            Session["ORDER_ID"] = id;
            return View();
        }

        public JsonResult GetFoodRequestDetails()
        {
            try
            {
                long id = Convert.ToInt64(Session["ORDER_ID"]);
                long resId = Convert.ToInt64(Session["SHOP_ID"]);
                cmd = new SqlCommand("Get_FoodRequest_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RES_ID", resId);
                cmd.Parameters.AddWithValue("@ORDER_ID", id);
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                ds = new DataSet();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                DataTable dt_Product = new DataTable();
                dt_Product = ds.Tables[1];
                con.Close();
                FoodRequestMaster rt;
                List<FoodRequestMaster> FinalreportList = new List<FoodRequestMaster>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rt = new FoodRequestMaster();
                        try
                        {
                            rt.ORDER_ID = Convert.ToInt64(dt.Rows[i]["ORDER_ID"]);
                            rt.USER_ID = Convert.ToInt64(dt.Rows[i]["USER_ID"]);
                            rt.ORDER_TYPE = (dt.Rows[i]["ORDER_TYPE"].ToString());
                            rt.USER_NAME = (dt.Rows[i]["USER_NAME"].ToString());
                            rt.MOBILE_NUMBER = (dt.Rows[i]["MOBILE_NUMBER"].ToString());
                            rt.NOTE = (dt.Rows[i]["NOTE"].ToString());
                            rt.TOTAL_AMOUNT = dt.Rows[i]["TOTAL_AMOUNT"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["TOTAL_AMOUNT"]);
                            rt.TOTAL_QUANTITY = dt.Rows[i]["TOTAL_QUANTITY"] is DBNull ? (long?)null : Convert.ToInt64(dt.Rows[i]["TOTAL_QUANTITY"]);
                            rt.RES_REMARK = (dt.Rows[i]["RES_REMARK"]).ToString();
                            rt.ORDER_REMARK = (dt.Rows[i]["ORDER_REMARK"]).ToString();
                            rt.ORDER_RATING = dt.Rows[i]["ORDER_RATING"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["ORDER_RATING"]);
                            rt.GST = dt.Rows[i]["GST"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["GST"]);
                            rt.DISCOUNT = dt.Rows[i]["DISCOUNT"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["DISCOUNT"]);
                            rt.DELIVERY_CHARGES = dt.Rows[i]["DELIVERY_CHARGES"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["DELIVERY_CHARGES"]);
                            rt.ORDER_TIME = Convert.ToDateTime(dt.Rows[i]["REG_DATE"]).ToString("hh:mm tt");
                            rt.ADDRESS = (dt.Rows[i]["ADDRESS"]).ToString();
                            rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
                            rt.PAYMENT_STATUS = (dt.Rows[i]["PAYMENT_STATUS"]).ToString();
                            rt.DELIVERY_BOY_STATUS = (dt.Rows[i]["DELIVERY_BOY_STATUS"]).ToString();
                            rt.REG_DATE = Convert.ToDateTime(dt.Rows[i]["REG_DATE"]).ToString("dd/MM/yyyy");
                            rt.FoodRequestProductList = FoodRequestProductList(dt_Product);
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
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static List<FoodRequestCart> FoodRequestProductList(DataTable dt)
        {
            FoodRequestCart rt;
            List<FoodRequestCart> FinalProductList = new List<FoodRequestCart>();

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new FoodRequestCart();
                    try
                    {
                        rt.OD_ID = Convert.ToInt64(dt.Rows[i]["OD_ID"]);
                        rt.ORDER_ID = Convert.ToInt64(dt.Rows[i]["ORDER_ID"]);
                        rt.PRODUCT_ID = Convert.ToInt64(dt.Rows[i]["PRODUCT_ID"]);
                        rt.P_CAT_ID = Convert.ToInt64(dt.Rows[i]["P_CAT_ID"]);
                        rt.PRODUCT_NAME = (dt.Rows[i]["PRODUCT_NAME"].ToString());
                        rt.PRODUCT_TYPE = (dt.Rows[i]["PRODUCT_TYPE"].ToString());
                        rt.CATEGORY_NAME = (dt.Rows[i]["CATEGORY_NAME"].ToString());
                        rt.QUANTITY = Convert.ToInt64(dt.Rows[i]["QUANTITY"]);
                        rt.AMOUNT = dt.Rows[i]["AMOUNT"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["AMOUNT"]);

                        FinalProductList.Add(rt);

                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return FinalProductList;
        }


        #endregion

        #region Dining Enquiry

        public JsonResult TotalDinningRecordCount(Search_AdminRequest tB_Admin)
        {
            int i = 0;
            try
            {
                long resId = Convert.ToInt64(Session["SHOP_ID"]);
                cmd = new SqlCommand("Get_TotalDinning_Count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FARMER_NAME", tB_Admin.CUSTOMER_NAME);
                cmd.Parameters.AddWithValue("@STATE_ID", tB_Admin.STATE_ID);
                cmd.Parameters.AddWithValue("@RES_ID", resId);
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

        public JsonResult GetallTotalsDinningRequest(Search_AdminRequest tB_Admin)
        {
            try
            {
                long resId = Convert.ToInt64(Session["SHOP_ID"]);
                cmd = new SqlCommand("Get_TotalDinning_Panel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageSize", tB_Admin.PageSize);
                cmd.Parameters.AddWithValue("@PageNo", tB_Admin.PageNo - 1);
                cmd.Parameters.AddWithValue("@FARMER_NAME", tB_Admin.CUSTOMER_NAME);
                cmd.Parameters.AddWithValue("@STATE_ID", tB_Admin.STATE_ID);
                cmd.Parameters.AddWithValue("@RES_ID", resId);
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
                DinningMaster rt;

                List<DinningMaster> FinalreportList = new List<DinningMaster>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rt = new DinningMaster();
                        try
                        {
                            rt.DINNING_ID = Convert.ToInt64(dt.Rows[i]["DINNING_ID"]);
                            rt.USER_ID = Convert.ToInt64(dt.Rows[i]["USER_ID"]);
                            rt.TOTAL_PRICE = Convert.ToDecimal(dt.Rows[i]["TOTAL_PRICE"]);
                            rt.DINNER_TYPE = (dt.Rows[i]["DINNER_TYPE"].ToString());
                            rt.USER_NAME = (dt.Rows[i]["USER_NAME"].ToString());
                            rt.MOBILE_NUMBER = (dt.Rows[i]["MOBILE_NUMBER"].ToString());
                            rt.CHOICE = (dt.Rows[i]["CHOICE"].ToString());
                            rt.NO_OF_PEOPLE = (dt.Rows[i]["NO_OF_PEOPLE"].ToString());
                            rt.MINIMUM_BUDGET = (dt.Rows[i]["MINIMUM_BUDGET"]).ToString();
                            rt.MINIMUM_BUDGET_DRINKS = (dt.Rows[i]["MINIMUM_BUDGET_DRINKS"]).ToString();
                            rt.REMARK = (dt.Rows[i]["REMARK"]).ToString();
                            rt.SHOW_MOBILE_NUMBER = (dt.Rows[i]["SHOW_MOBILE_NUMBER"]).ToString();
                            rt.ADDRESS_TYPE = (dt.Rows[i]["ADDRESS_TYPE"]).ToString();
                            rt.ARRIVAL_TIME = Convert.ToDateTime(dt.Rows[i]["ARRIVAL_TIME"]).ToString("hh:mm tt");
                            rt.OUT_TIME = Convert.ToDateTime(dt.Rows[i]["OUT_TIME"]).ToString("hh:mm tt");
                            rt.ADDRESS = (dt.Rows[i]["ADDRESS"]).ToString();
                            rt.ADVANCED_AMT = (dt.Rows[i]["ADVANCED_AMT"]).ToString();
                            rt.PAYMENT_STATUS = (dt.Rows[i]["PAYMENT_STATUS"]).ToString();
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
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult DiningRequestDetails(long id)
        {
            Session["DINING_ID"] = id;
            return View();
        }


        public JsonResult GetDinningDetails()
        {
            try
            {
                long id = Convert.ToInt64(Session["DINING_ID"]);
                long resId = Convert.ToInt64(Session["SHOP_ID"]);
                cmd = new SqlCommand("Get_Dinning_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RES_ID", resId);
                cmd.Parameters.AddWithValue("@DINNING_ID", id);
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Close();
                DinningMaster rt;
                List<DinningMaster> FinalreportList = new List<DinningMaster>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rt = new DinningMaster();
                        try
                        {
                            rt.DINNING_ID = Convert.ToInt64(dt.Rows[i]["DINNING_ID"]);
                            rt.USER_ID = Convert.ToInt64(dt.Rows[i]["USER_ID"]);
                            rt.DINNER_TYPE = (dt.Rows[i]["DINNER_TYPE"].ToString());
                            rt.MOBILE_NUMBER = (dt.Rows[i]["MOBILE_NUMBER"].ToString());
                            rt.USER_NAME = (dt.Rows[i]["USER_NAME"].ToString());
                            rt.CHOICE = (dt.Rows[i]["CHOICE"].ToString());
                            rt.TOTAL_PRICE = Convert.ToDecimal(dt.Rows[i]["TOTAL_PRICE"]);
                            rt.TOTAL_DISCOUNT = (dt.Rows[i]["TOTAL_DISCOUNT"].ToString());
                            rt.DRINK_DISCOUNT = (dt.Rows[i]["DRINK_DISCOUNT"].ToString());
                            rt.NO_OF_PEOPLE = (dt.Rows[i]["NO_OF_PEOPLE"].ToString());
                            rt.MINIMUM_BUDGET = (dt.Rows[i]["MINIMUM_BUDGET"]).ToString();
                            rt.MINIMUM_BUDGET_DRINKS = (dt.Rows[i]["MINIMUM_BUDGET_DRINKS"]).ToString();
                            rt.REMARK = (dt.Rows[i]["REMARK"]).ToString();
                            rt.SHOW_MOBILE_NUMBER = (dt.Rows[i]["SHOW_MOBILE_NUMBER"]).ToString();
                            rt.ADDRESS_TYPE = (dt.Rows[i]["ADDRESS_TYPE"]).ToString();
                            rt.RES_REMARK = (dt.Rows[i]["RES_REMARK"]).ToString();
                            rt.ARRIVAL_TIME = Convert.ToDateTime(dt.Rows[i]["ARRIVAL_TIME"]).ToString("hh:mm tt");
                            rt.OUT_TIME = Convert.ToDateTime(dt.Rows[i]["OUT_TIME"]).ToString("hh:mm tt");
                            rt.ADDRESS = (dt.Rows[i]["ADDRESS"]).ToString();
                            rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
                            rt.ADMIN_STATUS = (dt.Rows[i]["ADMIN_STATUS"]).ToString();
                            rt.FAMILYRESTRO_TYPE = (dt.Rows[i]["FAMILYRESTRO_TYPE"]).ToString();
                            rt.ADVANCED_AMT = (dt.Rows[i]["ADVANCED_AMT"]).ToString();
                            rt.PAYMENT_AMOUNT = (dt.Rows[i]["PAYMENT_AMOUNT"]).ToString();
                            rt.PAYMENT_PHOTO = (dt.Rows[i]["PAYMENT_PHOTO"]).ToString();
                            rt.REG_DATE = Convert.ToDateTime(dt.Rows[i]["REG_DATE"]).ToString("dd/MM/yyyy");
                            rt.BOOKING_DATE = Convert.ToDateTime(dt.Rows[i]["BOOKING_DATE"]).ToString("dd/MM/yyyy");
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
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #endregion

        #region Preferred Menu

        public JsonResult TotalPreferredMenuCount(Search_AdminRequest tB_Admin)
        {
            int i = 0;
            try
            {
                long resId = Convert.ToInt64(Session["SHOP_ID"]);
                cmd = new SqlCommand("Get_TotalPreferredMenu_Count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CUSTOMER_NAME", tB_Admin.CUSTOMER_NAME);
                cmd.Parameters.AddWithValue("@RES_ID", resId);
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

        public JsonResult GetAllTotalPreferredMenu(Search_AdminRequest tB_Admin)
        {
            try
            {
                long resId = Convert.ToInt64(Session["SHOP_ID"]);
                cmd = new SqlCommand("Get_TotalPreferredMenu_Panel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageSize", tB_Admin.PageSize);
                cmd.Parameters.AddWithValue("@PageNo", tB_Admin.PageNo - 1);
                cmd.Parameters.AddWithValue("@CUSTOMER_NAME", tB_Admin.CUSTOMER_NAME);
                cmd.Parameters.AddWithValue("@RES_ID", resId);
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
                PreferredMenuMaster rt;

                List<PreferredMenuMaster> FinalreportList = new List<PreferredMenuMaster>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rt = new PreferredMenuMaster();
                        try
                        {
                            rt.PreferredMenu_ID = Convert.ToInt64(dt.Rows[i]["PreferredMenu_ID"]);
                            rt.USER_ID = Convert.ToInt64(dt.Rows[i]["USER_ID"]);
                            rt.PREFERRED_MENU = (dt.Rows[i]["PREFERRED_MENU"].ToString());
                            rt.USER_NAME = (dt.Rows[i]["USER_NAME"].ToString());
                            rt.MOBILE_NUMBER = (dt.Rows[i]["MOBILE_NUMBER"].ToString());
                            rt.SHOW_MOBILE = (dt.Rows[i]["SHOW_MOBILE"].ToString());
                            rt.TOTAL_QUANTITY = dt.Rows[i]["TOTAL_QUANTITY"] is DBNull ? (int?)null : Convert.ToInt32(dt.Rows[i]["TOTAL_QUANTITY"]);
                            rt.TOTAL_DISCOUNT = dt.Rows[i]["TOTAL_DISCOUNT"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["TOTAL_DISCOUNT"]);
                            rt.GST = dt.Rows[i]["GST"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["GST"]);
                            rt.TOTAL_AMOUNT = dt.Rows[i]["TOTAL_AMOUNT"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["TOTAL_AMOUNT"]);
                            rt.DELIVERY_CHARGES = dt.Rows[i]["DELIVERY_CHARGES"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["DELIVERY_CHARGES"]);
                            rt.REMARK = (dt.Rows[i]["REMARK"]).ToString();
                            rt.ADDRESS = (dt.Rows[i]["ADDRESS"]).ToString();
                            rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
                            rt.PAYMENT_STATUS = (dt.Rows[i]["PAYMENT_STATUS"]).ToString();
                            rt.REG_DATE = Convert.ToDateTime(dt.Rows[i]["REG_DATE"]).ToString("dd/MM/yyyy");
                            rt.ORDER_TIME = Convert.ToDateTime(dt.Rows[i]["REG_DATE"]).ToString("hh:mm tt");
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
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult PreferredMenuDetails(long id)
        {
            Session["PreferredMenu_ID"] = id;
            return View();
        }


        public JsonResult GetPreferredMenuDetails(Search_Admin tB_Admin)
        {
            try
            {
                long id = Convert.ToInt64(Session["PreferredMenu_ID"]);
                long resId = Convert.ToInt64(Session["SHOP_ID"]);
                cmd = new SqlCommand("Panel_Get_PreferredMenuDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PreferredMenu_ID", id);
                cmd.Parameters.AddWithValue("@RES_ID", resId);
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Close();
                PreferredMenuMaster rt;

                List<PreferredMenuMaster> FinalreportList = new List<PreferredMenuMaster>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rt = new PreferredMenuMaster();
                        try
                        {
                            rt.PreferredMenu_ID = Convert.ToInt64(dt.Rows[i]["PreferredMenu_ID"]);
                            rt.USER_ID = Convert.ToInt64(dt.Rows[i]["USER_ID"]);
                            rt.PREFERRED_MENU = (dt.Rows[i]["PREFERRED_MENU"].ToString());
                            rt.USER_NAME = (dt.Rows[i]["USER_NAME"].ToString());
                            rt.MOBILE_NUMBER = (dt.Rows[i]["MOBILE_NUMBER"].ToString());
                            rt.SHOW_MOBILE = (dt.Rows[i]["SHOW_MOBILE"].ToString());
                            rt.TOTAL_QUANTITY = dt.Rows[i]["TOTAL_QUANTITY"] is DBNull ? (int?)null : Convert.ToInt32(dt.Rows[i]["TOTAL_QUANTITY"]);
                            rt.TOTAL_DISCOUNT = dt.Rows[i]["TOTAL_DISCOUNT"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["TOTAL_DISCOUNT"]);
                            rt.GST = dt.Rows[i]["GST"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["GST"]);
                            rt.TOTAL_AMOUNT = dt.Rows[i]["TOTAL_AMOUNT"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["TOTAL_AMOUNT"]);
                            rt.DELIVERY_CHARGES = dt.Rows[i]["DELIVERY_CHARGES"] is DBNull ? (decimal?)null : Convert.ToDecimal(dt.Rows[i]["DELIVERY_CHARGES"]);
                            rt.REMARK = (dt.Rows[i]["REMARK"]).ToString();
                            rt.ADDRESS = (dt.Rows[i]["ADDRESS"]).ToString();
                            rt.STATUS = (dt.Rows[i]["STATUS"]).ToString();
                            rt.PAYMENT_STATUS = (dt.Rows[i]["PAYMENT_STATUS"]).ToString();
                            rt.REG_DATE = Convert.ToDateTime(dt.Rows[i]["REG_DATE"]).ToString("dd/MM/yyyy");
                            rt.ORDER_TIME = Convert.ToDateTime(dt.Rows[i]["REG_DATE"]).ToString("hh:mm tt");
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
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public JsonResult GetPreferredMenuProductList()
        {
            long id = Convert.ToInt64(Session["PreferredMenu_ID"]);
            long resId = Convert.ToInt64(Session["SHOP_ID"]);
            cmd = new SqlCommand("Panel_GetProductPreferredMenuCartList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PreferredMenu_ID", id);
            cmd.Parameters.AddWithValue("@RES_ID", resId);
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            con.Close();

            PreferredMenuCart rt;
            List<PreferredMenuCart> FinalreportList = new List<PreferredMenuCart>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rt = new PreferredMenuCart();
                    try
                    {
                        rt.PMCART_ID = Convert.ToInt64(dt.Rows[i]["PMCART_ID"]);
                        rt.PreferredMenu_ID = Convert.ToInt64(dt.Rows[i]["PreferredMenu_ID"]);
                        rt.RES_ID = Convert.ToInt64(dt.Rows[i]["RES_ID"]);
                        rt.USER_ID = Convert.ToInt64(dt.Rows[i]["USER_ID"]);
                        rt.PRODUCT_ID = Convert.ToInt64(dt.Rows[i]["PRODUCT_ID"]);
                        rt.PRODUCT_NAME = Convert.ToString(dt.Rows[i]["PRODUCT_NAME"]);
                        rt.PRODUCT_TYPE = Convert.ToString(dt.Rows[i]["PRODUCT_TYPE"]);
                        rt.QUANTITY = Convert.ToInt32(dt.Rows[i]["QUANTITY"]);
                        rt.AMOUNT = Convert.ToDecimal(dt.Rows[i]["AMOUNT"]);
                    }
                    catch (Exception ex)
                    {
                    }
                    FinalreportList.Add(rt);
                }
            }
            return Json(FinalreportList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllProducts()
        {
            long resId = Convert.ToInt64(Session["SHOP_ID"]);
            var _getadmin = db.TB_ProductMaster.Where(z => z.STATUS == "Active" && z.RES_ID == resId).Select(s => new { s.PRODUCT_ID, s.PRODUCT_NAME, s.PRODUCT_TYPE, s.PRICE, s.DISCOUNT, s.SERVES_COUNT, s.STATUS, s.REG_DATE }).ToList();
            return Json(_getadmin, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}