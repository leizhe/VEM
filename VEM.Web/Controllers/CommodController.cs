using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VEM.Business;
using VEM.Model;
using PagedList;
using System.Text;

namespace VEM.Web.Controllers
{
    //商品管理控制器
    public class CommodController : BaseController
    {

        #region 商品管理
        public ActionResult CommodIndex(string searchString, string currentFilter, int? page)
        {
            SaveSearchString(ref searchString, ref page, currentFilter);
            CheckPageButtonPrivilege();
            ViewBag.CurrentFilter = searchString;
            ViewBag.MenuPage = "CommodIndex";
            var commodList = ReadyCall(BusinessOperations.GetCommodList, searchString).Data as IOrderedQueryable<Commod>;
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(commodList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CommodCreateEdit(int id)
        {

            ViewBag.Pagetitle = "添加商品";
            if (id > 0)
            {
                ViewBag.Pagetitle = "修改商品";
                Commod commod=ReadyCall(BusinessOperations.GetCommodById, id).Data as Commod;
                if (commod != null)
                {
                    ViewBag.js = "<script>$(function () {ImageBand('" + commod.Pic + "')});</script>";
                    return View(commod);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult CommodCreateEdit(Commod model, int id)
        {

            if (id > 0)
            {

                MessageAbstract msg = ReadyCall(BusinessOperations.SaveCommod, model);
                return Json(msg);
            }
            else
            {

                MessageAbstract msg = ReadyCall(BusinessOperations.AddCommod, model);
                return Json(msg);
            }
        }


        public ActionResult CommodDle(int id)
        {
            return Json(ReadyCall(BusinessOperations.DleCommod, id), JsonRequestBehavior.AllowGet);
        }

      

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadImage(HttpPostedFileBase uploadImage)
        {
            return UploadImg(uploadImage, "CommodPic");
        }

        public ActionResult CommodDetails(int id)
        {
            if (id > 0)
            {
                Commod commod = ReadyCall(BusinessOperations.GetCommodById, id).Data as Commod;
                if (commod != null)
                {
                    ViewBag.js = "<script>$(function () {ImageBand('" + commod.Pic + "')});</script>";
                    return View(commod);
                }
            }
            return View();
        }


        #endregion

        #region 货道供货
        public ActionResult CommodSupply(string searchString, string currentFilter, int? page)
        {
            SaveSearchString(ref searchString, ref page, currentFilter);
            CheckPageButtonPrivilege();
            ViewBag.CurrentFilter = searchString;
            ViewBag.MenuPage = "CommodSupply";
            var userList = ReadyCall(BusinessOperations.GetMachineList, searchString).Data as IOrderedQueryable<Machine>;
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(userList.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult ManageContainerRoad(int id)
        {
            Machine machine = ReadyCall(BusinessOperations.GetMachineById, id).Data as Machine;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='scroller' data-always-visible='1' data-rail-visible1='1' style=' overflow:auto; width: 900px; height: 400px;'>");
            sb.Append("     <div class='row-fluid'>");
            Debug.Assert(machine != null, "没有查询到售货机");
            for (int i = 0; i < machine.MachineModel.ContainerRoadCount; i++)
            {
                sb.Append("<div class='span3 user-info'>");
                ContainerRoad cr = machine.ContainerRoad.FirstOrDefault(p => p.Number == (i+1));
               
                if (cr!=null)
                {
                    sb.AppendFormat("<a href='ContainerRoadCreateEdit?ms=CommodSupply&RoadNum={0}&MachineId={1}&ContainerRoadId={2}'>", (i + 1),machine.Id, cr.Id);
                    sb.AppendFormat("<img style='width:45px; height:45px;' src='{0}'>", cr.Commod.Pic);
                }
                else
                {
                    sb.AppendFormat("<a href='ContainerRoadCreateEdit?ms=CommodSupply&RoadNum={0}&MachineId={1}&ContainerRoadId=0'>", (i + 1), machine.Id);
                    sb.Append("<img style='width:45px; height:45px;' src='/UpImage/Commod/defaultCommod.jpg'>");
                }
                sb.Append("</a>");
                sb.Append("<div class='details'>");
                sb.Append("<div>");
                if (cr != null)
                {
                    sb.AppendFormat("<a href='ContainerRoadCreateEdit?ms=CommodSupply&RoadNum={0}&MachineId={2}&ContainerRoadId={3}'>{1}</a>", (i + 1), cr.Commod.Name,machine.Id, cr.Id);
                    if (cr.ReamainderCount<=0)
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
                    sb.AppendFormat("<a href='ContainerRoadCreateEdit?ms=CommodSupply&RoadNum={0}&MachineId={1}&ContainerRoadId=0'>未添加商品</a>", (i + 1), machine.Id);
                    sb.Append("<span class='label label-info'>无商品</span>");
                }
                sb.Append("</div>");
                if (cr != null)
                {

                    sb.AppendFormat("<div>货道号：<code>{0}</code>  库存：{1}/{2}</div>",(i+1),cr.ReamainderCount,cr.MaxCount);
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


        public ActionResult ContainerRoadCreateEdit(int roadNum, int machineId, int containerRoadId, string searchString, string currentFilter, int? page)
        {

            SaveSearchString(ref searchString, ref page, currentFilter);

            ViewBag.CurrentFilter = searchString;
            ViewBag.ActionPage = "ContainerRoadCreateEdit";
            ViewBag.MenuPage = "CommodSupply";
            var commodList = ReadyCall(BusinessOperations.GetCommodList, searchString).Data as IOrderedQueryable<Commod>;
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            Machine machine = ReadyCall(BusinessOperations.GetMachineById, machineId).Data as Machine;
            Debug.Assert(machine != null, "没有查询到售货机");
            ViewBag.MachineCode = machine.MachineCode;
            ViewBag.MachineId= machine.Id;
            ViewBag.RoadNum = roadNum;
            ViewBag.ContainerRoadId = containerRoadId;
            if (containerRoadId>0)
            {
                ContainerRoad containerRoad = ReadyCall(BusinessOperations.GetContainerRoadById,containerRoadId).Data as ContainerRoad;
                if (containerRoad != null)
                {
                    ViewBag.js = "<script>$(function () {CommodSelect('" + containerRoad.Commod.Id + "','" + containerRoad.Commod.Name + "')});</script>";
                    ViewBag.containerRoad = containerRoad;
                }
            }

            return View(commodList.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult ContainerRoadCreateEdit(ContainerRoad containerRoad, string roadNum, string machineId, string commodId)
        {
            
            containerRoad.Number = int.Parse(roadNum);
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("ContainerRoad", containerRoad);
            args.Add("MachineId", machineId);
            args.Add("CommodId", commodId);
            if (containerRoad.Id>0)
            {
                return Json(ReadyCall(BusinessOperations.SaveContainerRoad, args));
            }
            else
            {
                return Json(ReadyCall(BusinessOperations.AddContainerRoad, args));
            }

            
        }

       

        #endregion
    }
}