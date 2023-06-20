using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Configuration;

namespace FoodOnAdmin
{
    public class Master
    {
        // public  static SqlConnection con = new SqlConnection(Properties.Settings.Default.dbconnection);
        public static string connectionString = ConfigurationManager.ConnectionStrings["DB_FoodOnLink"].ConnectionString;
        public static SqlConnection con = new SqlConnection(connectionString);
        static SqlCommand cmd;
        static SqlDataAdapter sda;
        static public int k;
        static DataTable dt;
       
        public static int FID, CID, CVID, PID;
        public static string username;
        public static string password;
        public static int ADMIN_ID = 1;
        public static string Qry = string.Empty;
        public static string msg = string.Empty;
        public static string Title = string.Empty;
        public static int role;
        public static string dashboarduser;
        public static string userlogin;
        public static string text, Tokens;
        public static int K = 0;
        public static string serverurl = @"https://admin.foodon.wowinfotech.co";
        public static string pdf = @"https://admin.foodon.wowinfotech.co";
 
        // public bool IsSplashFormVisible { get; }

        public static int sendpushsingle(string Msg, string TI, string user_id)
        {
            string Data;
            Tokens = Master.excutescalerqryString("select TOKEN from TB_UserRegistration where USER_ID = '" + user_id + "' ");
            if (Tokens != "")
            {
                try
                {
                    msg = Regex.Replace(Msg, "[\n]+", "");
                    //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://localhost:2005/api/PushNotification");
                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://132.148.136.83:8002/api/pushFCM");
                    // HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://localhost:2005/api/pushFCM");
                    myReq.Method = "POST";
                    myReq.ContentType = "application/json";
                    myReq.Timeout = 3000000;
                    TI = "S";
                    Data = "{\"MSG\":\"" + msg + "\",\"TITLE\":\"" + TI + "\",\"TOKEN\":\"" + Tokens + "\"}";
                    // string Data = "{\"IMAGE\":\"" + Convert.ToBase64String(image) + "\",\"MSG\":\"" + msg + "\"}";
                    //string Data = "{\"platform\":1,\"token\":\"" + null + "\",\"msg\":\"" + msg + "\",\"sound\":\"grapemaster\",\"badge\":\"\",\"payload\":{\"MSG\":\"" + msg + "\",\"TITLE\":\"" + TI + "\",\"IMAGE\":\"" + Convert.ToBase64String(image) + "\"}}";
                    byte[] PostData = System.Text.Encoding.UTF8.GetBytes(Data);
                    myReq.ContentLength = PostData.Length;
                    using (System.IO.Stream requestStream = myReq.GetRequestStream())
                    {
                        requestStream.Write(PostData, 0, PostData.Length);
                    }
                    myReq.GetResponse();

                    //if(msg.Length < 10)
                    //{ 
                    //    TI = "GrapeMaster " + msg ; 
                    //}
                    //else
                    //{
                    //    string a = msg.Substring(0, 10);
                    //        TI = "GrapeMaster "+msg.Substring(0,10);
                    //}
                    //TI = "GrapeMaster "+msg.Substring(m)+";
                    //msg =  Regex.Replace(Msg, "[\n]+", "");
                    //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://localhost:2005/api/PushSingleDeviceWoImage");
                    //myReq.Method = "POST";
                    //myReq.ContentType = "application/json";
                    //myReq.Timeout = 5000000;
                    Data = "{\"MSG\":\"" + msg + "\",\"TITLE\":\"" + TI + "\",\"TOKEN\":\"" + Tokens + "\"}";
                    //byte[] PostData = System.Text.Encoding.UTF8.GetBytes(Data);
                    //myReq.ContentLength = PostData.Length;
                    //using (System.IO.Stream requestStream = myReq.GetRequestStream())
                    //{
                    //    requestStream.Write(PostData, 0, PostData.Length);
                    //}
                    //myReq.GetResponse();

                    k = 0;
                }
                catch (Exception ex)
                {
                    k = 1;
                }
            }
            else
            {
                k = 1;

            }
            return k;
        }


