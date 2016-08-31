using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VEM.Business;
using VEM.Model;
using PagedList;
using System.Net;
using VEM.Business.Util;

namespace VEM.Web.Controllers
{
    public class MemberController : BaseController
    {
        #region 会员管理
        public ActionResult MemberIndex(string searchString, string currentFilter, int? page)
        {
            SaveSearchString(ref searchString, ref page, currentFilter);
            CheckPageButtonPrivilege();

            ViewBag.CurrentFilter = searchString;
            ViewBag.MenuPage = "MemberIndex";
            var userList = ReadyCall(BusinessOperations.GetMemberList, searchString).Data as IOrderedQueryable<Member>;
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(userList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MemberCreateEdit(int id)
        {
            ViewBag.Pagetitle = "添加会员";
            if (id > 0)
            {
                ViewBag.Pagetitle = "修改会员";
                return View(ReadyCall(BusinessOperations.GetMemberById, id).Data);
            }
            return View();
        }

        [HttpPost]
        public ActionResult MemberCreateEdit(Member model, int id)
        {

            if (id > 0)
            {

                MessageAbstract msg = ReadyCall(BusinessOperations.SaveMember, model);
                return Json(msg);
            }
            else
            {

                MessageAbstract msg = ReadyCall(BusinessOperations.AddMember, model);
                return Json(msg);
            }
        }

        public ActionResult MemberDelete(int id)
        {
            return Json(ReadyCall(BusinessOperations.DelMember, id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResetPassWord(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return Json(ReadyCall(BusinessOperations.ResetMemberPassWord, id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMemberCoupon(int id)
        {

            var couponLst = ReadyCall(BusinessOperations.GetMemberCouponByMemberId, id).Data as IOrderedQueryable<MemberCoupon>;

            ViewBag.thisMember = ReadyCall(BusinessOperations.GetMemberById, id).Data as Member;
            return PartialView("MemberCoupon", couponLst);
            
        }


        public ActionResult GetMemberAddCoupon(int id)
        {
            var couponlst =ReadyCall(BusinessOperations.GetCouponList).Data as IOrderedQueryable<Coupon>;
            var couponAvailableLst = from t in couponlst where t.Status && t.EndTime > DateTime.Now select t;
            ViewBag.MemberId = id;
            return PartialView("MemberAddCoupon", couponAvailableLst);
        }

        public ActionResult AddMemberCoupon(FormCollection form)
        {
            int memberId = Convert.ToInt32(form["MemberId"]);
            List<MemberCoupon> lst = new List<MemberCoupon>();

            foreach (string couponId in form)
            {
                if (!couponId.Equals("MemberId") && !string.IsNullOrEmpty(form.Get(couponId)))
                {
                    int count = Convert.ToInt32(form.Get(couponId));
                    for (int i = 0; i < count; i++)
                    {
                        MemberCoupon model = new MemberCoupon();
                        int cid = Convert.ToInt32(couponId);
                        model.Coupon = new Coupon() { Id = cid };
                        model.Member = new Member() { Id = memberId };
                        model.PayStatus = true;
                        lst.Add(model);
                    }
                  
                }
            }

            return Json(ReadyCall(BusinessOperations.AddMemberCoupon, lst));
        }

        public ActionResult MemberCouponDelete(int id)
        {
            return Json(ReadyCall(BusinessOperations.DelMemberCouponById, id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult MemberPayRecord(int? page, int? id)
        {

            Member member=ReadyCall(BusinessOperations.GetMemberById, id).Data as Member;
            if (member != null) ViewBag.MemberName = string.Format("会员{0}的充值消费记录",member.LoginName);

            Dictionary<string, object> args = new Dictionary<string, object> {{"MemberId", id}};


            var memberPayRecord = ReadyCall(BusinessOperations.GetMemberPayRecord, args).Data as IOrderedEnumerable<PayRecord>;

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(memberPayRecord.ToPagedList(pageNumber, pageSize));
        }
        

        #endregion


        #region 优惠券管理

        public ActionResult CouponIndex(string searchString, string currentFilter, int? page)
        {
            SaveSearchString(ref searchString, ref page, currentFilter);
            CheckPageButtonPrivilege();

            ViewBag.CurrentFilter = searchString;
            ViewBag.MenuPage = "CouponIndex";
            var userList = ReadyCall(BusinessOperations.GetCouponList, searchString).Data as IOrderedQueryable<Coupon>;
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(userList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CouponCreateEdit(int id)
        {
           
            if (id > 0)
            {
                ViewBag.Pagetitle = "修改优惠券";
                Coupon coupon = ReadyCall(BusinessOperations.GetCouponById, id).Data as Coupon;
                if (coupon != null)
                {
                    ViewBag.CouponTypeSelect = GetCouponTypeSelectItem(coupon.Type);
                    return View(coupon);
                }
            }else
	        {
                 ViewBag.Pagetitle = "添加优惠券";
                 ViewBag.CouponTypeSelect = GetCouponTypeSelectItem(0);
	        }
            return View();
        }

        [HttpPost]
        public ActionResult CouponCreateEdit(Coupon model, int id, int couponTypeSelect)
        {
            model.Type = couponTypeSelect;
            if (id > 0)
            {

                MessageAbstract msg = ReadyCall(BusinessOperations.SaveCoupon, model);
                return Json(msg);
            }
            else
            {
                

                MessageAbstract msg = ReadyCall(BusinessOperations.AddCoupon, model);
                return Json(msg);
            }
        }


        public ActionResult CouponDelete(int id)
        {
            return Json(ReadyCall(BusinessOperations.DelCoupon, id), JsonRequestBehavior.AllowGet);
        }


        private List<SelectListItem> GetCouponTypeSelectItem(int? selectValue)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            SelectListItem deaultOption = new SelectListItem() { Text="请选择优惠券类型",Value="0"};
            int aValue = (int)CouponType.现金券;
            int bValue = (int)CouponType.打折券;
            SelectListItem a = new SelectListItem() { Text = "现金券", Value = aValue.ToString() };
            SelectListItem b = new SelectListItem() { Text = "打折券", Value = bValue.ToString() };
            lst.Add(deaultOption);
            lst.Add(a);
            lst.Add(b);
            foreach (var item in lst)
            {
                if (string.IsNullOrEmpty(item.Value)&&!selectValue.HasValue)
                {
                    item.Selected = true;
                }
                else
                {
                    if (item.Value.Equals(selectValue.ToString()))
                    {
                        item.Selected = true;
                    }
                }
            }
            return lst;
        }

        #endregion


    }
}