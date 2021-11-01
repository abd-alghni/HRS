using AutoMapper;
using HRS.Core.Dtos;
using HRS.Core.Constants;
using HRS.Infrastructure.Services.Employees;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRS.Infrastructure.Services.Departments;
using Microsoft.AspNetCore.Authorization;

namespace HRS.Web.Controllers
{

    public class EmployeeController : BaseController
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public EmployeeController(IDepartmentService departmentService, IMapper mapper, IEmployeeService employeeService) : base(employeeService)
        {
            _mapper = mapper;
            _departmentService = departmentService;


    }
        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddFCM(string fcm)
        {
            await _employeeService.SetFCMToUser(userId, fcm);
            return Ok("Updated FCM User");
        }
        public async Task<JsonResult> GetEmployeeData(Pagination pagination, Query query)
        {
            var result = await _employeeService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
        public async Task<IActionResult> Create()
        {
            ViewData["departments"] = new SelectList(await _employeeService.GetDepartments(),"Id","Name");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateEmployeeDto dto)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.Create(dto);
                return Ok(Results.AddSuccessResult());
            }
            ViewData["departments"] = new SelectList(await _employeeService.GetDepartments(), "Id", "Name");

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _employeeService.Get(id);
            ViewData["departments"] = new SelectList(await _employeeService.GetDepartments(), "Id", "Name");

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateEmployeeDto dto)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.Update(dto);
                return Ok(Results.EditSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _employeeService.Delete(id);
            return Ok(Results.DeleteSuccessResult());
        }
        //[HttpPost]
        //public IActionResult ExportToExel()
        //{
        //    return View();
        //}
    }
}
