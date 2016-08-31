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
    
    public partial class Commod
    { 
        public Commod()
        {
            this.ContainerRoad = new HashSet<ContainerRoad>();
            this.SalesHistory = new HashSet<SalesHistory>();
            this.ShipmentStatusRecord = new HashSet<ShipmentStatusRecord>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Describle { get; set; }
        public string Code { get; set; }
        public Nullable<int> CreateUserID { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyUserID { get; set; }
        public string Unit { get; set; }
        public string Capacity { get; set; }
        public Nullable<double> Price { get; set; }
    
        public virtual ICollection<ContainerRoad> ContainerRoad { get; set; }
        public virtual ICollection<SalesHistory> SalesHistory { get; set; }
        public virtual ICollection<ShipmentStatusRecord> ShipmentStatusRecord { get; set; }
    }
}
