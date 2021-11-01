using HRS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Infrastructure.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetData();
        Task<List<PieChartViewModel>> GetEmployeeTypeChart();
        Task<List<PieChartViewModel>> GetHolidayStatusChart();
    }
}
