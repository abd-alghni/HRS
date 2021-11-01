using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Core.Dtos
{
    public class UpdateSalaryDto
    {
        public int Id { get; set; }
        public CreateEmployeeDto Employee { get; set; }
        [Required]
        [Display(Name = "اسم الموظف")]
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الراتب")]
        public int SalaryValue { get; set; }
    }
}
