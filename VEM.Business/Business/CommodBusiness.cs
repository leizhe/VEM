using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using VEM.Business.Exceptions.CommodException;
using VEM.Business.Util;
using VEM.Model;

namespace VEM.Business.Business
{
    public class CommodBusiness : BusinessAbstract
    {
        public override object DoAction(object obj)
        {
            switch (OperationCode)
            {
                default: return null;
                case BusinessOperations.GetCommodById: return GetCommodById(obj);
                case BusinessOperations.GetCommodList: return GetCommodList(obj);
                case BusinessOperations.AddCommod: return AddCommod(obj);
                case BusinessOperations.SaveCommod: return SaveCommod(obj);
                case BusinessOperations.DleCommod: return DleCommod(obj);
                case BusinessOperations.AddContainerRoad: return AddContainerRoad(obj);
                case BusinessOperations.GetContainerRoadById: return GetContainerRoadById(obj);
                case BusinessOperations.SaveContainerRoad: return SaveContainerRoad(obj);
            }
        }

        private object SaveContainerRoad(object obj)
        {
            Dictionary<string, object> args = obj as Dictionary<string, object>;
            if (args != null)
            {
                ContainerRoad containerRoadArgs = args["ContainerRoad"] as ContainerRoad;
                Debug.Assert(containerRoadArgs != null, "货道为空");
                ContainerRoad containerRoad = DataContext.ContainerRoadSet.Find(containerRoadArgs.Id);

                containerRoad.IsEnable = containerRoadArgs.IsEnable;
                containerRoad.Number = containerRoadArgs.Number;
                containerRoad.ReamainderCount = containerRoadArgs.ReamainderCount;
                containerRoad.MaxCount = containerRoadArgs.MaxCount;

                if (string.IsNullOrEmpty(Convert.ToString(args["CommodId"])))
                    throw new WrongCommodException();
                int commodId = Convert.ToInt32(args["CommodId"]);
                containerRoad.Commod = DataContext.CommodSet.Find(commodId);
            }

            DataContext.SaveChanges();
            return true;
        }

        private object GetContainerRoadById(object obj)
        {
            int containerRoadId = (int)obj;
            return DataContext.ContainerRoadSet.Find(containerRoadId);
        }

        private object AddContainerRoad(object obj)
        {
            Dictionary<string, object> args = obj as  Dictionary<string, object>;
            if (args != null)
            {
                ContainerRoad containerRoad = args["ContainerRoad"] as ContainerRoad;
                int machineId = Convert.ToInt32(args["MachineId"]);
                if (string.IsNullOrEmpty(Convert.ToString(args["CommodId"])))
                    throw new WrongCommodException();

                int commodId = Convert.ToInt32(args["CommodId"]);

                Debug.Assert(containerRoad != null, "货道是空的");
                containerRoad.Machine = DataContext.MachineSet.Find(machineId);
                containerRoad.Commod = DataContext.CommodSet.Find(commodId);

                DataContext.ContainerRoadSet.Add(containerRoad);
            }
            DataContext.SaveChanges();
            return true;
        }

        private object DleCommod(object obj)
        {
            int id = (int)obj;

            Commod commod = DataContext.CommodSet.Find(id);
            if (commod.ContainerRoad.Any()) throw new CommodNotNullException();

            DataContext.CommodSet.Remove(commod);
            DataContext.SaveChanges();
            return true;
        }

        private object SaveCommod(object obj)
        {
            Commod commod = (Commod)obj;
            DataContext.CommodSet.Attach(commod);
            commod.ModifyUserID = LogedInUser.Id;
            commod.ModifyDate = DateTime.Now;
            DataContext.Entry(commod).State = EntityState.Modified;
            DataContext.SaveChanges();
            return true;
        }

        private object AddCommod(object obj)
        {
            Commod commod = (Commod)obj;
            commod.CreateDate = DateTime.Now;
            commod.CreateUserID = LogedInUser.Id;
            DataContext.CommodSet.Add(commod);
            DataContext.SaveChanges();
            return true;
        }

        private object GetCommodList(object obj)
        {
            string searchString = Convert.ToString(obj);
            var cl = from t in DataContext.CommodSet select t;
            if (!CheckBrowserPrivilege((int)UserBrowserPrivilege.商品))
            {
                cl = cl.Where(p => p.CreateUserID == LogedInUser.Id);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                cl = cl.Where(p => p.Name.Contains(searchString) || p.Code.Contains(searchString));
            }
            return cl.OrderByDescending(p => p.Id);
        }

        private object GetCommodById(object obj)
        {
            int id = (int)obj;
            return DataContext.CommodSet.Find(id);
        }
    }
}
