using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OEEAPI.Models
{
    public class CATContext : DbContext
    {
        public CATContext(DbContextOptions<CATContext> options)
        : base(options)
        { }
        public DbSet<OEEData> OeeData { get; set; }
        public DbSet<Department_Equipment> Department_Equipment { get; set; }
    }

    public class OEEData
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Department_Equipment")]
        public int FK_Department_Equipment { get; set; }
        public string Part { get; set; }
        public int PlannedOutput { get; set; }
        public double NormalCycleTime { get; set; }
        public int ActualOutput { get; set; }
        public double PlannedMethodTime { get; set; }
        public int GoodOutput { get; set; }
        public DateTime? JobStartTime { get; set; }
        public DateTime? JobEndTime { get; set; }
        public int ActualClockTime { get; set; }
        public int ActualRunningTime { get; set; }
        public int Setup { get; set; }
        public int QualityInspection { get; set; }
        public int MachineBroken { get; set; }
        public int MachineAdjustment { get; set; }
        public int MaterialMissing { get; set; }
        public int Opteammembermissing { get; set; }
        public int ToolChange { get; set; }
        public int StartupLoss { get; set; }
        public int Other { get; set; }
        public int TotalLostTime { get; set; }
        public double Availability { get; set; }
        public double Performance { get; set; }
        public double Quality { get; set; }
        public int QualityLoss { get; set; }
        public double SpeedDelta { get; set; }
        public string Status { get; set; }
        public string TimeLossDescription { get; set; }
        public DateTime CreateDateTime { get; set; }

        public Department_Equipment Department_Equipment;
    }

    public class Department_Equipment
    {
        [Key]
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public string EquipmentNumber { get; set; }
        public DateTime? ReportingDate { get; set; }
        public int TotalAvailableTime { get; set; }
        public int NoProductionScheduled { get; set; }
        public int TotalPMTime { get; set; }
        public int NetOperatingTime { get; set; }

        [ForeignKey("FK_Department_Equipment")]
        public ICollection<OEEData> OEEDatas { get; set; }
    }

}
