
using AutoMapper;
using HRS.Core.Dtos;
using HRS.Core.Enums;
using HRS.Core.Exceptions;
using HRS.Core.ViewModels;
using HRS.Data;
using HRS.Data.Models;
using HRS.Infrastructure.Services.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Infrastructure.Services.Holidays
{
    public class HolidayService : IHolidayService
    {
        private readonly HRSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IEmployeeService _employeeService;
        public HolidayService(IEmailService emailService,HRSDbContext db, IMapper mapper, IEmployeeService employeeService)
        {
            _db = db;
            _mapper = mapper;
            _employeeService = employeeService;
            _emailService = emailService;
        }
        public async Task<List<EmployeeViewModel>> GetEmployeesNames()
        {
            var employees = await _db.Users.Where(x => !x.IsDelete).ToListAsync();
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }
        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Holidays.Include(x => x.Employee).Where(x => !x.IsDelete && (x.Employee.FullName.Contains(query.GeneralSearch)|| string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var holidays = _mapper.Map<List<HolidayViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = holidays,
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pages,
                    total = dataCount,
                }
            };
            return result;
        }
        public async Task<int> Create(CreateHolidayDto dto)
        {
            if (dto.StartDate >= dto.EndDate)
            {
                throw new InvalidDateException();
            }

            var holiday = _mapper.Map<Holiday>(dto);
            holiday.EmployeeId = dto.EmployeeId;
            await _db.Holidays.AddAsync(holiday);
            await _db.SaveChangesAsync();
            return holiday.Id;
        }
        public async Task<int> Update(UpdateHolidayDto dto)
        {
            var holiday = await _db.Holidays.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (holiday == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedHoliday = _mapper.Map<UpdateHolidayDto, Holiday>(dto, holiday);
            _db.Holidays.Update(updatedHoliday);
            await _db.SaveChangesAsync();
            return updatedHoliday.Id;
        }
        public async Task<UpdateHolidayDto> Get(int id)
        {
            var holiday = await _db.Holidays.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (holiday == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateHolidayDto>(holiday);
        }
        public async Task<int> Delete(int id)
        {

            var holiday = await _db.Holidays.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (holiday == null)
            {
                throw new EntityNotFoundException();
            }
            holiday.IsDelete = true;
            _db.Holidays.Update(holiday);
            await _db.SaveChangesAsync();
            return holiday.Id;
        }
        public async Task<int> UpdateStatus(int id,ContentStatus status)
        {

            var holiday = await _db.Holidays.Include(x => x.Employee).SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (holiday == null)
            {
                throw new EntityNotFoundException();
            }
            var changeLog = new ContentChangeLog();
            changeLog.ContentId = holiday.Id;
            changeLog.Type = ContentType.Holiday;
            changeLog.Old = holiday.Status;
            changeLog.New = status;
            changeLog.ChangeAt = DateTime.Now;

            await _db.ContentChangeLogs.AddAsync(changeLog);
            await _db.SaveChangesAsync();
            holiday.Status = status;
            _db.Holidays.Update(holiday);
            await _db.SaveChangesAsync();
            await _emailService.Send(holiday.Employee.Email, "UPDATE HOLIDAY STATUS !", $"YOUR LEAVE REQUEST IS {status.ToString()}");
            return holiday.Id;
        }
        public async Task<List<ContentChangeLogViewModel>> GetLog(int id)
        {
            var changes = await _db.ContentChangeLogs.Where(x => x.ContentId == id && x.Type == ContentType.Holiday).ToListAsync();
            return  _mapper.Map<List<ContentChangeLogViewModel>>(changes);
        }
    }
}
