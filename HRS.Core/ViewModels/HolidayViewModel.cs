using HRS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Core.ViewModels
{
    public class HolidayViewModel
    {
        public int Id { get; set; }
        public string HolidayType { get; set; }
        public int Duration { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public EmployeeViewModel Employee { get; set; }
    }
}
