using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using VEM.Business.Exceptions.PrivilegeException;
using VEM.Business.Util;
using VEM.Model;

namespace VEM.Business.Business
{
    public class PrivilegeBusiness : BusinessAbstract
    {
        public override object DoAction(object obj)
        {
            switch (OperationCode)
            {
                default: return null;
                case BusinessOperations.GetAllMenu: return GetAllMenu();
                case BusinessOperations.GetRoleList: return GetRoleList();
                case BusinessOperations.SaveRole: return SaveRole(obj);
                case BusinessOperations.GetRole: return GetRole(obj);
                case BusinessOperations.AddRole: return AddRole(obj);
                case BusinessOperations.DelRole: return DelRole(obj);
                case BusinessOperations.GetUserList: return GetUserList(obj);
                case BusinessOperations.GetUser: return GetUser(obj);
                case BusinessOperations.SaveUser: return SaveUser(obj);
                case BusinessOperations.DelUser: return DelUser(obj);
                case BusinessOperations.AddUser: return AddUser(obj);
                case BusinessOperations.SetRole: return SetRole(obj);
                case BusinessOperations.ResetPassWord: return ResetPassWord(obj);
                case BusinessOperations.GetMenuByMenuCode: return GetMenuByMenuCode(obj);
                case BusinessOperations.SavePrivilege: return SavePrivilege(obj);
                case BusinessOperations.GetUserPrivilege: return GetUserPrivilege(obj);
                case BusinessOperations.GetMenuByMenuArray: return GetMenuByMenuArray(obj);
                case BusinessOperations.GetRolePrivilege: return GetRolePrivilege(obj);
                case BusinessOperations.GetMenuCodeByMenuIdList: return GetMenuCodeByMenuIdList(obj);
                case BusinessOperations.GetMyButtonStringByMenuNo: return GetMyButtonStringByMenuNo();
                case BusinessOperations.GetBrowserPrivilege: return GetBrowserPrivilege(obj);
            }
        }

        private object GetBrowserPrivilege(object obj)
        {
            Dictionary<string, object> args = obj as Dictionary<string, object>;
            Debug.Assert(args != null, "参数为空");
            var privilegeMaster = Convert.ToInt32(args["PrivilegeMaster"]);
            var privilegeMasterValue = Convert.ToInt32(args["PrivilegeMasterValue"]);
            return
                DataContext.BrowserPrivilegeSet.FirstOrDefault(
                    p => p.PrivilegeMaster == privilegeMaster && p.PrivilegeMasterValue == privilegeMasterValue);
        }

        private object GetMyButtonStringByMenuNo()
        {
            StringBuilder sb = new StringBuilder();
            ICollection<Button> listButton = DataContext.ButtonSet.ToArray();

            int[] userButtonPrivilege = DataContext.PrivilegeSet
                .Where(p =>
                    p.PrivilegeMaster == (int)PrivilegeMasterStatus.UserMaster &
                    p.PrivilegeMasterValue == LogedInUser.Id &
                    p.PrivilegeAccess == (int)PrivilegeAccessStatus.Button
                   )
                .Select(p => p.PrivilegeAccessValue)
                .ToArray();

            List<int> userRoleIdList = LogedInUser.UserRole.Select(p => p.Role.Id).ToList();

            int[] roleButtonPrivlege = DataContext.PrivilegeSet.Where(p =>
                    p.PrivilegeMaster == (int)PrivilegeMasterStatus.RoleMaster &
                    userRoleIdList.Contains(p.PrivilegeMasterValue) &
                    p.PrivilegeAccess == (int)PrivilegeAccessStatus.Button
                 )
                .Select(p => p.PrivilegeAccessValue)
                .ToArray();

            foreach (var btn in listButton)
            {
                if (userButtonPrivilege.Contains(btn.Id) || roleButtonPrivlege.Contains(btn.Id))
                {

                    sb.Append(btn.BtnNo + ",");
                }
            }
            return sb.ToString();
        }

        private object GetMenuCodeByMenuIdList(object obj)
        {
            List<int> menuIdList = obj as List<int>;
            return DataContext.MenuSet.Where(p => menuIdList.Contains(p.Id)).Select(p => p.MenuParentNo);
        }

        private object GetRolePrivilege(object obj)
        {
            int roleId = Convert.ToInt32(obj);
            return DataContext.PrivilegeSet.Where(p => p.PrivilegeMaster == (int)PrivilegeMasterStatus.RoleMaster & p.PrivilegeMasterValue == roleId);
        }

        private object GetMenuByMenuArray(object obj)
        {
            int[] menuArray = obj as int[];
            return DataContext.MenuSet.Where(p => menuArray.Contains(p.Id));
        }

        private object GetUserPrivilege(object obj)
        {
            int userId = Convert.ToInt32(obj);
            return DataContext.PrivilegeSet.Where(p => p.PrivilegeMaster == (int)PrivilegeMasterStatus.UserMaster & p.PrivilegeMasterValue == userId);
        }

