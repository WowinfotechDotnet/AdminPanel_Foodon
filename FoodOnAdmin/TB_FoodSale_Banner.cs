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
    
    public partial class TB_FoodSale_Banner
    {
        public long BANNER_ID { get; set; }
        public string BANNER_NAME { get; set; }
        public string BANNER_URL { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> REG_DATE { get; set; }
        public Nullable<int> FOOD_CATEGORY_ID { get; set; }
    
        public virtual TB_FOOD_CATEGORY TB_FOOD_CATEGORY { get; set; }
    }
}
