using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OEEAPI.Models
{
    public class OEEViewModel
    {
        public Department_Equipment department_Equipment { get; set; }

        public OEEData[] oeeData { get; set; }
    }
}
