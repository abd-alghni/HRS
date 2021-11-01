using HRS.Infrastructure.Services.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IEmployeeService _employeeService;
        protected string userType;
        protected string userId;
        public BaseController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var employee = _employeeService.GetEmployeeByEmployeeName(userName);
                userType = employee.EmployeeType;
                userId = employee.Id;
                ViewBag.fullName = employee.FullName;
                ViewBag.userType = employee.EmployeeType.ToString();
                ViewBag.image = employee.Image;
            }
        }
    }
}
