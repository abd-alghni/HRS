using HRS.Core.Enums;
using HRS.Core.ViewModels;
using HRS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly HRSDbContext _db;
        public DashboardService(HRSDbContext db)
        {
            _db = db;
        }
        public async Task<DashboardViewModel> GetData()
        {
            var data = new DashboardViewModel();
            data.NumberOfDepartments = await _db.Departments.CountAsync(x => !x.IsDelete);
            data.NumberOfHolidays = await _db.Holidays.CountAsync(x => !x.IsDelete);
            data.NumberOfEmployees = await _db.Users.CountAsync(x => !x.IsDelete);
            data.NumberOfTaasks = await _db.Taasks.CountAsync(x => !x.IsDelete);

            return data;
        }
        public async Task<List<PieChartViewModel>> GetEmployeeTypeChart()
        {
            var data = new List<PieChartViewModel>();
            data.Add(new PieChartViewModel
            {
                Key = "Manager",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.EmployeeType == EmployeeType.Manager),
                Color = "Blue"
            });
            data.Add(new PieChartViewModel
            {
                Key = "Employee",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.EmployeeType == EmployeeType.Employee),
                Color = "Red"
            });
            data.Add(new PieChartViewModel
            {
                Key = "Accountant",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.EmployeeType == EmployeeType.Accountant),
                Color = "Black"
            });
            return data;
        }

        public async Task<List<PieChartViewModel>> GetHolidayStatusChart()
        {
            var data = new List<PieChartViewModel>();
            data.Add(new PieChartViewModel
            {
                Key = "Pending",
                Value = await _db.Holidays.CountAsync(x => !x.IsDelete && x.Status == ContentStatus.Pending),
                Color = "Blue"
            });
            data.Add(new PieChartViewModel
            {
                Key = "Approved",
                Value = await _db.Holidays.CountAsync(x => !x.IsDelete && x.Status == ContentStatus.Approved),
                Color = "Red"
            });
            data.Add(new PieChartViewModel
            {
                Key = "Rejected",
                Value = await _db.Holidays.CountAsync(x => !x.IsDelete && x.Status == ContentStatus.Rejected),
                Color = "Black"
            });
            return data;
        }

    }
}
