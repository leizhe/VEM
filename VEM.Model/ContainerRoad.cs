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
    
    public partial class ContainerRoad
    { 
        public int Id { get; set; }
        public int GroupNumber { get; set; }
        public bool IsEnable { get; set; }
        public int Number { get; set; }
        public Nullable<int> ReamainderCount { get; set; }
        public Nullable<int> MaxCount { get; set; }
        public Nullable<int> MotorStatus { get; set; }
    
        public virtual Commod Commod { get; set; }
        public virtual Machine Machine { get; set; }
    }
}
