using HRS.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Data.Models
{
   public class Employee : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime? DOB { get; set; }
        public string Image { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string FCMToken { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int? Salary { get; set; }
        public List<Holiday> Holidays { get; set; }
        public List<Taask> Tasks { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

    }
}
