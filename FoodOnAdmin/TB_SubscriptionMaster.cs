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
    
    public partial class TB_SubscriptionMaster
    {
        public long SUB_ID { get; set; }
        public Nullable<long> RES_ID { get; set; }
        public string TRANSACTION_ID { get; set; }
        public string REFERENCE_NO { get; set; }
        public Nullable<long> PACKAGE_ID { get; set; }
        public Nullable<System.DateTime> SUB_START_DATE { get; set; }
        public Nullable<System.DateTime> SUB_END_DATE { get; set; }
        public Nullable<decimal> PAID_AMOUNT { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> REG_DATE { get; set; }
        public string PAY_STATUS { get; set; }
        public string PAY_SOURCE { get; set; }
    
        public virtual TB_PackageMaster TB_PackageMaster { get; set; }
        public virtual TB_Restaurant TB_Restaurant { get; set; }
    }
}
