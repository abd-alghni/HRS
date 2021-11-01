using AutoMapper;
using HRS.Core.Dtos;
using HRS.Core.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRS.Infrastructure.Services.Holidays;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRS.Infrastructure.Services.Employees;
using HRS.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace HRS.Web.Controllers
{
    [Authorize(Roles = "Manager")]

    public class HolidayController : BaseController
    {
        private readonly IHolidayService _holidayService;
        private readonly IMapper _mapper;

        public HolidayController(IHolidayService holidayService, IMapper mapper, IEmployeeService employeeService) : base(employeeService)
        {
            _holidayService = holidayService;
            _mapper = mapper;
            
    }
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        public async Task<JsonResult> GetHolidayData(Pagination pagination, Query query)
        {
            var result = await _holidayService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
        public async Task<IActionResult> Create()
        {
            ViewData["employees"] = new SelectList(await _holidayService.GetEmployeesNames(),"Id","FullName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateHolidayDto dto)
        {
            if (ModelState.IsValid)
            {
                await _holidayService.Create(dto);
                return Ok(Results.AddSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _holidayService.Get(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateHolidayDto dto)
        {
            if (ModelState.IsValid)
            {
                await _holidayService.Update(dto);
                return Ok(Results.EditSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _holidayService.Delete(id);
            return Ok(Results.DeleteSuccessResult());
        }
        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id , ContentStatus status)
        {
            await _holidayService.UpdateStatus(id , status);
            return Ok(Results.UpdateStatusSuccessResult());
        }
        public async Task<IActionResult> GetLog(int id)
        {
            var logs = await _holidayService.GetLog(id);
            return View(logs);
        }
        //[HttpPost]
        //public IActionResult ExportToExel()
        //{
        //    return View();
        //}
    }
}
