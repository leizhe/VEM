using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using VEM.Business.Exceptions.SystemException;
using VEM.Model;

namespace VEM.Business.Business
{
    public class SystemBusiness : BusinessAbstract
    {
        public override object DoAction(object obj)
        {
            switch (OperationCode)
            {
                default: return null;
                case BusinessOperations.GetMenuByMenuParentNo: return GetMenuByMenuParentNo(obj);
                case BusinessOperations.AddMenu: return AddMenu(obj);
                case BusinessOperations.GetMenuById: return GetMenuById(obj);
                case BusinessOperations.AddIconList: return AddIconList(obj);
                case BusinessOperations.GetIconList: return GetIconList();
                case BusinessOperations.EditMenu: return EditMenu(obj);
                case BusinessOperations.DelMenuById: return DleMenuById(obj);
                case BusinessOperations.GetMenuByMenuNo: return GetMenuByMenuNo(obj);
                case BusinessOperations.GetCityById: return GetCityById(obj);
                case BusinessOperations.AddCity: return AddCity(obj);
                case BusinessOperations.EditCity: return EditCity(obj);
                case BusinessOperations.DelCity: return DelCity(obj);
                case BusinessOperations.GetDistrictById: return GetDistrictById(obj);
                case BusinessOperations.AddDistrict: return AddDistrict(obj);
                case BusinessOperations.EditDistrict: return EditDistrict(obj);
                case BusinessOperations.DelDistrict: return DelDistrict(obj);
            }
        }
        
        private object DelDistrict(object obj)
        {
            int id = Convert.ToInt32(obj);
            District district = DataContext.DistrictSet.Find(id);
            if (district.Machine.Any()|| district.User.Any())
            {
                throw new DistrictNotNullException();
            }
            DataContext.DistrictSet.Remove(district);
            DataContext.SaveChanges();
            return true;
        }

        private object EditDistrict(object obj)
        {
            District district=obj as District;
            Debug.Assert(district != null, "没找到区县");
            District dModel = DataContext.DistrictSet.Find(district.Id);
            dModel.DistrictName = district.DistrictName;
            DataContext.Entry(dModel).State = EntityState.Modified;
            DataContext.SaveChanges();
            return true;
        }

        private object AddDistrict(object obj)
        {

            Dictionary<string, object> args = obj as Dictionary<string, object>;

            if (args != null)
            {
                District district = args["District"] as District;
                City city = DataContext.CitySet.Find((int)args["CityId"]);
                Debug.Assert(district != null, "没找到区县");
                district.City = city;
                DataContext.Entry(district).State = EntityState.Added;
            }
            DataContext.SaveChanges();
            return true;
        }

        private object GetDistrictById(object obj)
        {
            int id = Convert.ToInt32(obj);
            return DataContext.DistrictSet.Find(id);
        }

        private object DelCity(object obj)
        {
            int id = Convert.ToInt32(obj);
            City city = DataContext.CitySet.Find(id);
            if (city.Machine.Any()||city.District.Any())
            {
                throw new CityNotNullException();
            }
            if (city.District.Count>0)
            {
                DataContext.DistrictSet.RemoveRange(city.District);
            }
            DataContext.CitySet.Remove(city);
            DataContext.SaveChanges();
            return true;
        }

        private object EditCity(object obj)
        {
            City city=obj as City;
            DataContext.Entry(city).State = EntityState.Modified;
            DataContext.SaveChanges();
            return true;
        }

        private object AddCity(object obj)
        {
            City city=obj as City;
            DataContext.CitySet.Add(city);
            DataContext.SaveChanges();
            return true;
        }

        private object GetCityById(object obj)
        {
            int id = Convert.ToInt32(obj);
            return DataContext.CitySet.Find(id);
        }

        private object GetMenuByMenuNo(object obj)
        {
            string menuNo = Convert.ToString(obj);
            return DataContext.MenuSet.FirstOrDefault(p=>p.MenuNo==menuNo);
        }

        private object DleMenuById(object obj)
        {
            int id = Convert.ToInt32(obj);
            Menu menu = DataContext.MenuSet.Find(id);
            
            var childMenu=DataContext.MenuSet.Where(p => p.MenuParentNo.Equals(menu.MenuNo));
            if (childMenu.Any())
            {
                throw new ChildMenuNotNullException();
                //DataContext.MenuSet.RemoveRange(childMenu);
            }
            if (menu.Button.Any())
            {
                throw new ButtonNotNullException();
                //DataContext.ButtonSet.RemoveRange(menu.Button);
            }
            DataContext.MenuSet.Remove(menu);
            DataContext.SaveChanges();
            return true;
        }

        private object EditMenu(object obj)
        {
            Menu menu = obj as Menu;
            if (menu != null && !string.IsNullOrEmpty(menu.MenuPic)) {
                try
                {
                    var iconName = Convert.ToInt32(menu.MenuPic);
                    var icon = DataContext.IconSet.FirstOrDefault(p => p.Id == iconName);
                    if (icon != null)
                        menu.MenuPic = icon.Name;
                }
                catch
                {

                    menu.MenuPic = menu.MenuPic;
                }
              
            }
            else
            {
                if (menu != null) menu.MenuPic = "";
            }
            if (menu != null)
            {
                var model =DataContext.MenuSet.Find(menu.Id);
                model.MenuName = menu.MenuName;
                model.MenuOrder = menu.MenuOrder;
                model.MenuPic = menu.MenuPic;
                model.IsVisible = menu.IsVisible;
                DataContext.Entry(model).State = EntityState.Modified;
            }
            DataContext.SaveChanges();
            return true;
        }

        private object GetIconList()
        {
            return DataContext.IconSet.ToList();
        }

        private object AddIconList(object obj)
        {
            DataContext.IconSet.RemoveRange(DataContext.IconSet);

            List<Icon> iconList=obj as List<Icon>;
            DataContext.IconSet.AddRange(iconList);
            DataContext.SaveChanges();
            return true;
        }

        private object GetMenuById(object obj)
        {
            int menuId = Convert.ToInt32(obj);
            return DataContext.MenuSet.Find(menuId);
        }

        private object AddMenu(object obj)
        {
            Menu menu = obj as Menu;
            Debug.Assert(menu != null, "菜单为空");
            if (!string.IsNullOrEmpty(menu.MenuPic))
            {
                var iconName = Convert.ToInt32(menu.MenuPic);
                var icon = DataContext.IconSet.FirstOrDefault(p => p.Id == iconName);
                if (icon != null)
                    menu.MenuPic = icon.Name;
            }
            else
            {
                menu.MenuPic = "";
            }
            
            menu.MenuNo = "m"+(DataContext.MenuSet.Count()+1);
            DataContext.MenuSet.Add(menu);
            DataContext.SaveChanges();
            return true;
        }

        private object GetMenuByMenuParentNo(object obj)
        {
            string menuParentNo = Convert.ToString(obj);
            return DataContext.MenuSet.Where(p => p.MenuParentNo.Equals(menuParentNo)).ToList();
        }
    }
}
