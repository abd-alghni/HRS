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
using Microsoft.AspNetCore.Authorization;

namespace HRS.Web.Controllers
{

    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService dapartmentService, IMapper mapper, IEmployeeService employeeService) : base(employeeService)
        {
            _departmentService = dapartmentService;
            _mapper = mapper;
            
    }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetDepartmentData(Pagination pagination, Query query)
        {
            var result = await _departmentService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto dto)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Create(dto);
                return Ok(Results.AddSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _departmentService.Get(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateDepartmentDto dto)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Update(dto);
                return Ok(Results.EditSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.Delete(id);
            return Ok(Results.DeleteSuccessResult());
        }
        //[HttpPost]
        //public IActionResult ExportToExel()
        //{
        //    return View();
        //}
    }
}
