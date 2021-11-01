using HRS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRS.Data.Models
{
    public class Taask : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ContentStatus Status { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Taask()
        {
            Status = ContentStatus.Pending;
        }

    }
}
