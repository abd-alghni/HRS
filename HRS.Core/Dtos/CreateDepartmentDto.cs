using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Core.Dtos
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name ="اسم القسم")]
        public string Name { get; set; }

    }
}
