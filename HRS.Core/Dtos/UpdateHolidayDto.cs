using HRS.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Core.Dtos
{
    public class UpdateHolidayDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "نوع الاجازة")]
        public HolidayType HolidayType { get; set; }
        [Required]
        [Display(Name = "مدة الاجازة")]
        public int Duration { get; set; }
        [Required]
        [Display(Name = "من")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "الى")]
        public DateTime EndDate { get; set; }
        public UpdateEmployeeDto Employee { get; set; }
    }
}
