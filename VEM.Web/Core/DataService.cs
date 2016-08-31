using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using VEM.Business;
using VEM.Business.Business;
using VEM.Business.Message;
using VEM.Business.Util;

namespace VEM.Web.Core
{
    public static class DataService
    {
       
        private static BusinessAbstract _businessObject;
     
        public static MessageAbstract Call(int op, object data)
        {
            object result = null;
            try
            {
                switch (op)
                {
                    case BusinessOperations.SignIn:
                    case BusinessOperations.SignOut:
                    case BusinessOperations.ChangeUserInfo:
                    case BusinessOperations.ChangePassword:
                    case BusinessOperations.ChangeUserPic:
                    case BusinessOperations.GetLogedInUser:
                    case BusinessOperations.GetCookie:
                    case BusinessOperations.DelCookie:
                    case BusinessOperations.GetCityList:
                    case BusinessOperations.GetDistrictList:
                        _businessObject = new AccountBusiness();
                        break;
                    case BusinessOperations.GetSalesHistoryList:
                    case BusinessOperations.GetShipmentStatusRecordList:
                    case BusinessOperations.GetFaultList:
                    case BusinessOperations.GetErrorCount:
                    case BusinessOperations.UpdFaultStatus:
                    case BusinessOperations.GetReserveCount:
                    case BusinessOperations.GetChartMonthSalesHistory:
                        _businessObject = new MonitorBusiness();
                        break;
                    case BusinessOperations.GetMachineModelList:
                    case BusinessOperations.AddMachineModel:
                    case BusinessOperations.SaveMachineModel:
                    case BusinessOperations.DelMachineModel:
                    case BusinessOperations.GetMachineModelById:
                    case BusinessOperations.GetMachineList:
                    case BusinessOperations.AddMachine:
                    case BusinessOperations.SaveMachine:
                    case BusinessOperations.DelMachine:
                    case BusinessOperations.GetMachineById:
                    case BusinessOperations.SaveMachineRentOrSell:
                        _businessObject = new MachineBusiness();
                        break;
                    case BusinessOperations.GetCommodById:
                    case BusinessOperations.GetCommodList:
                    case BusinessOperations.AddCommod:
                    case BusinessOperations.SaveCommod:
                    case BusinessOperations.DleCommod:
                    case BusinessOperations.AddContainerRoad:
                    case BusinessOperations.GetContainerRoadById:
                    case BusinessOperations.SaveContainerRoad:
                        _businessObject = new CommodBusiness();
                        break;
                    case BusinessOperations.GetRoleList:
                    case BusinessOperations.SaveRole:
                    case BusinessOperations.DelRole:
                    case BusinessOperations.GetRole:
                    case BusinessOperations.AddRole:
                    case BusinessOperations.GetAllMenu:
                    case BusinessOperations.GetUserList:
                    case BusinessOperations.GetUser:
                    case BusinessOperations.AddUser:
                    case BusinessOperations.SaveUser:
                    case BusinessOperations.DelUser:
                    case BusinessOperations.SetRole:
                    case BusinessOperations.ResetPassWord:
                    case BusinessOperations.GetMenuByMenuCode:
                    case BusinessOperations.SavePrivilege:
                    case BusinessOperations.GetUserPrivilege:
                    case BusinessOperations.GetMenuByMenuArray:
                    case BusinessOperations.GetRolePrivilege:
                    case BusinessOperations.GetMenuCodeByMenuIdList:
                    case BusinessOperations.GetMyButtonStringByMenuNo:
                    case BusinessOperations.GetBrowserPrivilege:
                        _businessObject = new PrivilegeBusiness();
                        break;
                    case BusinessOperations.GetMenuByMenuParentNo:
                    case BusinessOperations.AddMenu:
                    case BusinessOperations.GetMenuById:
                    case BusinessOperations.AddIconList:
                    case BusinessOperations.GetIconList:
                    case BusinessOperations.EditMenu:
                    case BusinessOperations.DelMenuById:
                    case BusinessOperations.GetMenuByMenuNo:
                    case BusinessOperations.GetCityById:
                    case BusinessOperations.AddCity:
                    case BusinessOperations.EditCity:
                    case BusinessOperations.DelCity:
                    case BusinessOperations.GetDistrictById:
                    case BusinessOperations.AddDistrict:
                    case BusinessOperations.EditDistrict:
                    case BusinessOperations.DelDistrict:
                        _businessObject = new SystemBusiness();
                        break;
                    case BusinessOperations.GetMemberList:
                    case BusinessOperations.GetMemberById:
                    case BusinessOperations.AddMember:
                    case BusinessOperations.SaveMember:
                    case BusinessOperations.DelMember:
                    case BusinessOperations.GetCouponList:
                    case BusinessOperations.GetCouponById:
                    case BusinessOperations.AddCoupon:
                    case BusinessOperations.SaveCoupon:
                    case BusinessOperations.DelCoupon:
                    case BusinessOperations.ResetMemberPassWord:
                    case BusinessOperations.GetMemberCouponByMemberId:
                    case BusinessOperations.AddMemberCoupon:
                    case BusinessOperations.DelMemberCouponById:
                    case BusinessOperations.GetMemberPayRecord:
                        _businessObject = new MemberBusiness();
                        break;

                }
                result = _businessObject.ProcessOperation(op, data);
            }
            catch (VEM.Business.Exceptions.VemException e)
            {
                return new UtilMessage
                {
                    StatusCode = e.ExceptionCode,
                    Data = null
                };
            }
            catch (NotImplementedException e)
            {
                return new UtilMessage
                {
                    StatusCode = (int)StatusCode.没有执行,
                    Data = e.Message
                };
            }
            catch (Exception e)
            {
                return new UtilMessage
                {
                    StatusCode = (int)StatusCode.未知错误,
                    Data = e.Message
                };
            }
            if (result != null && result.GetType() == typeof(ListMessage))
            {
                return (ListMessage)result;
            }
            return new UtilMessage
            {
                StatusCode = (int)StatusCode.操作成功,
                Data = result
            };

        }

    }
}