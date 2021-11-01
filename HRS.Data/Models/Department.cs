using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Data.Models
{
    public class Department : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public List<Employee> Employee { get; set; }

    }
}
