//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace VEM.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SalesHistory
    { 
        public int Id { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime SalesDate { get; set; }
        public double SalePrice { get; set; }
    
        public virtual Commod Commod { get; set; }
        public virtual Machine Machine { get; set; }
    }
}
