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
    
    public partial class TB_ProductMaster
    {
        public long PRODUCT_ID { get; set; }
        public Nullable<long> RES_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public Nullable<decimal> DISCOUNT { get; set; }
        public string IS_STOCK { get; set; }
        public string PRODUCT_TYPE { get; set; }
        public string PRODUCT_IMAGE { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> REG_DATE { get; set; }
        public string RATING { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<long> SERVES_COUNT { get; set; }
        public Nullable<double> DISTANACE_KM { get; set; }
        public Nullable<long> OFFERS_ID { get; set; }
        public Nullable<long> P_CAT_ID { get; set; }
    
        public virtual TB_Restaurant TB_Restaurant { get; set; }
    }
}
