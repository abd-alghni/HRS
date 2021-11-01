using AutoMapper;
using HRS.Core.Dtos;
using HRS.Core.Constants;
using HRS.Infrastructure.Services.Employees;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRS.Infrastructure.Services.Departments;
using HRS.Infrastructure.Services.Salaries;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRS.Web.Controllers
{
    public class SalaryController : BaseController
    {
        private readonly ISalaryService _salaryService;
        private readonly IMapper _mapper;

        public SalaryController(ISalaryService salaryService, IMapper mapper , IEmployeeService employeeService):base(employeeService)
        {
            _salaryService = salaryService;
            _mapper = mapper;
            
    }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetSalaryData(Pagination pagination, Query query)
        {
            var result = await _salaryService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["employees"] = new SelectList(await _salaryService.GetEmployeesNames(), "Id", "FullName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSalaryDto dto)
        {
            if (ModelState.IsValid)
            {
                await _salaryService.Create(dto);
                return Ok(Results.AddSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _salaryService.Get(id);

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateSalaryDto dto)
        {
            if (ModelState.IsValid)
            {
                await _salaryService.Update(dto);
                return Ok(Results.EditSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _salaryService.Delete(id);
            return Ok(Results.DeleteSuccessResult());
        }
        //[HttpPost]
        //public IActionResult ExportToExel()
        //{
        //    return View();
        //}
    }
}