        private object SavePrivilege(object obj)
        {

            Dictionary<string, object> args = obj as Dictionary<string, object>;
            if (args != null)
            {
                string type = args["Type"].ToString();
                int id = Convert.ToInt32(args["Id"]);
                int[] menuList = args["MenuList"] as int[];
                int[] btnList = args["BtnList"] as int[];

                int privilegeMaster = (int)PrivilegeMasterStatus.UserMaster;
                if (type.Equals("Role")) privilegeMaster = (int)PrivilegeMasterStatus.RoleMaster;

                IQueryable<Privilege> privilegeList = DataContext.PrivilegeSet.Where(p => p.PrivilegeMaster == privilegeMaster & p.PrivilegeMasterValue == id);
                DataContext.PrivilegeSet.RemoveRange(privilegeList);

                List<Privilege> lst = new List<Privilege>();
                if (menuList != null && menuList.Length > 0)
                {
                    lst.AddRange(menuList.Select(menuId => new Privilege()
                    {
                        PrivilegeMaster = privilegeMaster, PrivilegeMasterValue = id, PrivilegeAccess = (int) PrivilegeAccessStatus.Menu, PrivilegeAccessValue = menuId
                    }));
                }
                if (btnList != null && btnList.Length > 0)
                {
                    lst.AddRange(btnList.Select(btnId => new Privilege()
                    {
                        PrivilegeMaster = privilegeMaster, PrivilegeMasterValue = id, PrivilegeAccess = (int) PrivilegeAccessStatus.Button, PrivilegeAccessValue = btnId
                    }));
                }

                DataContext.PrivilegeSet.AddRange(lst);

                BrowserPrivilege bp = args["BrowserPrivilege"] as BrowserPrivilege;
                if (bp != null && bp.Id > 0)
                {
                    BrowserPrivilege browserPrivilege = DataContext.BrowserPrivilegeSet.Find(bp.Id);
                    if (browserPrivilege != null)
                    {
                        browserPrivilege.PrivilegeMaster = privilegeMaster;
                        browserPrivilege.PrivilegeMasterValue = id;
                        browserPrivilege.MachinePrivilege = bp.MachinePrivilege;
                        browserPrivilege.CommodPrivilege = bp.CommodPrivilege;
                        browserPrivilege.MemberPrivilege = bp.MemberPrivilege;
                        browserPrivilege.CouponPrivilege = bp.CouponPrivilege;
                    }
                }
                else
                {
                    BrowserPrivilege newBrowserPrivilege = new BrowserPrivilege
                    {
                        PrivilegeMaster = privilegeMaster,
                        PrivilegeMasterValue = id
                    };
                    if (bp != null)
                    {
                        newBrowserPrivilege.MachinePrivilege = bp.MachinePrivilege;
                        newBrowserPrivilege.CommodPrivilege = bp.CommodPrivilege;
                        newBrowserPrivilege.MemberPrivilege = bp.MemberPrivilege;
                        newBrowserPrivilege.CouponPrivilege = bp.CouponPrivilege;
                    }
                    DataContext.BrowserPrivilegeSet.Add(newBrowserPrivilege);
                }
            }

            DataContext.SaveChanges();

            return true;
        }

        private object GetMenuByMenuCode(object obj)
        {
            string menuNo = Convert.ToString(obj);
            return DataContext.MenuSet.FirstOrDefault(p => p.MenuNo.Equals(menuNo));
        }

        private object ResetPassWord(object obj)
        {
            int id = Convert.ToInt32(obj);
            User user = DataContext.PersonSet.OfType<User>().FirstOrDefault(p => p.Id == id);
            Debug.Assert(user != null, "没有找到用户");
            user.LoginPassword = GetMd5Hash("123456");
            DataContext.Entry(user).State = EntityState.Modified;
            DataContext.SaveChanges();
            return true;
        }

        private object SetRole(object obj)
        {
            Dictionary<string, object> args = obj as Dictionary<string, object>;
            if (args != null)
            {
                int userId = Convert.ToInt32(args["UserId"]);
                int[] roleList = args["RoleList"] as int[];
                User user = DataContext.PersonSet.OfType<User>().FirstOrDefault(p => p.Id == userId);

                if (user != null)
                {
                    foreach (var rr in user.UserRole.ToList())
                    {
                        user.UserRole.Remove(rr);
                        DataContext.UserRoleSet.Remove(rr);
                    }

                    ICollection<UserRole> rs = new List<UserRole>();
                    if (roleList != null)
                        foreach (int r in roleList)
                        {
                            UserRole re = new UserRole
                            {
                                User = user,
                                Role = DataContext.RoleSet.Find(r)
                            };
                            rs.Add(re);
                        }
                    DataContext.UserRoleSet.AddRange(rs);
                    user.UserRole = rs;
                    DataContext.Entry(user).State = EntityState.Modified;
                }
            }
            DataContext.SaveChanges();
            return null;
        }

