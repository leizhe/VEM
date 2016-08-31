using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Security;
using VEM.Business.Exceptions.AccountException;
using VEM.Model;

namespace VEM.Business.Business
{
    public class AccountBusiness : BusinessAbstract
    {
        public override object DoAction(object obj)
        {
            switch (OperationCode)
            {
                default: return null;
                case BusinessOperations.SignIn: return SignIn(obj);
                case BusinessOperations.SignOut: return SignOut();
                case BusinessOperations.GetLogedInUser: return GetLogedInUser();
                case BusinessOperations.GetCookie: return LoginGetCookie();
                case BusinessOperations.DelCookie: return LoginDelCookie();
                case BusinessOperations.ChangeUserInfo: return ChangeUserInfo(obj);
                case BusinessOperations.ChangePassword: return ChangePassword(obj);
                case BusinessOperations.ChangeUserPic: return ChangeUserPic(obj);
                case BusinessOperations.GetCityList: return GetCityList();
                case BusinessOperations.GetDistrictList: return GetDistrictList(obj);
            }
        }

        private object SignOut()
        {
            FormsAuthentication.SignOut();
            return null;
        }

        private object ChangeUserPic(object obj)
        {
            string userPicUrl = Convert.ToString(obj);
            User user = DataContext.PersonSet.OfType<User>().FirstOrDefault(p=>p.Id==LogedInUser.Id);
            if (user != null)
            {
                user.PicUrl = userPicUrl;
                DataContext.Entry(user).State = EntityState.Modified;
            }
            DataContext.SaveChanges();
            return true;

        }

        private object GetDistrictList(object obj)
        {
            List<District> lst = new List<District>();
            int cityId = Convert.ToInt32(obj);
            if (cityId>0)
            {
                lst = DataContext.DistrictSet.Where(p => p.City.Id == cityId).ToList();
            }
            District model = new District {DistrictName = "请选择区县"};
            lst.Insert(0, model);
            return lst;
        }

        private object GetCityList()
        {

            List<City> lst = DataContext.CitySet.ToList();
            City model = new City {CityName = "请选择城市"};
            lst.Insert(0, model);
            return lst;
           
        }

        private object ChangeUserInfo(object obj)
        {
            Dictionary<string, object> args = obj as Dictionary<string, object>;

            if (args != null)
            {
                User o = args["Model"] as User;
                User user = DataContext.PersonSet.OfType<User>().FirstOrDefault(p => p.Id == o.Id);
                Debug.Assert(user != null, "用户不存在");
                if (o != null)
                {
                    user.Name = o.Name;
                    if (o.LoginName != null) user.LoginName = o.LoginName;
                    user.Email = o.Email;
                    user.Address = o.Address;
                    user.Tel = o.Tel;
                    user.CompanyName = o.CompanyName;
                    int cityId = Convert.ToInt32(args["CityId"]);
                    if (cityId > 0) user.City = DataContext.CitySet.Find(cityId);
                    int districtId = Convert.ToInt32(args["DistrictId"]);
                    if (districtId > 0) user.District = DataContext.DistrictSet.Find(districtId);
                    if (o.PicUrl != null) user.PicUrl = o.PicUrl;
                    if (o.IsEnabled != null) user.IsEnabled = o.IsEnabled;
                }
                user.ModifyUserID = LogedInUser.Id;
                user.ModifyDate = DateTime.Now;
                DataContext.Entry(user).State = EntityState.Modified;
            }
            DataContext.SaveChanges();
            return true;
        }

        private object ChangePassword(object obj)
        {
            Dictionary<string, string> args = obj as Dictionary<string, string>;
            Debug.Assert(args != null, "参数为空");
            string oldPass = GetMd5Hash(args["OldPass"]);
            string newPass = GetMd5Hash(args["NewPass"]);
            int userId = Convert.ToInt32(args["UserId"]);
            User webUser =
                DataContext.PersonSet.OfType<User>().FirstOrDefault(
                    u => u.LoginPassword == oldPass && u.Id == userId);
            if (webUser == null) throw new WrongOldPasswordException();

            webUser.LoginPassword = newPass;
            DataContext.Entry(webUser).State = EntityState.Modified;
            DataContext.SaveChanges();
            return true;

        }

        private object GetLogedInUser()
        {
            return LogedInUser;
        }

        private object LoginDelCookie()
        {
            return DelCookie(CookieId);
        }

        private object LoginGetCookie()
        {
            object cookieId = GetCookie(CookieId);
            if (cookieId!=null)
            {
                int userId = Convert.ToInt32(cookieId);
                User webUser =
                   DataContext.PersonSet.OfType<User>().FirstOrDefault(p => p.Id == userId);
                return webUser;
            }
            return null;


        }

        private object SignIn(object obj)
        {
            Dictionary<string, string> args = obj as Dictionary<string, string>;
            Debug.Assert(args != null, "参数为空");
            string userName = args["UserName"];
            string password = GetMd5Hash(args["PassWord"]);
            bool remember = Convert.ToBoolean(args["Remember"]);

            User webUser =
                DataContext.PersonSet.OfType<User>().FirstOrDefault(
                    u => (u.LoginName == userName || u.Tel == userName) && u.LoginPassword == password);

            if (webUser == null)
            {
                throw new WrongTelOrPasswordException();
            }
            else if (webUser.IsEnabled != null && !webUser.IsEnabled.Value)
            {
               throw new AccountNotAvalibleException();
            }
            else
            {
                LogedInUser = new User
                {
                    Id = webUser.Id
                };
                if (remember)
                {
                    SetCookie(CookieId, webUser.Id.ToString(), 30);
                }
                else
                {
                    DelCookie(CookieId);
                }
                return true;
            }
            
        }



    }
}
