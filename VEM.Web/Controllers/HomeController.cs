using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using VEM.Business;
using VEM.Business.Util;
using VEM.Model;
using PagedList;

namespace VEM.Web.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            ViewBag.ErrorHtml = ErrorHtml();
            ViewBag.ReserveCount = ReserveCount();
            ViewBag.MemberCount = MemberCount();
            ViewBag.CommodCount = CommodCount();

            return View();
        }


        public ActionResult MyMachine(int? page)
        {
            ViewBag.MenuPage = "Index";
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var machineList = ReadyCall(BusinessOperations.GetMachineList).Data as IOrderedQueryable<Machine>;
            return PartialView("Controls/MyMachine", machineList.ToPagedList(pageNumber, pageSize));
        }
       
        public ActionResult GetHeader()
        {
            try
            {
                ViewData["UserName"] = string.IsNullOrEmpty(CurrentUser.Name) ? CurrentUser.LoginName : CurrentUser.Name;
                ViewData["UserPicUrl"] = CurrentUser.PicUrl;
                ViewData["ErrorMsgHtml"] = ErrorMsgHtml();
                return PartialView("Controls/Header");
            }
            catch 
            {
                return RedirectToAction("Login", "Account");
            }
          
        }

        public ActionResult GetMenu()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<ul class='page-sidebar-menu'>");
            sb.Append(" <li><div class='sidebar-toggler hidden-phone'></div></li>");
            sb.Append(" <li id='homeli' class='start'><a href='/Home/Index'><i class='icon-home'></i><span class='title'>首页</span> <span class=''></span></a></li>");

            List<MyMenu> allMenu = HtmlExtensions.AllMenu();
            List<int> menuResult = new List<int>();

            int? userId = CurrentUser.Id;
            IQueryable<Privilege> userPv = (ReadyCall(BusinessOperations.GetUserPrivilege, userId).Data as IQueryable<Privilege>);
            if (userPv != null)
            {
                List<int> userMenuList = userPv
                    .Where(p => p.PrivilegeAccess == (int)PrivilegeAccessStatus.Menu)
                    .Select(p=>p.PrivilegeAccessValue).ToArray()
                    .ToList();
                menuResult = menuResult.Union(userMenuList).ToList();
            }
            IEnumerable<int> roleIdList = CurrentUser.UserRole.Where(p=>p.User==CurrentUser).Select(p=>p.Role.Id);
            menuResult = (from rid in roleIdList select 
                              (ReadyCall(BusinessOperations.GetRolePrivilege, rid).Data as IQueryable<Privilege>) 
                              into rolePv where rolePv != null select rolePv
                              .Where(p => p.PrivilegeAccess == (int) PrivilegeAccessStatus.Menu)
                              .ToList()
                              .Select(p => p.PrivilegeAccessValue).ToArray().ToList())
                              .Aggregate(menuResult, (current, roleMenuArray) => current.Union(roleMenuArray).ToList());

            foreach (MyMenu menu in allMenu)
            {
                if (CheckMenuCode(menu.Code, menuResult))
                {
                    sb.AppendFormat("<li ><a href='javascript:;' code='{1}'><i class='{2}'></i><span class='title'>{0}</span><span class='arrow '></span></a><ul class='sub-menu'>", menu.Name, menu.Code, menu.Pic);
                    foreach (var childMenu in menu.Children)
                    {
                        if (menuResult.Contains(childMenu.MenuID))
                        {
                            sb.AppendFormat("<li><a href='/{2}' id='{3}' code='{1}'>{0}</a></li>", childMenu.Name, childMenu.Code, childMenu.Url, childMenu.Code);
                        }
                    }
                    sb.Append("</ul></li>");
                }
            }
            sb.Append("</ul>");
            ViewData["MyMenu"] = sb.ToString();
            return PartialView("Controls/Menu");
        }

        public ActionResult GetSalesHistory()
        {
            int resultCount = 15;
            Dictionary<string, object> args = new Dictionary<string, object> {{"ResultCount", resultCount}};
            var saleList = ReadyCall(BusinessOperations.GetSalesHistoryList, args).Data as IOrderedQueryable<SalesHistory>;
            return PartialView("SalesHistoryControl", saleList);
        }

        public ActionResult GetShipmentStatus()
        {
            int resultCount = 15;
            Dictionary<string, object> args = new Dictionary<string, object> {{"ResultCount", resultCount}};
            var shipmentStatusRecordList = ReadyCall(BusinessOperations.GetShipmentStatusRecordList, args).Data as IOrderedQueryable<ShipmentStatusRecord>;
            return PartialView("ShipmentStatusControl", shipmentStatusRecordList);
        }

        public ActionResult GetSalesHistoryChartData(int typeId)
        {
            
            if (typeId==1)
            {
               
                DateTime lastMonthBegin = DateTime.Now.AddMonths(-1).AddDays(1-DateTime.Now.Day);
                DateTime lastMonthEnd = DateTime.Now.AddDays(-DateTime.Now.Day);
                int days = (lastMonthEnd - lastMonthBegin).Days;
                object rData = ReadyCall(BusinessOperations.GetChartMonthSalesHistory,false).Data;
                return BindMonthSaleHistory(rData, days);
            }
            else
            {
                int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                object rData = ReadyCall(BusinessOperations.GetChartMonthSalesHistory,true).Data;
                return BindMonthSaleHistory(rData, days);
                

            }
        }

        private ActionResult BindMonthSaleHistory(object rData, int days)
        {
            List<ChartClass> clst = new List<ChartClass>();
            if (rData != null)
            {
                List<SalesHistory> lst = rData as List<SalesHistory>;
                for (int i = 1; i <= days; i++)
                {
                    if (lst != null)
                    {
                        int count = lst.Count(p => p.SalesDate.Day == i);
                        var model = new ChartClass { Day = i, SaleCount = count };
                        clst.Add(model);
                    }
                }

            }
            return Json(clst, JsonRequestBehavior.AllowGet);
        }

        private bool CheckMenuCode(string menuCode, List<int> menuIdLst)
        {
            IQueryable<string> menuCodeList = ReadyCall(BusinessOperations.GetMenuCodeByMenuIdList, menuIdLst).Data as IQueryable<string>;
            var result = menuCodeList != null && menuCodeList.Contains(menuCode);
          
            return result;
        }

        private dynamic ErrorMsgHtml()
        {
           Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("SearchString", "");
            args.Add("ErrorSelect", 1);
            var errorList = ReadyCall(BusinessOperations.GetFaultList,args).Data as IOrderedQueryable<Fault>;
            string errorCount = errorList == null ? "" : errorList.Count().ToString();
            StringBuilder sb = new StringBuilder();
            if (errorList != null && errorList.Any())
            {
                sb.Append("<li class='dropdown' id='header_notification_bar'>");

                sb.AppendFormat(@"<a href='#' class='dropdown-toggle' data-toggle='dropdown'>
                                    <i class='icon-warning-sign'></i>
                                    <span class='badge'>{0}</span>
                               </a>", errorCount);
                sb.Append("<ul class='dropdown-menu extended notification'>");
                sb.AppendFormat(@"<li>
                                     <p>您售货机有{0}个故障未处理</p>
                                 </li>", errorCount);
                foreach (var fault in errorList)
                {
                    if (fault.Cause != null)
                        sb.AppendFormat(@"<li><a href='/Monitor/ErrorMsg'><span class='label label-important'><i class='icon-bolt'></i></span>&lt;{0}&gt;{1}</a></li>", fault.Machine.MachineCode, fault.Cause.Value.ToEnumString(typeof(FaultCause)));
                }
                sb.Append(@" <li class='external'>
                                <a href='/Monitor/ErrorMsg'>查看详细故障信息 <i class='m-icon-swapright'></i></a>
                            </li>");

                sb.Append("</ul>");
                sb.Append("</li>");
            }
           
            
            return sb.ToString();
        }

        private dynamic ErrorHtml()
        {
            object reault=ReadyCall(BusinessOperations.GetErrorCount).Data;
            int errorCount = reault == null ? 0 : Convert.ToInt32(reault);
            StringBuilder sb = new StringBuilder();

            if (errorCount>0)
	        {
                sb.Append("<div class='dashboard-stat red'>");
                sb.Append(@"<div class='visual'>
                                <i class='icon-frown'></i>
                            </div>");
            }
            else
            {
                sb.Append("<div class='dashboard-stat blue'>");
                sb.Append(@"<div class='visual'>
                                <i class='icon-smile'></i>
                            </div>");
            }
            sb.Append("<div class='details'>");
            sb.AppendFormat(@"<div class='number'>{0}</div>",errorCount);
            sb.Append(errorCount > 0 ? "<div class='desc'>故障</div>" : "<div class='desc'>无故障</div>");
            sb.Append("</div>");

            sb.Append(@"<a class='more' href='/Monitor/ErrorMsg'>
                            查看更多 <i class='m-icon-swapright m-icon-white'></i>
                        </a>");

            sb.Append("</div>");

            return sb.ToString();
        }

        private dynamic ReserveCount()
        {
            object reault = ReadyCall(BusinessOperations.GetReserveCount).Data;
            int errorCount = reault == null ? 0 : Convert.ToInt32(reault);
            return errorCount;
        }

        private dynamic MemberCount()
        {
            var memberList = ReadyCall(BusinessOperations.GetMemberList).Data as IOrderedQueryable<Member>;
            int count = memberList != null && memberList.Any() ?  memberList.Count() :0;
            return count;
        }

        private dynamic CommodCount()
        {

            var commodList = ReadyCall(BusinessOperations.GetCommodList).Data as IOrderedQueryable<Commod>;
            int count = commodList != null && commodList.Any() ? commodList.Count() : 0;
            return count;
        }



    }


    public class ChartClass
    {
        public int Day { get; set; }
        public int SaleCount { get; set; }
    }
}