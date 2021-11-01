using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Core.ViewModels
{
    public class SalaryViewModel
    {
        public int Id { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public int SalaryValue { get; set; }
    }
}
