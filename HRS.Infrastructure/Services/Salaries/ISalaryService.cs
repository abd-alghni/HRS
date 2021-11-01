using HRS.Core.Dtos;
using HRS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Infrastructure.Services.Salaries
{
    public interface ISalaryService
    {
        Task<List<EmployeeViewModel>> GetEmployeesNames();
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<int> Create(CreateSalaryDto dto);
        Task<int> Update(UpdateSalaryDto dto);
        Task<UpdateSalaryDto> Get(int id);
        Task<int> Delete(int id);
    }
}
