using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using VEM.Business;
using VEM.Model;
using VEM.Web.Core;
namespace System.Web.Mvc
{
    public static class HtmlExtensions
    {
        public static string Truncate(this HtmlHelper helper, string input, int length)
        {
            if (input.Length <= length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length) + "...";
            }
        }
        public static List<MyMenu> AllMenu()
        {
            List<VEM.Model.Menu> menuList = DataService.Call(BusinessOperations.GetAllMenu,null).Data as List<VEM.Model.Menu>;
            var menus = from x in menuList.Where(m => m.IsLeaf == false).OrderBy(m=>m.MenuOrder)
                        select new MyMenu
                        {
                            MenuID=x.Id,
                            Code = x.MenuNo,
                            Name = x.MenuName,
                            Url = x.MenuUrl,
                            Pic = x.MenuPic,
                            Children = (from t in menuList.Where(m => m.IsLeaf == true && m.MenuParentNo == x.MenuNo).OrderBy(m => m.MenuOrder)
                                        select new LeftMenu
                                        {
                                            MenuID = t.Id,
                                            Code = t.MenuNo,
                                            Name = t.MenuName,
                                            Url = t.MenuUrl
                                        }).ToList()
                        };
            return menus.ToList();
        }
        public static string UploadFile(HttpPostedFileBase file, string fileType)
        {
            string filesPath = HttpContext.Current.Server.MapPath(GetPath(fileType));
            if (null == file) return "";
            if (!(file.ContentLength > 0)) return "";
            string fileName = Guid.NewGuid().ToString();
            string fileExt = Path.GetExtension(file.FileName);
            if (null == fileExt) return "";
            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory(filesPath);
            }
            string path = filesPath + fileName + fileExt;
            file.SaveAs(path);
            return GetPath(fileType) + fileName + fileExt;
        }
        private static string GetPath(string fileType)
        {
            string FilesPath = "/UpImage/";

            switch (fileType)
            {
                case "CommodPic": FilesPath = "/UpImage/Commod/"; break;
                case "MachinePic": FilesPath = "/UpImage/Machine/"; break;
                case "UserPic": FilesPath = "/UpImage/User/"; break;
            }
            return FilesPath;
        }
        public static void DeleteFile(string path)
        {
            if (path == "") return;
            if (File.Exists(path))
            {
                File.Delete(Path.GetFullPath(path));
            }
        }
        public static dynamic GetCitySelectList(int selected)
        {
            return new SelectList(DataService.Call(BusinessOperations.GetCityList, null).Data as IEnumerable<City>, 
                "Id", "CityName", selected);
        }
        public static dynamic GetDistrictSelectList(int selected, int CityId)
        {
            return new SelectList(DataService.Call(BusinessOperations.GetDistrictList, CityId).Data as IEnumerable<District>, "Id", "DistrictName", selected);
        }
        public static List<SelectListItem> GetDistrictByCityId(int cityId)
        {
            IEnumerable<District> result = DataService.Call(BusinessOperations.GetDistrictList, cityId).Data as IEnumerable<District>;
            List<SelectListItem> lst = new List<SelectListItem>();
            SelectListItem DeaultOption = new SelectListItem();
            foreach (var item in result)
            {
                SelectListItem model = new SelectListItem();
                model.Text = item.DistrictName;
                model.Value = item.Id.ToString();
                lst.Add(model);
            }
            return lst;

        }
        internal static dynamic GetMachineModelSelectList(int selected)
        {
            IEnumerable<MachineModel> messageData = DataService.Call(BusinessOperations.GetMachineModelList, null).Data as IEnumerable<MachineModel>;
            List<MachineModel> modelList = messageData.ToList();
            modelList.Add(new MachineModel() { Id=0,Name="请选择机器型号"});
            return new SelectList(modelList, "Id", "Name", selected);
        }

        public static string GetUserNameById(int? id)
        {
            User user = DataService.Call(BusinessOperations.GetUser, id).Data as User;
            return user.Name;
        }
      
        public static String ToEnumString(this int value, Type enumType)
        {
            NameValueCollection nvc = GetEnumStringFromEnumValue(enumType);
            return nvc[value.ToString()];
        }

        public static NameValueCollection GetEnumStringFromEnumValue(Type enumType)
        {
            NameValueCollection nvc = new NameValueCollection();
            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            string strText = string.Empty;
            string strValue = string.Empty;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    strValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    nvc.Add(strValue, field.Name);
                }
            }
            return nvc;
        }

        public static string SerializeObject(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static T DeserializeObject<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static List<T> JSONStringToList<T>(this string JsonStr)
        {

            JavaScriptSerializer Serializer = new JavaScriptSerializer();

            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);

            return objs;

        }


  
    }

    public class MyMenu
    {
        public int MenuID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Pic { get; set; }
        public List<LeftMenu> Children { set; get; }
    }

    public class LeftMenu
    {
        public int MenuID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}