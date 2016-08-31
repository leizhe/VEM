using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using VEM.Business;
using VEM.Model;

namespace VEM.Web.Controllers
{
    //系统设置控制器
    public class SystemController : BaseController
    {
        #region 菜单管理
        public ActionResult MenuManager()
        {
            CheckPageButtonPrivilege();
            var result = ReadyCall(BusinessOperations.GetMenuByMenuParentNo,"m0").Data;
            return View(result);
        }

        public ActionResult MenuCreateEdit(int id)
        {
           
            ViewBag.Pagetitle = "添加主菜单";
            ViewBag.PageMessage = "本页添加系统主菜单";
            if (id>0)
            {
                ViewBag.Pagetitle = "修改主菜单";
                ViewBag.PageMessage = "本页修改系统主菜单";
                return View(ReadyCall(BusinessOperations.GetMenuById,id).Data);
            }
            return View();
        }

        [HttpPost]
        public ActionResult MenuCreateEdit(Menu menu, int id)
        {
            if (id > 0)
            {

                MessageAbstract msg = ReadyCall(BusinessOperations.EditMenu, menu);
                return Json(msg);
            }
            else
            {
                menu.MenuParentNo = "m0";
                menu.MenuUrl = "";
                menu.IsLeaf = false;
                MessageAbstract msg = ReadyCall(BusinessOperations.AddMenu, menu);
                return Json(msg);
            }
           
        }

        [HttpPost]
        public ActionResult ImportIcon()
        {
            Regex reg = new Regex("^\\.(?<name>.*?):before\\s*\\{");
            string filePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "Media\\css\\font-awesome.css";

            List<Icon> iconLst = new List<Icon>();

            StreamReader sr = System.IO.File.OpenText(filePath);
            string nextLine;
            while ((nextLine = sr.ReadLine()) != null)
            {
                Match match = reg.Match(nextLine);
                if (!string.IsNullOrEmpty(match.Groups[1].Value)
                    && !match.Groups[1].Value.Contains("icon-large")
                    && !match.Groups[1].Value.Contains("icon-rotate-")
                    && !match.Groups[1].Value.Contains("icon-flip-"))
                {
                    Icon model = new Icon() {
                        Name = match.Groups[1].Value,
                        Css = "font-awesome"
                    };
                    iconLst.Add(model);

                }
            }
            sr.Close();

            for (int i = 0; i < iconLst.Count; i++) 
            {
                for (int j = iconLst.Count - 1; j > i; j--) 
                {
                    if (iconLst[i] == iconLst[j])
                    {
                        iconLst.RemoveAt(j);
                    }
                }
            }

            return Json(ReadyCall(BusinessOperations.AddIconList, iconLst));
        }