        private object GetUser(object obj)
        {
            int id = Convert.ToInt32(obj);
            return DataContext.PersonSet.OfType<User>().FirstOrDefault(p => p.Id == id);
        }

        private object DelUser(object obj)
        {
            int id = Convert.ToInt32(obj);
            User user = DataContext.PersonSet.OfType<User>().FirstOrDefault(p => p.Id == id);
            Debug.Assert(user != null, "没有找到该用户");
            DataContext.UserRoleSet.RemoveRange(user.UserRole);

            DataContext.PersonSet.Remove(user);
            DataContext.SaveChanges();
            return true;
        }

        private object SaveUser(object obj)
        {
            Dictionary<string, object> args = obj as Dictionary<string, object>;
            if (args != null)
            {
                User o = args["Model"] as User;
                User user = DataContext.PersonSet.OfType<User>().FirstOrDefault(p => p.Id == o.Id);
                if (user != null)
                {
                    if (o != null)
                    {
                        user.Name = o.Name;
                        if (o.LoginName != null) user.LoginName = o.LoginName;
                        user.Email = o.Email;
                        user.Address = o.Address;
                        user.Tel = o.Tel;
                        user.CompanyName = o.CompanyName;
                        if (o.PicUrl != null) user.PicUrl = o.PicUrl;
                        if (o.IsEnabled != null) user.IsEnabled = o.IsEnabled;
                    }
                    user.ModifyUserID = LogedInUser.Id;
                    user.ModifyDate = DateTime.Now;
                    int cityId = Convert.ToInt32(args["CityId"]);
                    if (cityId > 0) user.City = DataContext.CitySet.Find(cityId);
                    int districtId = Convert.ToInt32(args["DistrictId"]);
                    if (districtId > 0) user.District = DataContext.DistrictSet.Find(districtId);
                    DataContext.Entry(user).State = EntityState.Modified;
                }
            }
            DataContext.SaveChanges();
            return true;
        }

        private object AddUser(object obj)
        {


            Dictionary<string, object> args = obj as Dictionary<string, object>;

            if (args != null)
            {
                User user = args["Model"] as User;

                if (DataContext.PersonSet.OfType<User>().FirstOrDefault(p => p.LoginName == user.LoginName) != null)
                {
                    throw new AccountExistedException();
                }
                //if (DataContext.UserSet.FirstOrDefault(p => p.Email == user.Email) != null)
                //{
                //    throw new EmailExistedException();
                //}
                if (DataContext.PersonSet.OfType<User>().FirstOrDefault(p => p.Tel == user.Tel) != null)
                {
                    throw new TelExistedException();
                }

                int cityId = Convert.ToInt32(args["CityId"]);
                if (cityId > 0) if (user != null) user.City = DataContext.CitySet.Find(cityId);
                int districtId = Convert.ToInt32(args["DistrictId"]);
                if (districtId > 0) if (user != null) user.District = DataContext.DistrictSet.Find(districtId);

                if (user != null)
                {
                    user.LoginPassword = GetMd5Hash(user.LoginPassword);
                    user.PicUrl = "/UpImage/User/default.jpg";
                    user.CreateUserID = LogedInUser.Id;
                    user.CreateDate = DateTime.Now;
                    DataContext.PersonSet.Add(user);
                }
            }
            DataContext.SaveChanges();
            return true;
        }

        private object GetUserList(object obj)
        {
            string searchString = Convert.ToString(obj);
            var ul = from t in DataContext.PersonSet.OfType<User>() select t;
            if (!string.IsNullOrEmpty(searchString))
            {
                ul = ul.Where(p => p.LoginName.Contains(searchString) || p.Tel.Contains(searchString));
            }
            return ul.OrderByDescending(p => p.Id);
        }

        private object GetAllMenu()
        {
            return DataContext.MenuSet.Where(m => m.IsVisible).ToList();
        }

        private object DelRole(object obj)
        {
            int id = Convert.ToInt32(obj);
            Role role = new Role() { Id = id };
            DataContext.Entry(role).State = EntityState.Deleted;
            DataContext.SaveChanges();
            return true;

        }

        private object AddRole(object obj)
        {
            DataContext.RoleSet.Add((Role)obj);
            DataContext.SaveChanges();
            return true;
        }

        private object GetRole(object obj)
        {
            int id = Convert.ToInt32(obj);
            return DataContext.RoleSet.Find(id);
        }

        private object SaveRole(object obj)
        {
            var o = obj as Role;
            if (o != null)
            {
                Role role = DataContext.RoleSet.Find(o.Id);
                role.RoleName = o.RoleName;
                role.IsEnable = o.IsEnable;
                role.ModifyUserID = LogedInUser.Id;
                role.ModifyDate = DateTime.Now;
                DataContext.Entry(role).State = EntityState.Modified;
            }
            DataContext.SaveChanges();
            return true;
        }

        private object GetRoleList()
        {
            return DataContext.RoleSet.OrderByDescending(p => p.Id);
        }

    }
}
