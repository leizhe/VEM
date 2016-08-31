using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VEM.Business.Util;

namespace VEM.Business.Business
{
    public class MonitorBusiness : BusinessAbstract
    {
        public override object DoAction(object obj)
        {
            switch (OperationCode)
            {
                default: return null;
                case BusinessOperations.GetSalesHistoryList: return GetSalesHistoryList(obj);
                case BusinessOperations.GetShipmentStatusRecordList: return GetShipmentStatusRecordList(obj);
                case BusinessOperations.GetFaultList: return GetFaultList(obj);
                case BusinessOperations.GetErrorCount: return GetErrorCount();
                case BusinessOperations.UpdFaultStatus: return UpdFaultStatus(obj);
                case BusinessOperations.GetReserveCount: return GetReserveCount();
                case BusinessOperations.GetChartMonthSalesHistory: return GetChartMonthSalesHistory(obj);
              
            }
        }

        private object GetChartMonthSalesHistory(object obj)
        {
            bool type=Convert.ToBoolean(obj);

            //var salesHistory = (from t in DataContext.SalesHistorySet select t).ToArray();

            int mon=0;
            if (type)
	        {
                mon=DateTime.Now.Month;
	        }else
	        {
                mon=DateTime.Now.Month-1;
	        }
            
            var salesHistory = DataContext.SalesHistorySet
                .Where(p => p.SalesDate.Year == DateTime.Now.Year && p.SalesDate.Month == mon);

            if (!CheckBrowserPrivilege((int)UserBrowserPrivilege.售货机))
            {
                salesHistory = salesHistory.Where(p=>p.Machine.User.Id==LogedInUser.Id);
                //salesHistory = salesHistory.Where(p => p.Machine.User.Id == LogedInUser.Id).ToArray();
            }
             return salesHistory.ToList();
        }


        private object GetReserveCount()
        {


            var containerRoadLst = DataContext.MachineSet.SelectMany(p => p.ContainerRoad);
            if (!CheckBrowserPrivilege((int)UserBrowserPrivilege.售货机))
            {
                containerRoadLst = containerRoadLst.Where(p => p.Machine.User.Id == LogedInUser.Id);
            }

            int result = 0;
            foreach (var containerRoad in containerRoadLst)
            {
                result += containerRoad.ReamainderCount ?? 0;
            }
            return result;
        }

        private object UpdFaultStatus(object obj)
        {
            int id = (int)obj;
            var model = DataContext.FaultSet.Find(id);

            if (model.Status == (int)FaultStatus.故障发生)
            {
                model.Status = (int)FaultStatus.故障解除;
                model.SucceedTime = DateTime.Now;
            }
            else{
                model.Status = (int)FaultStatus.故障发生;
                model.SucceedTime = null;

            }
            DataContext.SaveChanges();
            return true;
        }

        private object GetErrorCount()
        {
            var r = from t in DataContext.FaultSet select t;

            if (!CheckBrowserPrivilege((int)UserBrowserPrivilege.售货机))
            {
                r = r.Where(p => p.Machine.User.Id == LogedInUser.Id);
            }

            return r.Count(p => p.Status == (int)FaultStatus.故障发生);
        }

        private object GetFaultList(object obj)
        {
            Dictionary<string, object> args = obj as Dictionary<string, object>;
            Debug.Assert(args != null, "参数为空");
            string searchString = Convert.ToString(args["SearchString"]);
            string errorSelect = args["ErrorSelect"] == null ? "" : Convert.ToString(args["ErrorSelect"]);

            var fl = from t in DataContext.FaultSet select t;

            if (!CheckBrowserPrivilege((int)UserBrowserPrivilege.售货机))
            {
                fl = fl.Where(p => p.Machine.User.Id == LogedInUser.Id);
            }


            if (!string.IsNullOrEmpty(searchString))
            {
                fl = fl.Where(p => p.Machine.MachineCode.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(errorSelect))
            {
                if (int.Parse(errorSelect) == 1)
                {
                    fl = fl.Where(p => p.Status == (int)FaultStatus.故障发生);
                }
                else if (int.Parse(errorSelect) == 2)
                {
                     fl = fl.Where(p => p.Status == (int)FaultStatus.故障解除);
                }
                
            }

            return fl.OrderByDescending(p => p.Id);
        }

        private object GetShipmentStatusRecordList(object obj)
        {
            Dictionary<string, object> args = obj as Dictionary<string, object>;

            var sl = from t in DataContext.ShipmentStatusRecordSet select t;

            if (!CheckBrowserPrivilege((int)UserBrowserPrivilege.售货机))
            {
                sl = sl.Where(p => p.Machine.User.Id == LogedInUser.Id);
            }


            
            if (args != null && args.Keys.Contains("SearchString"))
            {
                string searchString = Convert.ToString(args["SearchString"]);
                if (!string.IsNullOrEmpty(searchString))
                {
                    sl = sl.Where(p => p.Machine.MachineCode.Contains(searchString) || p.IdentityNumber.Contains(searchString));
                }
                
            }
            if (args != null && args.Keys.Contains("ResultCount"))
            {
                int rCount = Convert.ToInt32(args["ResultCount"]);
                sl = sl.OrderByDescending(p => p.Id).Take(rCount);
            }
            return sl.OrderByDescending(p => p.Id);
        }

        private object GetSalesHistoryList(object obj)
        {
            Dictionary<string,object> args=obj as Dictionary<string,object>;

            var sl = from t in DataContext.SalesHistorySet select t;

            if (!CheckBrowserPrivilege((int)UserBrowserPrivilege.售货机))
            {
                sl = sl.Where(p => p.Machine.User.Id == LogedInUser.Id);
            }

            //Debug.Assert(args != null, "参数为空");
            if (args != null && args.Keys.Contains("SearchString"))
            {
                string searchString = Convert.ToString(args["SearchString"]);
                if (!string.IsNullOrEmpty(searchString)) {
                    sl = sl.Where(p => p.Commod.Name.Contains(searchString) || p.Machine.MachineCode.Contains(searchString));
                }
                
            }
            if (args != null && args.Keys.Contains("Begintime"))
            {
                string begintime = Convert.ToString(args["Begintime"]);
                if (!string.IsNullOrEmpty(begintime))
                {
                    DateTime dt = Convert.ToDateTime(begintime);
                    sl = sl.Where(p => p.SalesDate >= dt);
                }
                
            }
            if (args != null && args.Keys.Contains("EndTime"))
            {
                string endTime = Convert.ToString(args["EndTime"]);
                if (!string.IsNullOrEmpty(endTime))
                {
                    DateTime dt = Convert.ToDateTime(endTime);
                    sl = sl.Where(p => p.SalesDate <= dt);
                }
               
            }
            if (args != null && args.Keys.Contains("ResultCount"))
            {
                int rCount = Convert.ToInt32(args["ResultCount"]);
                sl = sl.OrderByDescending(p => p.Id).Take(rCount);
            }

            return sl.OrderByDescending(p => p.Id);
        }
    }
}