        [HttpPost]
        public JsonResult GetIconList()
        {
            List<Icon> lst = ReadyCall(BusinessOperations.GetIconList).Data as List<Icon>;
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='row-fluid'>");
            sb.Append("<div class='span4'><ul class='unstyled'>");

            Debug.Assert(lst != null, "没有查询到图标");
            for (int i = 0; i < lst.Count; i++)
            {

                sb.AppendFormat("<li><i class={0}></i><a href='javaScript:;'  onclick='SelectThisIcon(this)' id={2}>{1}</a></li>", lst[i].Name, lst[i].Name.Length > 15 ? lst[i].Name.Substring(0, 15) : lst[i].Name, lst[i].Id);

                if ((i + 1) % 20 == 0)
                {
                    sb.Append(" </ul></div><div class='span4'><ul class='unstyled'>");
                }
                if ((i + 1) % 60 == 0)
                {
                    sb.Append("</ul></div></div><div class='row-fluid'><div class='span4'><ul class='unstyled'>");
                }

            }
            sb.Append("</ul></div></div>");
            return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult DelMenu(int? id)
        {
            return Json(ReadyCall(BusinessOperations.DelMenuById,id),JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetChidrenList(string menuParendNo)
        {
            MessageAbstract msg = ReadyCall(BusinessOperations.GetMenuByMenuParentNo, menuParendNo);
            List<Menu> lst = msg.Data as List<Menu>;
            StringBuilder sb = new StringBuilder();
            var mainMenu = ReadyCall(BusinessOperations.GetMenuByMenuNo,menuParendNo).Data as Menu;
            Debug.Assert(mainMenu != null, "没有查询到主菜单");
            sb.AppendFormat(@"<div class='clearfix'>
                            <div class='btn-group'><a href='ChildMenuCreateEdit?id=0&parentMenuNo={1}&ms=MenuManager' class='btn red'>添加<{0}>子菜单</a>
                            </div>
                            <div class='btn-group pull-right'>
                            </div>
                        </div>", mainMenu.MenuName,mainMenu.MenuNo);
            sb.AppendFormat(@"<table class='table table-striped table-bordered table-hover' id='sample_1'>
                            <thead>
                                <tr>
                                    <th>
                                        菜单名称
                                    </th>
                                    <th>
                                        顺序
                                    </th>
                                    <th>
                                        URL
                                    </th>
                                    <th>
                                        是否显示
                                    </th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>");
            Debug.Assert(lst != null, "没有查询到主菜单");
            foreach (var item in lst)
            {
                sb.AppendFormat(@"  <tr class='odd gradeX'>
                                        <td>
                                           {0}
                                        </td>
                                        <td>
                                            {1}
                                        </td>
                                        <td>
                                          {2}
                                        </td>
                                        <td>
                                       ",item.MenuName,item.MenuOrder,item.MenuUrl);
                sb.Append(item.IsVisible
                    ? "<span class='label label-success'>显示</span>"
                    : "<span class='label label-danger'>不显示</span>");
                sb.AppendFormat(@"     </td>
                                        <td>
                                            <a href='ChildMenuCreateEdit?id={1}&parentMenuNo={0}&ms=MenuManager' class='btn mini green'><i class='icon-edit'></i> 修改</a>
                                            <a onclick='delMenu(this);' href='javaScript:;' url='/System/DelMenu/?id={1}' name='btnDelMenu' class='btn mini red  delete-link'><i class='icon-remove'></i>删除</a>
                                        </td>
                                    </tr>
                           ", item.MenuNo,item.Id);
            }
            sb.Append("</tbody></table>");

            return Json(sb.ToString(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChildMenuCreateEdit(int id, string parentMenuNo)
        {
            var mainMenu = ReadyCall(BusinessOperations.GetMenuByMenuNo, parentMenuNo).Data as Menu;

            if (mainMenu != null)
            {
                ViewBag.MenuParentNo = mainMenu.MenuNo;
                ViewBag.Pagetitle = "添加子菜单";
                ViewBag.PageMessage = "本页可以为<" + mainMenu.MenuName + ">添加子菜单";
            
                if (id > 0)
                {
                    ViewBag.Pagetitle = "修改子菜单";
                    ViewBag.PageMessage = "本页可以为<" + mainMenu.MenuName + ">修改子菜单";

                    return View(ReadyCall(BusinessOperations.GetMenuById, id).Data);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChildMenuCreateEdit(Menu menu, int id)
        {
            if (id > 0)
            {
                MessageAbstract msg = ReadyCall(BusinessOperations.EditMenu, menu);
                return Json(msg);
            }
            else
            {
                
                menu.IsLeaf = true;
                MessageAbstract msg = ReadyCall(BusinessOperations.AddMenu, menu);
                return Json(msg);
            }

        }


        #endregion
        
        #region 地域维护

        public ActionResult RegionManager()
        {
            CheckPageButtonPrivilege();
            List<City> cityList=ReadyCall(BusinessOperations.GetCityList).Data as List<City>;
            Debug.Assert(cityList != null, "没找到城市");
            cityList.Remove(cityList[0]);
            return View(cityList);
        }

        public ActionResult CityCreateEdit(int id)
        {
            ViewBag.Pagetitle = "添加城市";
            if (id > 0)
            {
                ViewBag.Pagetitle = "修改城市";
                return View(ReadyCall(BusinessOperations.GetCityById, id).Data);
            }
            return View();
        }

        [HttpPost]
        public ActionResult CityCreateEdit(City city, int id)
        {
            if (id > 0)
            {

                MessageAbstract msg = ReadyCall(BusinessOperations.EditCity, city);
                return Json(msg);
            }
            else
            {
                MessageAbstract msg = ReadyCall(BusinessOperations.AddCity, city);
                return Json(msg);
            }
        }

        public JsonResult DelCity(int? id)
        {
            return Json(ReadyCall(BusinessOperations.DelCity, id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DelDistrict(int? id)
        {
            return Json(ReadyCall(BusinessOperations.DelDistrict, id), JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult DistrictCreate(int? cityId)
        {
            City city = ReadyCall(BusinessOperations.GetCityById, cityId).Data as City;
            Debug.Assert(city != null, "没找到城市");
            ViewBag.Pagetitle = city.CityName ;
            ViewBag.cityId = cityId;
            return View();
        }

        [HttpPost]
        public ActionResult DistrictCreate(District district,int cityId)
        {
            Dictionary<string, object> args = new Dictionary<string, object>
            {
                {"District", district},
                {"CityId", cityId}
            };

            return Json(ReadyCall(BusinessOperations.AddDistrict, args));
        }


        [HttpPost]
        public ActionResult DistrictEdit(int did, string dname)
        {
            District model=new District(){
                Id=did,
                DistrictName = dname
            };
            return Json(ReadyCall(BusinessOperations.EditDistrict,model),JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 系统日志
        //public ActionResult SystemLog()
        //{
        //    return View();
        //}
        #endregion

    }
}