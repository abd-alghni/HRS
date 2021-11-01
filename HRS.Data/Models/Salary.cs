using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Data.Models
{
    public class Salary : BaseEntity
    {
        public int Id { get; set; }
        public int SalaryValue { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
