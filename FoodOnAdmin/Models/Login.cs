using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
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

    public class Employee
    {
        public long EMP_ID { get; set; }
        public long VEHICAL_ID { get; set; }
        public string EMP_NAME { get; set; }
        public string MOBILE_NO { get; set; }
        public string PASSWORD { get; set; }
        public string ADDRESS { get; set; }
        public string AreaOfWork { get; set; }
        public string AADHAAR { get; set; }
        public string PAN { get; set; }
        public string EMAIL { get; set; }
        public string STATUS { get; set; }
        public string REG_DATE { get; set; }
        public string PROJECT_ACTION { get; set; }
        public string DOCUMENT_TYPE { get; set; }
        public string ADHARCARD_COPY { get; set; }

        //adharcard
        public string ImageBase64Data { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }

        //pancard
        public string PancardImageBase64Data { get; set; }
        public string PancardImageName { get; set; }
        public string PancardImageExtension { get; set; }

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
        public decimal FOODON_RATING { get; set; }
        public string RESTRO_BOOKING { get; set; }
        public string FOOD_DELIVERY { get; set; }
        public string Food { get; set; }
        public string Sweets { get; set; }
        public string Juice { get; set; }
        public string Cafeteria { get; set; }
        public string CATEGORY { get; set; }

        public string ImageBase64Data { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
    }

    public class Package
    {
        public long P_ID { get; set; }
        public string PACKAGE_NAME { get; set; }
        public string PACKAGE_DESCRIPTION { get; set; }
        public Decimal MRP { get; set; }
        public Decimal OFFER_PRICE { get; set; }
        public long PACKAGE_VALIDITY { get; set; }
        public int POST_COUNT { get; set; }
        public string STATUS { get; set; }
        public string REG_DATE { get; set; }

    }
    public class RestaurantVideoURLMaster
    {
        public long RES_ID { get; set; }
        public long VID_BANNER_ID { get; set; }
        public string RES_NAME { get; set; }
        public string VIDEO_URL_LINK { get; set; }
        public string VIDEO_NAME { get; set; }
        public string STATUS { get; set; }
        public string REG_DATE { get; set; }
        public string ACTION { get; set; }
       
    }

    public class FoodOnVideoURLMaster
    {
        public long VID_BANNER_ID { get; set; }
        public string VIDEO_URL_LINK { get; set; }
        public string VIDEO_NAME { get; set; }
        public int? FOOD_CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string STATUS { get; set; }
        public string REG_DATE { get; set; }
        public string ACTION { get; set; }

    }

    public class FoodOnSaleBannerMaster
    {
        public long BANNER_ID { get; set; }
        public int? FOOD_CATEGORY_ID { get; set; }
        public string BANNER_URL { get; set; }
        public string BANNER_NAME { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string STATUS { get; set; }
        public string REG_DATE { get; set; }
        public string ACTION { get; set; }
        public string ImageBase64Data { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
    }

    public class SubscriptionMaster
    {
        public long RES_ID { get; set; }
        public long SUB_ID { get; set; }
        public long PACKAGE_ID { get; set; }
        public long PACKAGE_VALIDITY { get; set; }
        public long POST_COUNT { get; set; }
        public string RES_NAME { get; set; }
        public string PACKAGE_NAME { get; set; }
        public string SUB_START_DATE { get; set; }
        public string SUB_END_DATE { get; set; }
        public string STATUS { get; set; }
        public string REG_DATE { get; set; }
    }
}