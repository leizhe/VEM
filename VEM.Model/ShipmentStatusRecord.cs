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
    
    public partial class ShipmentStatusRecord
    { 
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public Nullable<int> ShipmentStatus { get; set; }
        public Nullable<int> FailedCode { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    
        public virtual Machine Machine { get; set; }
        public virtual Commod Commod { get; set; }
    }
}
