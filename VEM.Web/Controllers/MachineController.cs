using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VEM.Business;
using VEM.Model;
using PagedList;

namespace VEM.Web.Controllers
{
    //售货机管理控制器
    public class MachineController : BaseController
    {

        #region 型号管理
        public ActionResult MachineModel(int? page)
        {
            CheckPageButtonPrivilege();
            ViewBag.MenuPage = "MachineModel";
            var result = ReadyCall(BusinessOperations.GetMachineModelList).Data as IEnumerable<MachineModel>;
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MachineModelCreateEdit(int id)
        {
            ViewBag.Pagetitle = "添加售货机型号";
            if (id > 0)
            {
                ViewBag.Pagetitle = "修改售货机型号";
                return View(ReadyCall(BusinessOperations.GetMachineModelById, id).Data);
            }
            return View();
        }

        [HttpPost]
        public ActionResult MachineModelCreateEdit(MachineModel model,int id)
        {

            if (id > 0)
            {

                MessageAbstract msg = ReadyCall(BusinessOperations.SaveMachineModel, model);
                return Json(msg);
            }
            else
            {

                MessageAbstract msg = ReadyCall(BusinessOperations.AddMachineModel, model);
                return Json(msg);
            }
        }


        public ActionResult MachineModelDle(int id)
        {
            return Json(ReadyCall(BusinessOperations.DelMachineModel,id),JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 售货机管理
        public ActionResult MachineIndex(string searchString, string currentFilter, int? page)
        {
            SaveSearchString(ref searchString, ref page, currentFilter);
            CheckPageButtonPrivilege();

            ViewBag.CurrentFilter = searchString;
            ViewBag.MenuPage = "MachineIndex";
            var machineList = ReadyCall(BusinessOperations.GetMachineList, searchString).Data as IOrderedQueryable<Machine>;
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(machineList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MachineCreateEdit(int id)
        {
            if (id > 0)
            {
                ViewBag.Pagetitle = "修改售货机";
                Machine machine = ReadyCall(BusinessOperations.GetMachineById, id).Data as Machine;
                if (machine != null)
                {
                    ViewBag.MachineModelSelect = HtmlExtensions.GetMachineModelSelectList(machine.MachineModel.Id);
                    ViewBag.CitySelect = HtmlExtensions.GetCitySelectList(machine.City.Id);
                    ViewBag.DistrictSelect = HtmlExtensions.GetDistrictSelectList(machine.District.Id, machine.City.Id);
                    return View(machine);
                }
            }
            else
            {
                ViewBag.MachineModelSelect = HtmlExtensions.GetMachineModelSelectList(0);
                ViewBag.CitySelect = HtmlExtensions.GetCitySelectList(0);
                ViewBag.DistrictSelect = HtmlExtensions.GetDistrictSelectList(0, 0);
                ViewBag.Pagetitle = "添加售货机";
            }
            return View();
        }

        [HttpPost]
        public ActionResult MachineCreateEdit(Machine machine, int id, string machineModelSelect, string citySelect, string districtSelect)
        {

            Dictionary<string, object> args = new Dictionary<string, object>
            {
                {"Machine", machine},
                {"MachineModelId", machineModelSelect},
                {"CityId", citySelect},
                {"DistrictId", districtSelect}
            };

            if (id > 0)
            {

                MessageAbstract msg = ReadyCall(BusinessOperations.SaveMachine, args);
                return Json(msg);
            }
            else
            {

                MessageAbstract msg = ReadyCall(BusinessOperations.AddMachine, args);
                return Json(msg);
            }
        }

        public ActionResult MachineDle(int id)
        {
            return Json(ReadyCall(BusinessOperations.DelMachine, id), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 租售状态管理
        public ActionResult MachineStatus(string searchString, string currentFilter, int? page)
        {
            SaveSearchString(ref searchString, ref page, currentFilter);
            CheckPageButtonPrivilege();
            ViewBag.CurrentFilter = searchString;
            ViewBag.MenuPage = "MachineStatus";
            var userList = ReadyCall(BusinessOperations.GetMachineList, searchString).Data as IOrderedQueryable<Machine>;
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(userList.ToPagedList(pageNumber, pageSize));
        }



        

        public ActionResult MachineStatusUpdate(string searchString, string currentFilter, int? page, int id)
        {
            SaveSearchString(ref searchString, ref page, currentFilter);
            Machine machine = ReadyCall(BusinessOperations.GetMachineById, id).Data as Machine;
            ViewBag.ThisMachine = machine;
            ViewBag.ThisId = id;

            ViewBag.CurrentFilter = searchString;
            ViewBag.MenuPage = "MachineStatusUpdate";
            var userList = ReadyCall(BusinessOperations.GetUserList, searchString).Data as IOrderedQueryable<User>;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(userList.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult MachineStatusUpdate(string machineId, string userId, string machineStatusType)
        {
            Dictionary<string, object> args = new Dictionary<string, object>
            {
                {"MachineId", machineId},
                {"UserId", userId},
                {"MachineStatusType", machineStatusType}
            };

            return Json(ReadyCall(BusinessOperations.SaveMachineRentOrSell, args));
        }

        #endregion
    }
}