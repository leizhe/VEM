namespace VEM.Business
{
    public class BusinessOperations
    {
        public const int SignIn = 0x0001;
        public const int SignOut = 0x0002;
        public const int GetCookie = 0x0003;
        public const int DelCookie = 0x0004;
        public const int GetLogedInUser = 0x0005;
        public const int GetCityList = 0x0006;
        public const int GetDistrictList = 0x0007;

        //1.数据监控
        public const int GetSalesHistoryList = 0x1001;
        public const int GetShipmentStatusRecordList = 0x1002;
        public const int GetFaultList = 0x1003;
        public const int GetErrorCount = 0x1004;
        public const int UpdFaultStatus = 0x1005;
        public const int GetReserveCount = 0x1006;
        public const int GetChartMonthSalesHistory = 0x1007;

        //2.售货机管理
        public const int GetMachineModelList = 0x2001;
        public const int AddMachineModel = 0x2002;
        public const int SaveMachineModel = 0x2003;
        public const int DelMachineModel = 0x2004;
        public const int GetMachineModelById = 0x2005;
        public const int GetMachineList = 0x2006;
        public const int AddMachine = 0x2007;
        public const int SaveMachine = 0x2008;
        public const int DelMachine = 0x2009;
        public const int GetMachineById = 0x2010;
        public const int SaveMachineRentOrSell = 0x2011;

        //3.商品管理
        public const int GetCommodList = 0x3001;
        public const int GetCommodById = 0x3002;
        public const int AddCommod = 0x3003;
        public const int SaveCommod = 0x3004;
        public const int DleCommod = 0x3005;
        public const int AddContainerRoad = 0x3006;
        public const int GetContainerRoadById = 0x3007;
        public const int SaveContainerRoad = 0x3008;


        //4.个人信息管理
        public const int ChangeUserInfo = 0x4001;
        public const int ChangePassword = 0x4002;
        public const int ChangeUserPic = 0x4003;

        //5.权限管理
        public const int GetRoleList = 0x5001;
        public const int SaveRole = 0x5002;
        public const int DelRole = 0x5004;
        public const int GetRole = 0x5005;
        public const int AddRole = 0x5006;
        public const int GetAllMenu = 0x5007;
        public const int GetUserList = 0x5008;
        public const int GetUser = 0x5009;
        public const int AddUser = 0x5010;
        public const int SaveUser = 0x5011;
        public const int DelUser = 0x5012;
        public const int SetRole = 0x5013;
        public const int ResetPassWord = 0x5014;
        public const int GetMenuByMenuCode = 0x5015;
        public const int SavePrivilege = 0x5016;
        public const int GetUserPrivilege = 0x5017;
        public const int GetMenuByMenuArray = 0x5018;
        public const int GetRolePrivilege = 0x5019;
        public const int GetMenuCodeByMenuIdList = 0x5020;
        public const int GetMyButtonStringByMenuNo = 0x5021;
        public const int GetBrowserPrivilege = 0x5022;

         //6.系统设置
        public const int GetMenuByMenuParentNo = 0x6001;
        public const int AddMenu = 0x6002;
        public const int GetMenuById = 0x6003;
        public const int AddIconList = 0x6004;
        public const int GetIconList = 0x6005;
        public const int EditMenu = 0x6006;
        public const int DelMenuById = 0x6007;
        public const int GetMenuByMenuNo = 0x6008;
        public const int GetCityById = 0x6009;
        public const int AddCity = 0x6010;
        public const int EditCity = 0x6011;
        public const int DelCity = 0x6012;
        public const int GetDistrictById = 0x6013;
        public const int AddDistrict = 0x6014;
        public const int EditDistrict = 0x6015;
        public const int DelDistrict = 0x6016;


        //7.会员管理
        public const int GetMemberList = 0x7001;
        public const int GetMemberById = 0x7002;
        public const int AddMember = 0x7003;
        public const int SaveMember = 0x7004;
        public const int DelMember = 0x7005;
        public const int GetCouponList = 0x7006;
        public const int GetCouponById = 0x7007;
        public const int AddCoupon = 0x7008;
        public const int SaveCoupon = 0x7009;
        public const int DelCoupon = 0x7010;
        public const int ResetMemberPassWord = 0x7011;
        public const int GetMemberCouponByMemberId = 0x7012;
        public const int AddMemberCoupon = 0x7013;
        public const int DelMemberCouponById = 0x7014;
        public const int GetMemberPayRecord = 0x7015;

    }
}
