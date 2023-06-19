using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;

namespace FoodOnAdmin.Models
{
    public class Login
    {
        public string MOBILE_NO { set; get; }
        [RegularExpression("[789][0-9]{9}", ErrorMessage = "Invalid Mobile Number")]
        public string PASSWORD { get; set; }
        ////public string ROLE { get; set; }
        ////public string OTP { get; set; }
        ////public string keeeMeLoggedIn { get; set; }
        public bool RememberMe { get; set; }
    }

    public class HomeLogin
    {
        public string MOBILE_NO { set; get; }
        [RegularExpression("[789][0-9]{9}", ErrorMessage = "Invalid Mobile Number")]
        public string PASSWORD { get; set; }
        ////public string ROLE { get; set; }
        ////public string OTP { get; set; }
        ////public string keeeMeLoggedIn { get; set; }
        public bool RememberMe { get; set; }
    }




    public class Admin
    {
        public long ADMIN_ID { get; set; }
        public string ADMIN_NAME { get; set; }
        public string MOBILE_NO { get; set; }
        public string PASSWORD { get; set; }
        public string ADDRESS { get; set; }
        public string ADHARCARD_NO { get; set; }
        public string EMAIL { get; set; }
        public string ROLE_NAME { get; set; }
        public Nullable<long> ROLE_ID { get; set; }
        public string STATUS { get; set; }
        public string REG_DATE { get; set; }

    }

    public class UserMaster
    {
        public long USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string EMAIL_ID { get; set; }
        public string ADDRESS { get; set; }
        public string PROFILE_PHOTO { get; set; }
        public string STATUS { get; set; }
        public string REG_DATE { get; set; }
    }

    public class Hotel
    {
        public long RES_ID { get; set; }
        public long RI_ID { get; set; }
        public string RES_NAME { get; set; }
        public string R_PHOTO { get; set; }
        public string R_TYPE { get; set; }
        public string OWNER_NAME { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string FOOD_TYPE { get; set; }
        public string PASSWORD { get; set; }
        public string CONFIRM_PASSWORD { get; set; }
        public string RES_LOGO { get; set; }
        public string RES_OPEN_TIME { get; set; }
        public string RES_CLOSE_TIME { get; set; }
        public string ADDRESS { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public string PINCODE { get; set; }
        public string STATUS { get; set; }
        public string REG_DATE { get; set; }
        public string DESCRIPTION { get; set; }

        public string ImageBase64Data { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
    }

}