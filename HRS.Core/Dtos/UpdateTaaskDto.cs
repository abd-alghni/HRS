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
    public class UpdateTaaskDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "عنوان المهمة")]
        public string Title { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "التفاصيل")]
        public string Details { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "المدة")]
        public int Duration { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "تاريخ البداية")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "تاريخ النهاية")]
        public DateTime EndTime { get; set; }
        public UpdateEmployeeDto AssignedTo { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الموظف المكلف بالمهمة")]
        public string EmployeeId { get; set; }
    }
}
