using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OEEAPI.Models
{
    public class OEE
    {
        public OEE()
        {

        }
        public static void SaveOEEData(CATContext context, OEEViewModel oEEViewModel)
        {
            ValidateInputData(oEEViewModel);
            // Check whether Department and Equipment data exists or not
            var existingRecord = context.Department_Equipment
                .Where(obj => obj.DepartmentName.ToLower() == oEEViewModel.department_Equipment.DepartmentName
                       && obj.EquipmentNumber.ToLower() == oEEViewModel.department_Equipment.EquipmentNumber
                       && obj.ReportingDate == oEEViewModel.department_Equipment.ReportingDate).SingleOrDefault();
            if (existingRecord == null)
            {
                existingRecord = oEEViewModel.department_Equipment;
                context.Department_Equipment.Add(existingRecord);
            }
            foreach (var oee in oEEViewModel.oeeData)
            {
                oee.FK_Department_Equipment = existingRecord.ID;
                if (oee.JobStartTime != null || oee.JobEndTime != null)
                {
                    oee.ActualClockTime = Convert.ToInt32(oee.JobEndTime.Value.Subtract(oee.JobStartTime.Value).TotalMinutes);
                }
                else
                    oee.ActualClockTime = 0;
                oee.TotalLostTime = oee.Setup + oee.QualityInspection + oee.MachineBroken + oee.MachineAdjustment
                            + oee.Opteammembermissing + oee.ToolChange + oee.StartupLoss + oee.Other;

                oee.ActualRunningTime = oee.ActualClockTime == 0 ? 0 : oee.ActualClockTime - oee.TotalLostTime;

                oee.Availability = oee.ActualRunningTime == 0 ? 0 : oee.ActualRunningTime / oee.ActualClockTime;

                if (oee.ActualRunningTime == 0)
                    oee.Performance = 0;
                else if ((oee.PlannedMethodTime / oee.ActualRunningTime) * 100 > 100)
                    oee.Performance = 100;
                else
                    oee.Performance = (oee.PlannedMethodTime / oee.ActualRunningTime) * 100;

                oee.Quality = oee.ActualRunningTime == 0 ? 0 : (oee.GoodOutput / oee.ActualOutput) * 100;

                oee.QualityLoss = (oee.ActualOutput - oee.GoodOutput) * Convert.ToInt32(oee.NormalCycleTime);

                oee.SpeedDelta = oee.ActualRunningTime == 0
                    ? 0
                    : -1 * (((oee.PlannedMethodTime / oee.PlannedOutput) - (oee.ActualRunningTime / oee.ActualOutput)) * oee.ActualOutput);

                oee.Status = oee.SpeedDelta > 0 ? "Loss" : "Gain";
                oee.CreateDateTime = DateTime.Now;
            }

            context.OeeData.AddRange(oEEViewModel.oeeData);

            try
            {
                //Save data to Database
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while inserting data into database. ", ex);
            }
        }

        private static void ValidateInputData(OEEViewModel oEEViewModel)
        {
            if (oEEViewModel.department_Equipment.DepartmentName == "")
                throw new Exception("Department name can't be empty.");
            if (oEEViewModel.department_Equipment.EquipmentNumber == "")
                throw new Exception("EquipmentNumber can't be empty.");
            if (oEEViewModel.department_Equipment.ReportingDate == null)
                throw new Exception("ReportingDate can't be empty.");

        }
    }
}
