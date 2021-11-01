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

namespace HRS.Infrastructure.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HRSDbContext _db;
        private readonly IMapper _mapper;
        public DepartmentService(HRSDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Departments.Where(x => !x.IsDelete && (x.Name.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var departments = _mapper.Map<List<DepartmentViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = departments,
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
        public async Task<int> Create(CreateDepartmentDto dto)
        {
            var department = _mapper.Map<Department>(dto);
            await _db.Departments.AddAsync(department);
            await _db.SaveChangesAsync();
            department.Name = dto.Name;
            return department.Id;
        }
        public async Task<int> Update(UpdateDepartmentDto dto)
        {
            
            var department = await _db.Departments.SingleOrDefaultAsync(x => !x.IsDelete && x.Id ==dto.Id);
            if(department == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedDepartment = _mapper.Map<UpdateDepartmentDto, Department>(dto, department);
            
            _db.Departments.Update(updatedDepartment);
            await _db.SaveChangesAsync();
            return updatedDepartment.Id;
        }
        public async Task<UpdateDepartmentDto> Get(int id)
        {

            var department = await _db.Departments.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (department == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateDepartmentDto>(department);
        }
        public async Task<int> Delete(int id)
        {

            var department = await _db.Departments.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (department == null)
            {
                throw new EntityNotFoundException();
            }
            department.IsDelete = true;
            _db.Departments.Update(department);
            await _db.SaveChangesAsync();
            return department.Id;
        }
    }
    
}
