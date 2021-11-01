using HRS.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Data
{
    public static class DbSeeder
    {

        public static IHost SeedDb(this IHost webHost)
    {
        using var scope = webHost.Services.CreateScope();
        try
        {
            var context = scope.ServiceProvider.GetRequiredService<HRSDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
             context.SeedDepartment().Wait();
            userManager.SeedEmployee().Wait();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return webHost;

    }
        public static async Task<IEnumerable<Department>> SeedDepartment(this HRSDbContext _db)
        {
            var _department = await _db.Departments.AnyAsync();
            if (_department == true)
            {
                return await _db.Departments.ToListAsync();
            }

            var departments = new List<Department>();

            var department = new Department();
            department.Name = "A1";
            department.Id = 1;
            department.CreatedAt = DateTime.Now;

            var department2 = new Department();
            department2.Name = "A2";
            department2.CreatedAt = DateTime.Now;

            departments.Add(department);
            departments.Add(department2);

            await _db.Departments.AddRangeAsync(departments);
            await _db.SaveChangesAsync();

            return departments;
        }

        public static async Task SeedEmployee(this UserManager<Employee> userManger)
        {
            if (await userManger.Users.AnyAsync())
            {
                return;
            }
            var user = new Employee();
            user.FullName = "System Developer";
            user.UserName = "dev@gmail.com";
            user.Email = "dev@gmail.com";
            user.CreatedAt = DateTime.Now;
            user.DepartmentId = 1;

            await userManger.CreateAsync(user, "Admin111$$");
        }
    }
}