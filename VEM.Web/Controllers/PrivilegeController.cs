using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using PagedList;
using VEM.Business;
using VEM.Business.Util;
using VEM.Model;

namespace VEM.Web.Controllers
{
    ///权限管理控制器

    public class PrivilegeController : BaseController
    {


        #region 用户管理

        public ActionResult ResetPassWord(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return Json(ReadyCall(BusinessOperations.ResetPassWord, id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GiveUserRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = ReadyCall(BusinessOperations.GetUser, id).Data as User;
            ViewData["Roles"] = ((IOrderedQueryable<Role>) (ReadyCall(BusinessOperations.GetRoleList).Data)).Where(p => p.IsEnable.Value).ToList();
            Debug.Assert(user != null, "没有查询到用户");
            string strRole = user.UserRole.Aggregate(string.Empty, (current, r) => current + ("," + r.Role.Id));
            ViewData["RoleStr"] = strRole;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SetRole(int id, int[] role)
        {
            Dictionary<string, object> args = new Dictionary<string, object> {{"UserId", id}, {"RoleList", role}};
            return Json(ReadyCall(BusinessOperations.SetRole, args), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserIndex(string searchString, string currentFilter, int? page)
        {

            SaveSearchString(ref searchString, ref page, currentFilter);
            CheckPageButtonPrivilege();

            ViewBag.CurrentFilter = searchString;
            ViewBag.MenuPage = "UserIndex";
            var userList = ReadyCall(BusinessOperations.GetUserList, searchString).Data as IOrderedQueryable<User>;
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(userList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UserCreate()
        {
            ViewBag.CitySelect = HtmlExtensions.GetCitySelectList(0);
            ViewBag.DistrictSelect = HtmlExtensions.GetDistrictSelectList(0, 0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UserCreate(User user, string citySelect, string districtSelect)
        {
            Dictionary<string, object> args = new Dictionary<string, object>
            {
                {"Model", user},
                {"CityId", citySelect},
                {"DistrictId", districtSelect}
            };

            MessageAbstract message = ReadyCall(BusinessOperations.AddUser, args);

            ViewBag.CitySelect = HtmlExtensions.GetCitySelectList(0);
            ViewBag.DistrictSelect = HtmlExtensions.GetDistrictSelectList(0, 0);

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = ReadyCall(BusinessOperations.GetUser, id).Data as User;
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CitySelect = HtmlExtensions.GetCitySelectList(user.City.Id);
            ViewBag.DistrictSelect = HtmlExtensions.GetDistrictSelectList(user.District.Id, user.City.Id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UserEdit(User user, string citySelect, string districtSelect)
        {

            Dictionary<string, object> args = new Dictionary<string, object>
            {
                {"Model", user},
                {"CityId", citySelect},
                {"DistrictId", districtSelect}
            };
            MessageAbstract message = ReadyCall(BusinessOperations.SaveUser, args);

            ViewBag.CitySelect = HtmlExtensions.GetCitySelectList(user.City == null ? 0 : user.City.Id);
            ViewBag.DistrictSelect = HtmlExtensions.GetDistrictSelectList(user.District == null ? 0 : user.District.Id, user.City == null ? 0 : user.City.Id);

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserDelete(int id)
        {
            return Json(ReadyCall(BusinessOperations.DelUser, id), JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UserSave(User user)
        //{
        //    user.Id = base.CurrentUser.Id;
        //    MessageAbstract message = ReadyCall(BusinessOperations.SaveUser, user);
        //    //CheckErrors(message);
        //    return View();
        //}

        #endregion


        #region 角色管理
        public ActionResult RoleIndex(int page = 1)
        {
            CheckPageButtonPrivilege();
            ViewBag.MenuPage = "RoleIndex";
            const int pageSize = 14;
            var rolePage = ((ReadyCall(BusinessOperations.GetRoleList).Data) as IOrderedQueryable<Role>).ToPagedList(page, pageSize);
            return View(rolePage);
        }

        public ActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RoleCreate(Role role)
        {
            role.CreateUserID = CurrentUser.Id;
            role.CreateDate = DateTime.Now;
            role.ModifyDate = DateTime.Now;
            role.ModifyUserID = CurrentUser.Id;
            return Json(ReadyCall(BusinessOperations.AddRole, role), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RoleEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = ReadyCall(BusinessOperations.GetRole, id).Data;
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RoleEdit(Role role)
        {
            return Json(ReadyCall(BusinessOperations.SaveRole, role), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RoleDelete(int? id)
        {
            return Json(ReadyCall(BusinessOperations.DelRole, id), JsonRequestBehavior.AllowGet);
        }
        
        //public ActionResult RoleDetails(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var role = ReadyCall(BusinessOperations.GetRole, id).Data;
        //    return View(role);
        //}


        #endregion


        #region 用户权限
        public ActionResult UserPrivilege(string searchString, string currentFilter, int? page)
        {
            SaveSearchString(ref searchString, ref page, currentFilter);
            CheckPageButtonPrivilege();
            ViewBag.CurrentFilter = searchString;
            ViewBag.MenuPage = "UserPrivilege";
            var userList = ReadyCall(BusinessOperations.GetUserList, searchString).Data as IOrderedQueryable<User>;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(userList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UserPrivilegeManager(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = ReadyCall(BusinessOperations.GetUser, id).Data as User;
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewData["ThisUser"] = user;

            IQueryable<Privilege> userPv = (ReadyCall(BusinessOperations.GetUserPrivilege, id).Data as IQueryable<Privilege>);
            Debug.Assert(userPv != null, "没有查询到用户权限");
            int[] userMenuArray = userPv
                .Where(p => p.PrivilegeAccess == (int)PrivilegeAccessStatus.Menu)
                .Select(p => p.PrivilegeAccessValue)
                .ToArray();

            ViewData["UserMenu"] = PrivilegeManager(ref userMenuArray);
            ViewData["UserButton"] = GetMenuButton(userMenuArray, userPv);

            BrowserPrivilege browserPrivilege = GetBrowserPrivilege((int)PrivilegeMasterStatus.UserMaster, id.Value).Data as BrowserPrivilege;
            if (browserPrivilege != null)
            {
                return View(browserPrivilege);
            }
            return View(new BrowserPrivilege 
            { 
                MachinePrivilege = false, 
                CommodPrivilege = false,
                MemberPrivilege = false,
                CouponPrivilege = false 
            });
        }

        [HttpPost]
        public JsonResult GetUserMenuButtonAll(int[] menuidArray, int thisId)
        {
            IQueryable<Privilege> userPv = (ReadyCall(BusinessOperations.GetUserPrivilege, thisId).Data as IQueryable<Privilege>);
            return Json(GetMenuButton(menuidArray, userPv), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUserMenuButton(string menuCode, int thisId)
        {
            IQueryable<Privilege> userPv = (ReadyCall(BusinessOperations.GetUserPrivilege, thisId).Data as IQueryable<Privilege>);
            return Json(GetButtonByAjax(ref userPv, menuCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UserPrivilegeSave(int userId, int[] menuList, int[] btnList, BrowserPrivilege bp)
        {
            MessageAbstract msg = PrivilegeSave(userId, menuList, btnList, "User", bp);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 角色权限
        public ActionResult RolePrivilege(int page = 1)
        {
            CheckPageButtonPrivilege();
            ViewBag.MenuPage = "RolePrivilege";
            const int pageSize = 14;
            var rolePage = ((ReadyCall(BusinessOperations.GetRoleList).Data) as IOrderedQueryable<Role>).ToPagedList(page, pageSize);

            return View(rolePage);
        }


        public ActionResult RolePrivilegeManager(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = ReadyCall(BusinessOperations.GetRole, id).Data as Role;
            if (role == null)
            {
                return HttpNotFound();
            }
            ViewData["ThisRole"] = role;

            IQueryable<Privilege> rolePv = (ReadyCall(BusinessOperations.GetRolePrivilege, id).Data as IQueryable<Privilege>);

            Debug.Assert(rolePv != null, "没有查询到角色权限");
            int[] roleMenuArray = rolePv
                .Where(p => p.PrivilegeAccess == (int)PrivilegeAccessStatus.Menu)
                .Select(p => p.PrivilegeAccessValue)
                .ToArray();

            ViewData["RoleMenu"] = PrivilegeManager(ref roleMenuArray);
            ViewData["RoleButton"] = GetMenuButton(roleMenuArray, rolePv);

            BrowserPrivilege browserPrivilege = GetBrowserPrivilege((int)PrivilegeMasterStatus.RoleMaster, id.Value).Data as BrowserPrivilege;
            if (browserPrivilege != null)
            {
                return View(browserPrivilege);
            }
            return View(new BrowserPrivilege
            {
                MachinePrivilege = false,
                CommodPrivilege = false,
                MemberPrivilege = false,
                CouponPrivilege = false
            });
        }


        [HttpPost]
        public JsonResult RolePrivilegeSave(int roleId, int[] menuList, int[] btnList, BrowserPrivilege bp)
        {
            MessageAbstract msg = PrivilegeSave(roleId, menuList, btnList, "Role", bp);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetRoleMenuButton(string menuCode, int thisId)
        {
            IQueryable<Privilege> rolePv = (ReadyCall(BusinessOperations.GetRolePrivilege, thisId).Data as IQueryable<Privilege>);
            return Json(GetButtonByAjax(ref rolePv, menuCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRoleMenuButtonAll(int[] menuidArray, int thisId)
        {
            IQueryable<Privilege> rolePv = (ReadyCall(BusinessOperations.GetRolePrivilege, thisId).Data as IQueryable<Privilege>);
            return Json(GetMenuButton(menuidArray, rolePv), JsonRequestBehavior.AllowGet);
        }





        #endregion


        #region 权限管理公用方法

        private MessageAbstract GetBrowserPrivilege(int privilegeMaster, int privilegeMasterValue)
        {
            Dictionary<string, object> args = new Dictionary<string, object>
            {
                {"PrivilegeMaster", privilegeMaster},
                {"PrivilegeMasterValue", privilegeMasterValue}
            };
            return ReadyCall(BusinessOperations.GetBrowserPrivilege, args);
        }

        private MessageAbstract PrivilegeSave(int id, int[] menuList, int[] btnList, string type, BrowserPrivilege bp)
        {
            Dictionary<string, object> args = new Dictionary<string, object>
            {
                {"Type", type},
                {"Id", id},
                {"MenuList", menuList},
                {"BtnList", btnList},
                {"BrowserPrivilege", bp}
            };
            return ReadyCall(BusinessOperations.SavePrivilege, args);
        }

        private string GetButtonByAjax(ref IQueryable<Privilege> privilegeList, string menuCode)
        {
            int[] buttonArray = privilegeList
                         .Where(p => p.PrivilegeAccess == (int)PrivilegeAccessStatus.Button)
                         .Select(p => p.PrivilegeAccessValue)
                         .ToArray();

            MessageAbstract msg = ReadyCall(BusinessOperations.GetMenuByMenuCode, menuCode);
            Menu menu = msg.Data as Menu;
            StringBuilder sb = new StringBuilder();
            Debug.Assert(menu != null, "没有找到菜单");
            sb.AppendFormat("<span class='label label-info'><i class='{1}'></i>{0}</span>", menu.MenuName, menu.MenuPic);
            sb.Append("<div class='row-fluid'>");
            sb.AppendFormat("<ul class='tree' menuBtnListCode='{0}'>", menu.MenuNo);
            foreach (var btn in menu.Button)
            {
                sb.AppendFormat(
                    buttonArray.Contains(btn.Id)
                        ? "<li style='float: left;'><a href='javascript:;' onclick='NewButtonClick(this);' class='checkBtn'><div class='checker'><span class='checked'><input checked='checked' name='btnList'  value='{1}' id='{1}' type='checkbox' /></span></div>{0}<i class='{2}'></i></a></li>"
                        : "<li style='float: left;'><a href='javascript:;' onclick='NewButtonClick(this);' class='checkBtn'><div class='checker'><span class=''><input name='btnList'  value='{1}' id='{1}' type='checkbox' /></span></div>{0}<i class='{2}'></i></a></li>",
                    btn.BtnName, btn.Id, btn.BtnIcon);
            }
            sb.Append("</ul>");
            sb.Append("</div>");
            return sb.ToString();
        }

        private string GetMenuButton(int[] menuArray, IQueryable<Privilege> privilegeList)
        {
            IQueryable<Menu> userMenu = (ReadyCall(BusinessOperations.GetMenuByMenuArray, menuArray).Data as IQueryable<Menu>);

            int[] userButtonArray = privilegeList
                .Where(p => p.PrivilegeAccess == (int)PrivilegeAccessStatus.Button)
                .Select(p => p.PrivilegeAccessValue)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            if (menuArray != null && menuArray.Length > 0)
            {
                if (userMenu != null)
                    foreach (var menu in userMenu.ToList())
                    {
                        sb.AppendFormat("<span class='label label-info'><i class='{1}'></i>{0}</span>", menu.MenuName, menu.MenuPic);
                        sb.Append("<div class='row-fluid'>");
                        sb.AppendFormat("<ul class='tree' menuBtnListCode='{0}'>", menu.MenuNo);
                        foreach (var btn in menu.Button)
                        {
                            sb.AppendFormat(
                                userButtonArray.Contains(btn.Id)
                                    ? "<li style='float: left;'><a href='javascript:;' onclick='NewButtonClick(this);' class='checkBtn'><div class='checker'><span class='checked'><input checked='checked' name='btnList'  value='{1}' id='{1}' type='checkbox' /></span></div>{0}<i class='{2}'></i></a></li>"
                                    : "<li style='float: left;'><a href='javascript:;' onclick='NewButtonClick(this);' class='checkBtn'><div class='checker'><span class=''><input name='btnList'  value='{1}' id='{1}' type='checkbox' /></span></div>{0}<i class='{2}'></i></a></li>",
                                btn.BtnName, btn.Id, btn.BtnIcon);
                        }
                        sb.Append("</ul>");
                        sb.Append("</div>");
                    }
            }


            return sb.ToString();
        }

        private string PrivilegeManager(ref int[] menuArray)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class='tree' id='tree_1'>");
            sb.Append(" <li><a href='#' data-role='branch' class='tree-toggle'>系统菜单</a>");
            sb.Append("     <ul class='branch in'>");
            sb.Append("         <li><a href='javascript:;' data-role='leaf'><i class='icon-home'></i>首页</a></li>");

            List<MyMenu> allMenu = HtmlExtensions.AllMenu();
            foreach (MyMenu menu in allMenu)
            {
                sb.AppendFormat("<li><a href='javascript:;' class='tree-toggle'><i class='{1}'></i>{0}</a>", menu.Name, menu.Pic);
                sb.Append(" <ul class='branch in'>");
                foreach (var childMenu in menu.Children)
                {
                    sb.AppendFormat(
                        menuArray.Contains(childMenu.MenuID)
                            ? "<li><a href='javascript:;' menuCode={1}  menuId={2} class='checkMenu'><div class='checker'><span class='checked'><input checked='checked' name='menuList' value='{2}' type='checkbox' /></span></div>{0}</a></li>"
                            : "<li><a href='javascript:;' menuCode={1} menuId={2} class='checkMenu'><input name='menuList' value='{2}' type='checkbox' />{0}</a></li>",
                        childMenu.Name, childMenu.Code, childMenu.MenuID);
                }
                sb.Append("</ul>");
                sb.Append("</li>");
            }
            sb.Append("</ul></li></ul>");
            return sb.ToString();
        }
        #endregion
    }
}