using HRS.Core.Dtos;
using HRS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Infrastructure.Services.Departments
{
    public interface IDepartmentService
    {
        Task<int> Create(CreateDepartmentDto dto);
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<int> Update(UpdateDepartmentDto dto);
        Task<UpdateDepartmentDto> Get(int id);
        Task<int> Delete(int id);
        
    }
}
