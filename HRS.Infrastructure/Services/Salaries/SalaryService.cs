using AutoMapper;
using HRS.Core.Dtos;
using HRS.Core.Exceptions;
using HRS.Core.ViewModels;
using HRS.Data;
using HRS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Infrastructure.Services.Salaries
{
    public class SalaryService : ISalaryService
    {
        private readonly HRSDbContext _db;
        private readonly IMapper _mapper;
        public SalaryService(HRSDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<List<EmployeeViewModel>> GetEmployeesNames()
        {
            var employees = await _db.Users.Where(x => !x.IsDelete).ToListAsync();
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }
        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Salaries.Where(x => !x.IsDelete && (x.Employee.FullName.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var salaries = _mapper.Map<List<SalaryViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = salaries,
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
        public async Task<int> Create(CreateSalaryDto dto)
        {
            var salary = _mapper.Map<Salary>(dto);
            salary.EmployeeId = dto.EmployeeId;
            await _db.Salaries.AddAsync(salary);
            await _db.SaveChangesAsync();
            return salary.Id;
        }
        public async Task<int> Update(UpdateSalaryDto dto)
        {

            var salary = await _db.Salaries.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (salary == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedSalary = _mapper.Map<UpdateSalaryDto, Salary>(dto, salary);

            _db.Salaries.Update(updatedSalary);
            await _db.SaveChangesAsync();
            return updatedSalary.Id;
        }
        public async Task<UpdateSalaryDto> Get(int id)
        {

            var salary = await _db.Salaries.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (salary == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateSalaryDto>(salary);
        }
        public async Task<int> Delete(int id)
        {

            var salary = await _db.Salaries.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (salary == null)
            {
                throw new EntityNotFoundException();
            }
            salary.IsDelete = true;
            _db.Salaries.Update(salary);
            await _db.SaveChangesAsync();
            return salary.Id;
        }
    }
}
