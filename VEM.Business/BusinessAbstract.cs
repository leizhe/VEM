using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using VEM.Business.Util;
using VEM.Data;
using VEM.Model;

namespace VEM.Business
{
    public abstract class BusinessAbstract
    {
        public static readonly string CookieId = "USER_ID";

        protected int OperationCode;

        private VemContainer _context;
        public VemContainer DataContext
        {
            get { return _context ?? (_context = new VemContainer()); }
        }

        public abstract object DoAction(object obj);

        public object ProcessOperation(int op, object obj)
        {
            OperationCode = op;
            return DoAction(obj);
        }

        protected User LogedInUser
        {
            get
            {
                int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                User account = DataContext.PersonSet.OfType<User>().FirstOrDefault(p => p.Id == userId);

                return account;
            }
            set
            {
                string userId = value == null ? "-1" : value.Id.ToString();
                FormsAuthentication.SetAuthCookie(userId, false);
            }
        }


        protected bool CheckBrowserPrivilege(int browserPrivilegeType)
        {


            var userBrowserPrivilege = DataContext.BrowserPrivilegeSet
                .FirstOrDefault(p => p.PrivilegeMaster == (int)PrivilegeMasterStatus.UserMaster && p.PrivilegeMasterValue == LogedInUser.Id);

            
            var userRoles=LogedInUser.UserRole.Select(p=>p.Role.Id);
            var roleBrowserPrivilege = DataContext.BrowserPrivilegeSet
                .Where(p => p.PrivilegeMaster == (int)PrivilegeMasterStatus.RoleMaster && userRoles.Contains(p.PrivilegeMasterValue));

         
            switch (browserPrivilegeType)
            {
                case (int)UserBrowserPrivilege.售货机:
                    if (userBrowserPrivilege != null&&userBrowserPrivilege.MachinePrivilege)
                    {
                        return true;
                    }
                    foreach (var item in roleBrowserPrivilege)
                    {
                        if (item.MachinePrivilege)
                        {
                            return true;
                        }
                    }
                    return false;
                case (int)UserBrowserPrivilege.商品:
                    if (userBrowserPrivilege != null && userBrowserPrivilege.CommodPrivilege)
                    {
                        return true;
                    }
                    foreach (var item in roleBrowserPrivilege)
                    {
                        if (item.CommodPrivilege)
                        {
                            return true;
                        }
                    }
                    return false;
                case (int)UserBrowserPrivilege.会员:
                    if (userBrowserPrivilege != null && userBrowserPrivilege.MemberPrivilege)
                    {
                        return true;
                    }
                    foreach (var item in roleBrowserPrivilege)
                    {
                        if (item.MemberPrivilege)
                        {
                            return true;
                        }
                    }
                    return false;
                case (int)UserBrowserPrivilege.优惠券:
                    if (userBrowserPrivilege != null && userBrowserPrivilege.CouponPrivilege)
                    {
                        return true;
                    }
                    foreach (var item in roleBrowserPrivilege)
                    {
                        if (item.CouponPrivilege)
                        {
                            return true;
                        }
                    }
                    return false;
            }
            return false;
        }

        protected bool SetCookie(string strName, string strValue, int strDay)
        {
            try
            {
                HttpCookie cookie = new HttpCookie(strName)
                {
                    Expires = DateTime.Now.AddYears(strDay),
                    Value = strValue
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected static string GetCookie(string strName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie != null)
            {
                return cookie.Value;
            }
            else
            {
                return null;
            }
        }

        protected static bool DelCookie(string strName)
        {
            try
            {
                HttpCookie cookie = new HttpCookie(strName) {Expires = DateTime.Now.AddDays(-1)};
                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected string GetMd5Hash(string input)
        {
            MD5 hash = MD5.Create();
            byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            foreach (byte t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }

    


    }
}