        public static DataTable fillData(String qry)
        {
            //try
            //{
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dt = new DataTable();
            cmd = new SqlCommand(qry, con);
            sda = new SqlDataAdapter(cmd);
            // cmd.CommandTimeout = 60;
            sda.Fill(dt);
            con.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Check You Internet Connection. Please Try Again","Connection",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //}
            if (dt.Rows.Count > 0)
                return dt;
            else
                return null;


        }
        public static void sendmail(string mail1, string message, string title)
        {
            //try
            //{
            //    MailMessage mail = new MailMessage();
            //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            //    mail.From = new MailAddress("bhajiwala.onlineapp@gmail.com");
            //    mail.To.Add(mail1.Trim());
            //    mail.Subject = title;
            //    mail.Body = message;

            //    SmtpServer.Credentials = new System.Net.NetworkCredential("bhajiwala.onlineapp@gmail.com", "Bhajiwala@321Online");
            //    SmtpServer.EnableSsl = true;
            //    SmtpServer.Send(mail);

            //}
            //catch (Exception ex)
            //{

            //}
            try 
            { 
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(mail1);
            mail.To.Add(mail1.Trim());
            mail.Subject = title;
            mail.Body = message;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("bhajiwala.onlineapp@gmail.com", "Bhajiwala@321Online");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            //MessageBox.Show("mail Send");
        }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString());
            }

}
        //public static void send(string varMSG, string varPhNo)
        //{
        //    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://nimbusit.co.in/api/swsend.asp?username=t1AmericanFerto&password=14876344&sender=AFCWOW&sendto=" + varPhNo + "&message=" + varMSG + "");
        //    // HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://nimbusit.co.in/api/swsendUni2.asp?username=t1wowinfotech&password=33022096&sender=AFCWOW&sendto=" + varPhNo + "&message=" + varMSG + "");
        //    // HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://49.50.67.32/smsapi/httpapi.jsp?username=wowdp&password=wowdp&from=wowinf&to=" + varPhNo + "&text=" + varMSG + "&coding=0");
        //    //  HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://login.businesslead.co.in/api/mt/SendSMS?user=wowinfotech&password=wowinfotech6125&senderid=WOWINF&channel=Trans&DCS=0&flashsms=0&number=" + varPhNo + "&text=" + varMSG + "&route=6");
        //    //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://nimbusit.co.in/api/swsendSingle.asp?username=t1wowinfotech&password=33022096&sender=AGROJY&sendto=" + varPhNo + "&message=" + varMSG + "");
        //    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        //    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        //    string responseString = respStreamReader.ReadToEnd();
        //    respStreamReader.Close();
        //    myResp.Close();
        //}
        public static DataSet getdataDataSet(string sqlQuery)
        {
            try
            {
                DataSet ds = new DataSet();

                // set your connection here
                // SqlConnection conn = new SqlConnection("");

                // execute query with your connection
                SqlDataAdapter adapt = new SqlDataAdapter(sqlQuery, con);

                // open connection, fill data and close
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                adapt.Fill(ds);
                con.Close();

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static int findRec(String qry)
        {

            cmd = new SqlCommand(qry, con);
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            object i = cmd.ExecuteScalar();
            con.Close();
            if (Convert.ToInt32(i) > 0)
                return Convert.ToInt32(i);
            else
                return 0;
        }
        public static int getcount(String qry)
        {
            cmd = new SqlCommand(qry, con);
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            object i = cmd.ExecuteScalar();
            con.Close();
            if (Convert.ToInt32(i) > 0)
                return Convert.ToInt32(i);
            else
                return 0;
        }

        public void getData(string Q)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            cmd = new SqlCommand(Q, con);
            // cmd.CommandTimeout = 60;
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static int getcount1(string Q)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dt = new DataTable();
            cmd = new SqlCommand(Q, con);
            sda = new SqlDataAdapter(cmd);
            // cmd.CommandTimeout = 60;
            sda.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows.Count);
            else
                return 0;
        }

        public static int excutescalerqryint(String qry)
        {
            object i = null;
            cmd = new SqlCommand(qry, con);
            cmd.CommandTimeout = 20000;
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                i = cmd.ExecuteScalar();
                con.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ec.Message);
            }
            if (Convert.ToInt32(i) > 0)
                return Convert.ToInt32(i);
            else
                return 0;
        }

        public static void executeQueryWithTransactionStmt(string Qry, string msg, string tital)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            cmd = Master.con.CreateCommand();
            SqlTransaction trn;
            trn = con.BeginTransaction("SimpleTransaction");
            cmd.Connection = con;
            cmd.Transaction = trn;
            try
            {
                cmd.CommandText = Qry;
                int i = cmd.ExecuteNonQuery();
                trn.Commit();
                con.Close();
                if (i > 0)
                {
                    //XtraMessageBox.Show("'" + msg + "'", "'" + tital + "'", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show("Commit Exception Type: '" + ex.GetType() + "' \n Message: '" + ex.Message + "' ", "PO Generation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    trn.Rollback();
                }
                catch (Exception ex2)
                {
                    //XtraMessageBox.Show("Commit Exception Type: '" + ex2.GetType() + "' \n Message: '" + ex2.Message + "' ", "PO Generation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }

        public static string excutescalerqryString(String qry)
        {
            Object i = null;
            cmd = new SqlCommand(qry, con);
            //  cmd.CommandTimeout = 60;
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                i = cmd.ExecuteScalar();
                con.Close();
            }
            catch (Exception ex2)
            { //XtraMessageBox.Show("Commit Exception Type: '" + ex2.GetType() + "' \n Message: '" + ex2.Message + "' ", "PO Generation", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            if (i != null)
                return Convert.ToString(i);
            else
                return null;
        }

        public static int InsertUpdateDeleteRecord(string qry)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            cmd = con.CreateCommand();
            // cmd.CommandTimeout = 60;
            SqlTransaction trn;
            trn = con.BeginTransaction("SimpleTransaction");
            cmd.Connection = con;
            cmd.Transaction = trn;

            try
            {
                cmd.CommandText = qry;
                k = cmd.ExecuteNonQuery();
                trn.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show("Commit Exception Type: '" + ex.GetType() + "' \n Message: '" + ex.Message + "' ", "PO Generation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    trn.Rollback();
                }
                catch (Exception ex2)
                {
                    //XtraMessageBox.Show("Commit Exception Type: '" + ex2.GetType() + "' \n Message: '" + ex2.Message + "' ", "PO Generation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            return (k);
        }

        public static void SendNotification(string sqa, string sq)
        {
            string data = "";
            string P_ICON = "http://bvfagroadmin.wowbharat.com/assets/img/IMG-20190720-WA0004.jpg";
            WebRequest WebRequest = WebRequest.Create("https://onesignal.com/api/v1/notifications");
            WebRequest.Method = "post";
            WebRequest.ContentType = "application/json";

            WebRequest.Headers.Add("Authorization", "Basic NTY4ODZlZTItNjNjMC00MjcxLWI3YmEtYmY0OWMxMjY2NDRl");

            data = "{\"app_id\":\"15fb2e75-442d-4261-bda4-48a1406d7350\",\"include_player_ids\": [\"" + sqa + "\"],\"headings\":{\"en\":\"BVF Agro Solution\"},\"contents\": {\"en\": \"" + sq + "\"},  \"big_picture\":\"" + P_ICON + "\",\"android_sound\":\"1\"}";//"big_picture":"http://www.planwallpaper.com/static/images/9-credit-1.jpg"

            if (data != "")
            {
                Byte[] byteArray = Encoding.UTF8.GetBytes(data);
                WebRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = WebRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse WebResponse = WebRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = WebResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
        }



        private static Random random = new Random((int)DateTime.Now.Ticks);
        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            int ch;

            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65));
                builder.Append(ch);
            }
            return builder.ToString();
        }


    }
}

