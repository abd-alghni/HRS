using HRS.Core.Dtos;
using HRS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Infrastructure.Services.Employees
{
    public interface IEmployeeService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<string> Create(CreateEmployeeDto dto);
        Task<string> Update(UpdateEmployeeDto dto);
        Task<string> Delete(string id);
        Task<UpdateEmployeeDto> Get(string id);
        Task<List<DepartmentViewModel>> GetDepartments();
        EmployeeViewModel GetEmployeeByEmployeeName(string employeeName);
        Task<string> SetFCMToUser(string fcmToken, string employeeId);
    }
}
