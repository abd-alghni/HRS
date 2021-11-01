using HRS.Infrastructure.Services;
using HRS.Infrastructure.Services.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Web.Controllers
{

    public class HomeController : BaseController
    {
        private readonly IDashboardService _dashboardService;
        public HomeController(IDashboardService dashboardService, IEmployeeService employeeService) : base(employeeService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<IActionResult> Index()
        {
            
                var data = await _dashboardService.GetData();
                return View(data);
            
        }

        public async Task<IActionResult> GetEmployeeTypeChartData()
        {
            var data = await _dashboardService.GetEmployeeTypeChart();
            return Ok(data);
        }
        public async Task<IActionResult> GetHolidayStatusChartData()
        {
            var data = await _dashboardService.GetHolidayStatusChart();
            return Ok(data);
        }
    }
}
