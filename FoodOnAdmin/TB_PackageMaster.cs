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
    
    public partial class TB_PackageMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_PackageMaster()
        {
            this.TB_SubscriptionMaster = new HashSet<TB_SubscriptionMaster>();
        }
    
        public long P_ID { get; set; }
        public string PACKAGE_NAME { get; set; }
        public string PACKAGE_DESCRIPTION { get; set; }
        public Nullable<decimal> MRP { get; set; }
        public Nullable<decimal> OFFER_PRICE { get; set; }
        public Nullable<long> PACKAGE_VALIDITY { get; set; }
        public Nullable<long> POST_COUNT { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> REG_DATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_SubscriptionMaster> TB_SubscriptionMaster { get; set; }
    }
}
