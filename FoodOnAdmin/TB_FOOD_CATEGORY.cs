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
    
    public partial class TB_FOOD_CATEGORY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_FOOD_CATEGORY()
        {
            this.TB_FoodSale_Banner = new HashSet<TB_FoodSale_Banner>();
            this.TB_MasterPage_Video_Banner_Image = new HashSet<TB_MasterPage_Video_Banner_Image>();
        }
    
        public int CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> REG_DATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_FoodSale_Banner> TB_FoodSale_Banner { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_MasterPage_Video_Banner_Image> TB_MasterPage_Video_Banner_Image { get; set; }
    }
}
