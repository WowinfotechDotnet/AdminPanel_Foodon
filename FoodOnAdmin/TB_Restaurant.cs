//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodOnAdmin
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_Restaurant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_Restaurant()
        {
            this.TB_DinningAnswer = new HashSet<TB_DinningAnswer>();
            this.TB_PostMaster = new HashSet<TB_PostMaster>();
            this.TB_ProductMaster = new HashSet<TB_ProductMaster>();
            this.TB_SubscriptionMaster = new HashSet<TB_SubscriptionMaster>();
        }
    
        public long RES_ID { get; set; }
        public string RES_NAME { get; set; }
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
        public Nullable<System.DateTime> REG_DATE { get; set; }
        public string DESCRIPTION { get; set; }
        public string ADVANCED_AMT { get; set; }
        public string QR_CODE { get; set; }
        public string CATEGORY { get; set; }
        public Nullable<decimal> RES_RATING { get; set; }
        public Nullable<double> DISTANCE_KM { get; set; }
        public Nullable<long> OFFERS_ID { get; set; }
        public Nullable<long> TIME { get; set; }
        public string RES_LIKE { get; set; }
        public string Food { get; set; }
        public string Sweets { get; set; }
        public string Juice { get; set; }
        public string Cafeteria { get; set; }
        public string RESTRO_BOOKING { get; set; }
        public string FOOD_DELIVERY { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_DinningAnswer> TB_DinningAnswer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_PostMaster> TB_PostMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_ProductMaster> TB_ProductMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_SubscriptionMaster> TB_SubscriptionMaster { get; set; }
    }
}
