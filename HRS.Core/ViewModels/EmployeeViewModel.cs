using HRS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Core.ViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Image { get; set; }
        public string EmployeeType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DepartmentViewModel Department { get; set; }

    }
}
