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
using HRS.Infrastructure.Services.Tasks;
using HRS.Core.Enums;
using HRS.Core.ViewModels;

namespace HRS.Web.Controllers
{
    public class TaaskController : BaseController
    {
        private readonly ITaaskService _taaskService;
        private readonly IMapper _mapper;

        public TaaskController(ITaaskService taaskService, IMapper mapper, IEmployeeService employeeService) : base(employeeService)
        {
            _taaskService = taaskService;
            _mapper = mapper;
            
    }
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        public async Task<JsonResult> GetTaaskData(Pagination pagination, Query query)
        {
            var result = await _taaskService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
        public async Task<IActionResult> Create()
        {
            ViewData["employees"] = new SelectList(await _taaskService.GetEmployeesNames(),"Id","FullName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaaskDto dto)
        {
            if (ModelState.IsValid)
            {
                await _taaskService.Create(dto);
                return Ok(Results.AddSuccessResult());
            }
            ViewData["employees"] = new SelectList(await _taaskService.GetEmployeesNames(), "Id", "FullName");

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var taask = await _taaskService.Get(id);
            ViewData["employees"] = new SelectList(await _taaskService.GetEmployeesNames(), "Id", "FullName");

            return View(taask);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateTaaskDto dto)
        {
            if (ModelState.IsValid)
            {
                await _taaskService.Update(dto);
                return Ok(Results.EditSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _taaskService.Delete(id);
            return Ok(Results.DeleteSuccessResult());
        }
        public async Task<IActionResult> UpdateStatus(int id , ContentStatus status)
        {
            await _taaskService.UpdateStatus(id, status);
            return Ok(Results.UpdateStatusSuccessResult());
        }
        public async Task<IActionResult> GetLog(int id)
        {
            var logs = await _taaskService.GetLog(id);
            return View(logs); 
        }
        //[HttpPost]
        //public IActionResult ExportToExel()
        //{
        //    return View();
        //}
    }
}
