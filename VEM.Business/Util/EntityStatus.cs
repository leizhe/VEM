namespace VEM.Business.Util
{
    public enum MemberStatus
    {
        Disabled = 0,
        Available = 1
    }

    public enum PrivilegeMasterStatus
    {
        UserMaster = 0,
        RoleMaster = 1
    }

    public enum PrivilegeAccessStatus
    {
        Menu = 0,
        Button = 1
    }

    public enum CouponType
    {
        打折券 = 1,
        现金券 = 2
    }

    public enum MemberPayRecord
    {
        充值 = 1,
        消费 = 2
    }
    public enum MachineStatusType
    {
        租借 = 0,
        出售 = 1
    }

    public enum ShipmentStatus
    {
        可以出货 = 0x01,
        不可以出货 = 0x02,

        出货成功等待取货 = 0x03,
        出货成功已取货 = 0x04,
        出货失败 = 0x05
    }
    public enum FailedCode
    {
        售餐机处于配置状态 = 0x01,
        售餐机处于出货状态 = 0x02,
        售餐机缺货 = 0x03,

        货梯电机或红外对管故障 = 0x04,
        货梯电机或出货开关故障 = 0x05,
        货道电机故障或缺货 = 0x06,
    }

    public enum FaultCause 
    {
        货门位置检测故障 = 0x01,
        货梯高度检测故障 = 0x02,
        掉货红外检测故障 = 0x03,
        制冷系统故障 = 0x04,
        升降电机故障 = 0x05,
        货道电机故障 = 0x06,
    }

    public enum FaultStatus
    {
        故障发生 = 0x00,
        故障解除 = 0x01
    }

    public enum UserBrowserPrivilege
    {
        售货机 = 0x00,
        商品 = 0x01,
        优惠券 = 0x03,
        会员 = 0x04
    }


}
