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

namespace HRS.Infrastructure.Services.Tasks
{
    public class TaaskService : ITaaskService
    {
        private readonly HRSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        public TaaskService(HRSDbContext db, IMapper mapper, IEmployeeService employeeService)
        {
            _db = db;
            _mapper = mapper;
            _employeeService = employeeService;
        }

        public async Task<List<EmployeeViewModel>> GetEmployeesNames()
        {
            var employees = await _db.Users.Where(x => !x.IsDelete).ToListAsync();
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }
        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Taasks.Include(x => x.Employee).Where(x => !x.IsDelete &&(x.Title.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var taasks = _mapper.Map<List<TaaskViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = taasks,
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
        public async Task<int> Create(CreateTaaskDto dto)
        {
            //if (dto.StartTime >= dto.StartTime)
            //{
            //    throw new InvalidDateException();
            //}

            var taask = _mapper.Map<Taask>(dto);
            taask.EmployeeId = dto.EmployeeId;
            await _db.Taasks.AddAsync(taask);
            await _db.SaveChangesAsync();
            return taask.Id;
        }
        public async Task<int> Update(UpdateTaaskDto dto)
        {
            var taask = await _db.Taasks.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (taask == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedTaask = _mapper.Map<UpdateTaaskDto, Taask>(dto, taask);
            _db.Taasks.Update(updatedTaask);
            await _db.SaveChangesAsync();
            return updatedTaask.Id;
        }
        public async Task<UpdateTaaskDto> Get(int id)
        {
            var taask = await _db.Taasks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (taask == null)
            {
                throw new EntityNotFoundException();
            } 
            return _mapper.Map<UpdateTaaskDto>(taask);
        }
        public async Task<int> Delete(int id)
        {

            var taask = await _db.Taasks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (taask == null)
            {
                throw new EntityNotFoundException();
            }
            taask.IsDelete = true;
            _db.Taasks.Update(taask);
            await _db.SaveChangesAsync();
            return taask.Id;
        }
        public async Task<int> UpdateStatus(int id, ContentStatus status)
        {

            var taask = await _db.Taasks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (taask == null)
            {
                throw new EntityNotFoundException();
            }
            taask.Status = status;
            _db.Taasks.Update(taask);
            await _db.SaveChangesAsync();
            return taask.Id;
        }
        public async Task<List<ContentChangeLogViewModel>> GetLog(int id)
        {
            var changes = await _db.ContentChangeLogs.Where(x => x.ContentId == id && x.Type == ContentType.Taask).ToListAsync();
            return _mapper.Map<List<ContentChangeLogViewModel>>(changes);
        }
    }
}
