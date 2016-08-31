using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using VEM.Business.Exceptions.MemberException;
using VEM.Business.Exceptions.PrivilegeException;
using VEM.Business.Util;
using VEM.Model;

namespace VEM.Business.Business
{
    public class MemberBusiness : BusinessAbstract
    {
        public override object DoAction(object obj)
        {
            switch (OperationCode)
            {
                default: return null;
                case BusinessOperations.GetMemberList: return GetMemberList(obj);
                case BusinessOperations.GetMemberById: return GetMemberById(obj);
                case BusinessOperations.AddMember: return AddMember(obj);
                case BusinessOperations.SaveMember: return SaveMember(obj);
                case BusinessOperations.DelMember: return DelMember(obj);
                case BusinessOperations.GetCouponList: return GetCouponList();
                case BusinessOperations.GetCouponById: return GetCouponById(obj);
                case BusinessOperations.AddCoupon: return AddCoupon(obj);
                case BusinessOperations.SaveCoupon: return SaveCoupon(obj);
                case BusinessOperations.DelCoupon: return DelCoupon(obj);
                case BusinessOperations.ResetMemberPassWord: return ResetMemberPassWord(obj);
                case BusinessOperations.GetMemberCouponByMemberId: return GetMemberCouponByMemberId(obj);
                case BusinessOperations.AddMemberCoupon: return AddMemberCoupon(obj);
                case BusinessOperations.DelMemberCouponById: return DelMemberCouponById(obj);
                case BusinessOperations.GetMemberPayRecord: return GetMemberPayRecord(obj);
            }
        }

        private object GetMemberPayRecord(object obj)
        {
            Dictionary<string, object> args = obj as Dictionary<string, object>;
            Debug.Assert(args != null, "参数为空");
            var memberId = (int)args["MemberId"];

            var member = DataContext.PersonSet.OfType<Member>().FirstOrDefault(p => p.Id == memberId);
            if (member != null)
            {
                var memberPayRecord = member.PayRecord;

                return memberPayRecord.OrderByDescending(p => p.Id);
            }
            return null;
        }

        private object DelMemberCouponById(object obj)
        {
            int id = (int)obj;
            MemberCoupon model = DataContext.MemberCouponSet.Find(id);
            DataContext.MemberCouponSet.Remove(model);
            DataContext.SaveChanges();
            return true;
        }

        private object AddMemberCoupon(object obj)
        {
            List<MemberCoupon> lst = obj as List<MemberCoupon>;

            if (lst != null)
            {
                foreach (var mc in lst)
                {
                    mc.Coupon = DataContext.CouponSet.Find(mc.Coupon.Id);
                    mc.Member = DataContext.PersonSet.OfType<Member>().FirstOrDefault(p=>p.Id==mc.Member.Id);
                }
                DataContext.MemberCouponSet.AddRange(lst);
            }
            DataContext.SaveChanges();
            return true;
        }

        private object GetMemberCouponByMemberId(object obj)
        {
            int memberId = (int)obj;
            return DataContext.MemberCouponSet.Where(p => p.Member.Id == memberId);
        }

        private object ResetMemberPassWord(object obj)
        {
            int id = (int)obj;
            Member member = DataContext.PersonSet.OfType<Member>().FirstOrDefault(p=>p.Id==id);
            if (member != null) member.LoginPassword = GetMd5Hash("123456");
            DataContext.SaveChanges();
            return true;
        }

        private object DelCoupon(object obj)
        {
            int id = (int)obj;
            Coupon coupon = DataContext.CouponSet.Find(id);
            if (coupon!=null)
            {
                if (coupon.MemberCoupon.Any()) throw new MemberNotNullException();
            }
            DataContext.CouponSet.Remove(coupon);
            DataContext.SaveChanges();
            return true;
        }

        private object SaveCoupon(object obj)
        {
            Coupon coupon = obj as Coupon;
            DataContext.Entry(coupon).State = EntityState.Unchanged;
            DataContext.Entry(coupon).State = EntityState.Modified;
            DataContext.SaveChanges();
            return true;
        }

        private object AddCoupon(object obj)
        {
            Coupon coupon = obj as Coupon;
            Debug.Assert(coupon != null, "优惠券为空");
            coupon.CreateID = LogedInUser.Id;
            coupon.CreateDate = DateTime.Now;
            DataContext.CouponSet.Add(coupon);
            DataContext.SaveChanges();
            return true;

        }

        private object GetCouponById(object obj)
        {
            int id = (int)obj;
            return DataContext.CouponSet.Find(id);
        }

        private object GetCouponList()
        {
            var coupon=from t in DataContext.CouponSet select t;

            if (!CheckBrowserPrivilege((int)UserBrowserPrivilege.优惠券))
            {
                coupon = coupon.Where(p => p.CreateID == LogedInUser.Id);
            }

            return coupon.OrderByDescending(p=>p.Id);
        }

        private object DelMember(object obj)
        {
            int id = (int)obj;
            Member meber = DataContext.PersonSet.OfType<Member>().FirstOrDefault(p=>p.Id==id);
            if (meber!=null)
            {
                if (meber.MemberCoupon.Any()) DataContext.MemberCouponSet.RemoveRange(meber.MemberCoupon);
                if (meber.PayRecord.Any()) DataContext.PayRecordSet.RemoveRange(meber.PayRecord);
            }
            DataContext.PersonSet.Remove(meber);
            DataContext.SaveChanges();
            return true;
        }

        private object SaveMember(object obj)
        {
            Member member = obj as Member;

            DataContext.Entry(member).State = EntityState.Unchanged;
            DataContext.Entry(member).State = EntityState.Modified;

            DataContext.SaveChanges();
            return true;
        }

        private object AddMember(object obj)
        {
            Member member = obj as Member;
            if (DataContext.PersonSet.OfType<Member>().FirstOrDefault(p=>p.LoginName==member.LoginName)!=null)
            {
                throw new AccountExistedException();
            }
            if (DataContext.PersonSet.OfType<Member>().FirstOrDefault(p => p.Tel == member.Tel) != null)
            {
                throw new TelExistedException();
            }
            if (member != null)
            {
                member.LoginPassword = GetMd5Hash("123456");
                member.PicUrl = "/UpImage/User/default.jpg";
                member.CreateUserID = LogedInUser.Id;
                member.CreateDate = DateTime.Now;
                DataContext.PersonSet.Add(member);
            }
            DataContext.SaveChanges();
            return true;
        }

        private object GetMemberById(object obj)
        {
            int id = (int)obj;
            return DataContext.PersonSet.OfType<Member>().FirstOrDefault(p => p.Id == id);
        }

        private object GetMemberList(object obj)
        {
            string searchString = Convert.ToString(obj);
            var ml = from t in DataContext.PersonSet.OfType<Member>() select t;
            if (!CheckBrowserPrivilege((int)UserBrowserPrivilege.会员))
            {
                ml = ml.Where(p => p.CreateUserID == LogedInUser.Id);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                ml = ml.Where(p => p.LoginName.Contains(searchString) || p.Tel.Contains(searchString));
            }
            return ml.OrderByDescending(p => p.Id);
        }
    }
}
