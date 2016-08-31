using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using VEM.Business.Exceptions.MachineException;
using VEM.Business.Util;
using VEM.Model;

namespace VEM.Business.Business
{
    public class MachineBusiness : BusinessAbstract
    {
        public override object DoAction(object obj)
        {
            switch (OperationCode)
            {
                default: return null;
                case BusinessOperations.GetMachineModelList: return GetMachineModelList();
                case BusinessOperations.AddMachineModel: return AddMachineModel(obj);
                case BusinessOperations.SaveMachineModel: return SaveMachineModel(obj);
                case BusinessOperations.DelMachineModel: return DelMachineModel(obj);
                case BusinessOperations.GetMachineModelById:return GetMachineModelById(obj);
                case BusinessOperations.GetMachineList: return GetMachineList(obj);
                case BusinessOperations.AddMachine: return AddMachine(obj);
                case BusinessOperations.SaveMachine: return SaveMachine(obj);
                case BusinessOperations.DelMachine: return DelMachine(obj);
                case BusinessOperations.GetMachineById: return GetMachineById(obj);
                case BusinessOperations.SaveMachineRentOrSell: return SaveMachineRentOrSell(obj);
            }
        }

        private object SaveMachineRentOrSell(object obj)
        {
            Dictionary<string, object> args = obj as Dictionary<string, object>;
            if (args != null)
            {
                int machineId = Convert.ToInt32(args["MachineId"]);
                int userId =  Convert.ToInt32(args["UserId"]);
                int machineStatusType =  Convert.ToInt32(args["MachineStatusType"]);
                Machine machine = DataContext.MachineSet.Find(machineId);
                machine.User = DataContext.PersonSet.OfType<User>().FirstOrDefault(p=>p.Id==userId);
                machine.RentOrSell = Convert.ToBoolean(machineStatusType);
            }
            DataContext.SaveChanges();
            return true;
        }

        private object GetMachineById(object obj)
        {
            int id = (int)obj;
            return DataContext.MachineSet.Find(id);
        }

        private object DelMachine(object obj)
        {
            int id = (int)obj;
            Machine machine = DataContext.MachineSet.Find(id);

            if (machine.ContainerRoad.Any())throw new ContainerRoadNotNullException();
            if (machine.SalesHistory.Any()) throw new SalesHistoryNotNullException();

            if (machine.MachineTem != null)
            {
                DataContext.MachineTemSet.Remove(machine.MachineTem);
            }
           
            DataContext.MachineSet.Remove(machine);
            DataContext.SaveChanges();
            return true;
        }

        private object SaveMachine(object obj)
        {
            Dictionary<string, object> args = obj as Dictionary<string, object>;

            if (args != null)
            {
                Machine argsMachine = args["Machine"] as Machine;

                Debug.Assert(argsMachine != null, "售货机为空");
                Machine machine = DataContext.MachineSet.Find(argsMachine.Id);

                int machineModelId = Convert.ToInt32(args["MachineModelId"]);
                if (machineModelId > 0) machine.MachineModel = DataContext.MachineModelSet.Find(machineModelId);
                int cityId = Convert.ToInt32(args["CityId"]);
                if (cityId > 0) machine.City = DataContext.CitySet.Find(cityId);
                int districtId = Convert.ToInt32(args["DistrictId"]);
                if (districtId > 0) machine.District = DataContext.DistrictSet.Find(districtId);
                machine.Address = argsMachine.Address;
                machine.MachineCode = argsMachine.MachineCode;
                machine.SoftwareId = argsMachine.SoftwareId;
                machine.HardwareId = argsMachine.HardwareId;
                machine.ModifyUserID = LogedInUser.Id;
                machine.ModifyDate = DateTime.Now;
            }

            DataContext.SaveChanges();
            return true;
        }

        private object AddMachine(object obj)
        {

            Dictionary<string, object> args = obj as Dictionary<string, object>;

            if (args != null)
            {
                Machine machine = args["Machine"] as Machine;
                int machineModelId = Convert.ToInt32(args["MachineModelId"]);
                if (machineModelId > 0) if (machine != null) machine.MachineModel = DataContext.MachineModelSet.Find(machineModelId);
                int cityId = Convert.ToInt32(args["CityId"]);
                if (cityId > 0) if (machine != null) machine.City = DataContext.CitySet.Find(cityId);
                int districtId = Convert.ToInt32(args["DistrictId"]);
                if (districtId > 0) if (machine != null) machine.District = DataContext.DistrictSet.Find(districtId);
                if (machine != null)
                {
                    machine.CreateUserID = LogedInUser.Id;
                    machine.CreateDate = DateTime.Now;
                    machine.User = LogedInUser;

                    machine.MachineTem = new MachineTem();

                    DataContext.MachineSet.Add(machine);
                }
            }
            DataContext.SaveChanges();
            return true;
        }

        private object GetMachineList(object obj)
        {

            string searchString = Convert.ToString(obj);


            var ml = from t in DataContext.MachineSet select t;
            if (!CheckBrowserPrivilege((int)UserBrowserPrivilege.售货机))
            {
                ml = ml.Where(p=>p.User.Id==LogedInUser.Id);
            }


            if (!string.IsNullOrEmpty(searchString))
            {
                ml = ml.Where(p => p.MachineCode.Contains(searchString));
            }

            return ml.OrderByDescending(p => p.Id);

        }

        private object GetMachineModelById(object obj)
        {
            int id = (int)obj;
            return DataContext.MachineModelSet.Find(id);
        }

        private object DelMachineModel(object obj)
        {
            int id = (int)obj;
            MachineModel model = DataContext.MachineModelSet.Find(id);
            if (model.Machine.Any()) throw new MachineNotNullException();

            DataContext.MachineModelSet.Remove(model);
            DataContext.SaveChanges();
            return true;
        }

        private object SaveMachineModel(object obj)
        {
            MachineModel model = obj as MachineModel;
            DataContext.Entry(model).State = EntityState.Unchanged;
            DataContext.Entry(model).State = EntityState.Modified;
            DataContext.SaveChanges();
            return true;
        }

        private object AddMachineModel(object obj)
        {
            MachineModel model = obj as MachineModel;
            DataContext.MachineModelSet.Add(model);
            DataContext.SaveChanges();
            return true;
        }

        private object GetMachineModelList()
        {
            return DataContext.MachineModelSet.OrderByDescending(p => p.Id);
        }
    }
}
