using HRS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Data.Models
{
    public class Holiday:BaseEntity
    {
        public int Id { get; set; }
        public HolidayType HolidayType { get; set; }
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ContentStatus Status { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Holiday()
        {
            Status = ContentStatus.Pending;
        }
    }
}
