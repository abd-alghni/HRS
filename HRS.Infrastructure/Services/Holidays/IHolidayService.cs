using HRS.Core.Dtos;
using HRS.Core.Enums;
using HRS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Infrastructure.Services.Holidays
{
    public interface IHolidayService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<int> Create(CreateHolidayDto dto);
        Task<int> Update(UpdateHolidayDto dto);
        Task<int> Delete(int id);
        Task<UpdateHolidayDto> Get(int id);
        Task<List<EmployeeViewModel>> GetEmployeesNames();
        Task<int> UpdateStatus(int id, ContentStatus status);
        Task<List<ContentChangeLogViewModel>> GetLog(int id);
    }
}
