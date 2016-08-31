using System.IO;
using System.Web;
using System.Web.Mvc;
using VEM.Business;
using VEM.Business.Message;
using VEM.Business.Util;
using VEM.Model;
using VEM.Web.Core;

namespace VEM.Web.Controllers
{
    public class BaseController : Controller
    {

        private User _currentUser;
        protected User CurrentUser
        {
            get
            {
                if (_currentUser != null)
                    return _currentUser;
                string userid = User.Identity.Name;
                if (string.IsNullOrEmpty(userid))
                    return null;
                _currentUser = ReadyCall(BusinessOperations.GetLogedInUser).Data as User;
                return _currentUser;
            }
        }

        protected MessageAbstract ReadyCall(int op, object obj)
        {
            return DataService.Call(op, obj);
            
        }
        protected MessageAbstract ReadyCall(int op)
        {
            return DataService.Call(op, null);
        }

        protected void CheckPageButtonPrivilege()
        {
            string buttonString = ReadyCall(BusinessOperations.GetMyButtonStringByMenuNo).Data.ToString();
            ViewBag.js = "<script>jQuery(document).ready(function () {PageButton.init('" + buttonString + "');});</script>";
        }

        protected void SaveSearchString(ref string searchString, ref int? page, string currentFilter)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
        }

        protected ActionResult UploadImg(HttpPostedFileBase uploadImage,string typeStr)
        {
            var userPicUrl = "";

            if (null != uploadImage && uploadImage.ContentLength > 0 && Path.GetExtension(uploadImage.FileName) != null)
            {
                userPicUrl = HtmlExtensions.UploadFile(uploadImage, typeStr);
            }

            return Json(new UtilMessage() { Data = userPicUrl, StatusCode = (int)StatusCode.操作成功 }, JsonRequestBehavior.AllowGet);
        }

       
    }
}