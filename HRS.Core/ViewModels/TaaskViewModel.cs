using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Core.ViewModels
{
    public class TaaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Duration { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public string Status { get; set; }
    }
}
