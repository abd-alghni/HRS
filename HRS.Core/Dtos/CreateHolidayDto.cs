using HRS.Core.Enums;
using HRS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Core.Dtos
{
    public class CreateHolidayDto
    {
        [Required]
        [Display(Name ="نوع الاجازة")]
        public HolidayType HolidayType { get; set; }
        [Required]
        [Display(Name = "مدة الاجازة")]
        public int Duration { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "من")]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "الى")]
        public DateTime EndDate { get; set; }
        public CreateEmployeeDto Employee { get; set; }
        [Required]
        [Display(Name = "اسم الموظف")]
        public string EmployeeId { get; set; }
    }
}
