using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using VEM.Business;
using VEM.Model;

namespace VEM.Web.Controllers
{
    //个人信息管理控制器
    public class AccountController : BaseController
    {
        public JsonResult GetDistrict(int cityId)
        {
            return Json(HtmlExtensions.GetDistrictByCityId(cityId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            ReadyCall(BusinessOperations.SignOut);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Login()
        {
            var message = ReadyCall(BusinessOperations.GetCookie);

            var user = message.Data
                ;
            if (user != null)
            {
                return View("LoginLock", user as User);
            }
            return View();
        }


        [HttpPost]
        public JsonResult Login(string username, string password, string remember)
        {
            return LoginAction(username, password, remember);
        }


        [HttpPost]
        public JsonResult LoginLock(string password)
        {
            var message = ReadyCall(BusinessOperations.GetCookie);
            var user = message.Data as User;

            Debug.Assert(user != null, "没有查询到用户");
            return LoginAction(user.LoginName, password, "true");
        }


        public ActionResult NoMySelf()
        {
            ReadyCall(BusinessOperations.DelCookie);
            return View("Login");
        }

        private JsonResult LoginAction(string username, string password, string remember)
        {
            var args = new Dictionary<string, string>
            {
                {"UserName", username},
                {"PassWord", password},
                {"Remember", remember}
            };
            return Json(ReadyCall(BusinessOperations.SignIn, args), JsonRequestBehavior.AllowGet);
        }

        #region 个人信息管理

        public ActionResult UserDetails()
        {
            CheckPageButtonPrivilege();
            var user = ReadyCall(BusinessOperations.GetUser, CurrentUser.Id).Data as User;
            Debug.Assert(user != null, "没有查询到用户");
            ViewBag.CitySelect = HtmlExtensions.GetCitySelectList(user.City.Id);
            ViewBag.DistrictSelect = HtmlExtensions.GetDistrictSelectList(user.District.Id, user.City.Id);
            return View(user);
        }

        [HttpPost]
        public JsonResult UserDetails(User user, string citySelect, string districtSelect)
        {
            var args = new Dictionary<string, object>();
            args.Add("Model", user);
            args.Add("CityId", citySelect);
            args.Add("DistrictId", districtSelect);

            return Json(ReadyCall(BusinessOperations.ChangeUserInfo, args), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeUserPic(string userPic)
        {
            ReadyCall(BusinessOperations.ChangeUserPic, userPic);
            return RedirectToAction("UserDetails", "Account");
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadImage(HttpPostedFileBase uploadImage)
        {
            return UploadImg(uploadImage, "UserPic");
        }





        public ActionResult MyMachine(int? page)
        {
            ViewBag.MenuPage = "UserDetails";
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var machineList = ReadyCall(BusinessOperations.GetMachineList).Data as IOrderedQueryable<Machine>;
            return PartialView("Controls/MyMachine", machineList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MyCommod(int? page)
        {
            ViewBag.MenuPage = "UserDetails";
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            var commodList = ReadyCall(BusinessOperations.GetCommodList).Data as IOrderedQueryable<Commod>;
            return PartialView("Controls/MyCommod", commodList.ToPagedList(pageNumber, pageSize));
        }

        #endregion

        #region 修改密码

        public ActionResult EditPass()
        {
            CheckPageButtonPrivilege();
            return View();
        }


        [HttpPost]
        public JsonResult EditPass(FormCollection form)
        {
            var args = new Dictionary<string, string>
            {
                {"UserId", CurrentUser.Id.ToString()},
                {"OldPass", form["oldPass"]},
                {"NewPass", form["newPass"]}
            };
            return Json(ReadyCall(BusinessOperations.ChangePassword, args), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}