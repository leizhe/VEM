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
    
    public partial class BrowserPrivilege
    { 
        public int Id { get; set; }
        public bool MachinePrivilege { get; set; }
        public bool CommodPrivilege { get; set; }
        public bool MemberPrivilege { get; set; }
        public int PrivilegeMaster { get; set; }
        public int PrivilegeMasterValue { get; set; }
        public bool CouponPrivilege { get; set; }
    }
}