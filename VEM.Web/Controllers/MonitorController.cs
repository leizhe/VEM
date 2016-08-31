using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using VEM.Business;
using VEM.Model;
using System.Text;

namespace VEM.Web.Controllers
{
    //数据监控控制器
    public class MonitorController : BaseController
    {
        #region 出货状态
        public ActionResult ShipmentMsg(string searchString, string currentFilter, int? page)
        {
            SaveSearchString(ref searchString, ref page, currentFilter);
            CheckPageButtonPrivilege();

            ViewBag.CurrentFilter = searchString;
            ViewBag.MenuPage = "ShipmentMsg";

            Dictionary<string, object> args = new Dictionary<string, object> {{"SearchString", searchString}};
            var errorList = ReadyCall(BusinessOperations.GetShipmentStatusRecordList, args).Data as IOrderedQueryable<ShipmentStatusRecord>;
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(errorList.ToPagedList(pageNumber, pageSize));
        }


        #endregion

        #region 故障信息
        public ActionResult ErrorMsg(string searchString, string currentFilter, int? page, string errorSelect, string currentSelect)
        {

            if (searchString != null||errorSelect != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                errorSelect = currentSelect;
            }

            CheckPageButtonPrivilege();
            

            ViewBag.CurrentFilter = searchString;
            int tempSelect = errorSelect == null ? 0 : int.Parse(errorSelect);
            ViewBag.ErrorSelect = GetErrorSelectItem(tempSelect);
            ViewBag.CurrentSelect = tempSelect;

            ViewBag.MenuPage = "ErrorMsg";

            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("SearchString", searchString);
            args.Add("ErrorSelect", errorSelect);
            var errorList = ReadyCall(BusinessOperations.GetFaultList, args).Data as IOrderedQueryable<Fault>;
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(errorList.ToPagedList(pageNumber, pageSize));
        }


        private List<SelectListItem> GetErrorSelectItem(int? selectValue)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            SelectListItem deaultOption = new SelectListItem() { Text = "故障类型", Value = "0" };

            SelectListItem a = new SelectListItem() { Text = "未解除的故障", Value = "1" };
            SelectListItem b = new SelectListItem() { Text = "已解除的故障", Value = "2" };
            lst.Add(deaultOption);
            lst.Add(a);
            lst.Add(b);
            foreach (var item in lst)
            {
                if (string.IsNullOrEmpty(item.Value) && !selectValue.HasValue)
                {
                    item.Selected = true;
                }
                else
                {
                    if (item.Value.Equals(selectValue.ToString()))
                    {
                        item.Selected = true;
                    }
                }
            }
            return lst;
        }

        public ActionResult ErrorStatusUpdate(int id)
        {
            return Json(ReadyCall(BusinessOperations.UpdFaultStatus,id),JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 库存信息
        public ActionResult ReserveMsg(string searchString, string currentFilter, int? page)
        {

            SaveSearchString(ref searchString, ref page, currentFilter);
            CheckPageButtonPrivilege();

            ViewBag.CurrentFilter = searchString;
            ViewBag.MenuPage = "ReserveMsg";
            var userList = ReadyCall(BusinessOperations.GetMachineList, searchString).Data as IOrderedQueryable<Machine>;
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(userList.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult ManageContainerRoad(int id)
        {
            Machine machine = ReadyCall(BusinessOperations.GetMachineById, id).Data as Machine;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='scroller' data-always-visible='1' data-rail-visible1='1' style=' overflow:auto;'>");
            sb.Append("     <div class='row-fluid'>");
            Debug.Assert(machine != null, "没有查询到售货机");
            for (int i = 0; i < machine.MachineModel.ContainerRoadCount; i++)
            {
                sb.Append("<div class='span3 user-info'>");
                ContainerRoad cr = machine.ContainerRoad.FirstOrDefault(p => p.Number == (i + 1));

                if (cr != null)
                {
                   
                    sb.AppendFormat("<img style='width:55px; height:55px;' src='{0}'>", cr.Commod.Pic);
                }
                else
                {
                    sb.Append("<img style='width:45px; height:45px;' src='/UpImage/Commod/defaultCommod.jpg'>");
                }
                sb.Append("<div class='details'>");
                sb.Append("<div>");
                if (cr != null)
                {
                    sb.AppendFormat("<lable>{0}</lable>",  cr.Commod.Name);
                    if (cr.ReamainderCount <= 0)
                    {
                        sb.AppendFormat("<span class='label label-important'>已售空</span>");
                    }
                    else
                    {
                        sb.AppendFormat("<span class='label label-success'>正常销售</span>");
                    }

                }
                else
                {
                    sb.Append("<lable>未添加商品</lable></a>");
                    sb.Append("<span class='label label-info'>无商品</span>");
                }
                sb.Append("</div>");
                if (cr != null)
                {

                    sb.AppendFormat("<div>货道号：<code>{0}</code>  库存：{1}/{2}</div>", (i + 1), cr.ReamainderCount, cr.MaxCount);
                }
                else
                {
                    sb.AppendFormat("<div>货道号：<code>{0}</code>  库存：0/0</div>", (i + 1));
                }

                sb.Append("</div>");
                sb.Append("</div>");
                if ((i + 1) % 4 == 0)
                {
                    sb.Append(" </div>");
                    sb.Append("     <div class='row-fluid'>");
                }
            }
            sb.Append(@"    </div>
                       </div>");
            return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        }


        public string GetReserveAndCapacityString(int manageId)
        {
            Machine machine = ReadyCall(BusinessOperations.GetMachineById, manageId).Data as Machine;
            int? allReamainderCount = 0;
            int? allMaxCount = 0;
            Debug.Assert(machine != null, "没有查询到售货机");
            for (int i = 0; i < machine.MachineModel.ContainerRoadCount; i++) { 
                 ContainerRoad cr = machine.ContainerRoad.FirstOrDefault(p => p.Number == (i + 1));
                 if (cr != null) {
                     allReamainderCount += cr.ReamainderCount;
                     allMaxCount += cr.MaxCount;
                 }
            }
            return allReamainderCount + "/" + allMaxCount;
        }

        public string GetContainerRoadString(int manageId)
        {
            Machine machine = ReadyCall(BusinessOperations.GetMachineById, manageId).Data as Machine;
            Debug.Assert(machine != null, "没有查询到售货机");
            return machine.ContainerRoad.Count.ToString();

        }



        #endregion

        #region 销售信息


        public ActionResult SaleMsg(string searchString, 
            string currentFilter, 
            int? page, 
            string begintime, 
            string endTime, 
            string saveBegintime, 
            string saveEndTime)
        {

            if (searchString != null || begintime != null || endTime != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                begintime = saveBegintime;
                endTime = saveEndTime; 
            }

            CheckPageButtonPrivilege();

            ViewBag.CurrentFilter = searchString;
            ViewBag.SaveBegintime = begintime;
            ViewBag.SaveEndTime = endTime;
            ViewBag.MenuPage = "SaleMsg";

            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("SearchString", searchString);
            args.Add("Begintime", begintime);
            args.Add("EndTime", endTime);
            var saleList = ReadyCall(BusinessOperations.GetSalesHistoryList, args).Data as IOrderedQueryable<SalesHistory>;
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(saleList.ToPagedList(pageNumber, pageSize));
        }

        #endregion


    }
}