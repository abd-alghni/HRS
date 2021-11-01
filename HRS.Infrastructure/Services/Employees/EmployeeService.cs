using AutoMapper;
using HRS.Core.Dtos;
using HRS.Infrastructure.Services;
using HRS.Core.Constants;
using HRS.Core.Exceptions;
using HRS.Core.ViewModels;
using HRS.Data;
using HRS.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRS.Infrastructure.Services.Departments;

namespace HRS.Infrastructure.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HRSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly UserManager<Employee> _userManager;
        private readonly IDepartmentService _departmentService;
        public EmployeeService(IDepartmentService departmentService, IEmailService emailService, HRSDbContext db, IMapper mapper, UserManager<Employee> userManager, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _fileService = fileService;
            _emailService = emailService;
            _departmentService = departmentService;
        }
        public async Task<string> SetFCMToUser(string fcmToken , string employeeId)
        {
            var employee = _db.Users.SingleOrDefault(x => x.Id == employeeId && !x.IsDelete);
            if (employee == null)
            {
                throw new EntityNotFoundException();
            }
            employee.FCMToken = fcmToken;
            _db.Users.Update(employee);
            await _db.SaveChangesAsync();
            return employee.Id;
        }
        public EmployeeViewModel GetEmployeeByEmployeeName(string employeeName)
        {
            var employees =  _db.Users.SingleOrDefault(x => x.UserName == employeeName && !x.IsDelete);
            if(employees == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<EmployeeViewModel>(employees);
        }
        public async Task<List<DepartmentViewModel>> GetDepartments()
        {
            var departments = await _db.Departments.Where(x => !x.IsDelete).ToListAsync();
            return _mapper.Map<List<DepartmentViewModel>>(departments);
        }
        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Users.Include(x => x.Department).Where(x => !x.IsDelete && (x.FullName.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch) || x.Email.Contains(query.GeneralSearch) || x.PhoneNumber.Contains(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var users = _mapper.Map<List<EmployeeViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = users,
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
        public async Task<string> Create(CreateEmployeeDto dto)
        {
            var EmailOrPhoneIsExist = _db.Users.Any(x => !x.IsDelete && (x.Email == dto.Email || x.PhoneNumber == dto.PhoneNumber));
            if (EmailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }
            var user = _mapper.Map<Employee>(dto);
            user.UserName = dto.Email;
            user.DepartmentId = dto.DepartmentId;
            //await _db.Users.AddAsync(user);
            //await _db.SaveChangesAsync();

            if (dto.Image != null)
            {
                user.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            var password = GenratePassword();
            try
            {
                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new OperationFailedException();
                }
            }
            catch (Exception e)
            {

            }
            await _emailService.Send(user.Email, "New Account", $"User Name is : {user.Email} and password is {password}");

            return user.Id;
        }
        public async Task<string> Update(UpdateEmployeeDto dto)
        {
            var emailOrPhoneIsExist = _db.Users.Any(x => !x.IsDelete && (x.Email == dto.Email || x.PhoneNumber == dto.PhoneNumber) && x.Id != dto.Id);
            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }
            var user = await _db.Users.FindAsync(dto.Id);
            var updatedUser = _mapper.Map<UpdateEmployeeDto, Employee>(dto, user);
            if (dto.Image != null)
            {
                updatedUser.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            _db.Users.Update(updatedUser);
            await _db.SaveChangesAsync();
            return updatedUser.Id;
        }
        public async Task<string> Delete(string id)
        {

            var employee = await _db.Users.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (employee == null)
            {
                throw new EntityNotFoundException();
            }
            employee.IsDelete = true;
            _db.Users.Update(employee);
            await _db.SaveChangesAsync();
            return employee.Id;
        }
        public async Task<UpdateEmployeeDto> Get(string id)
        {

            var employee = await _db.Users.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (employee == null)
            {
                throw new EntityNotFoundException();
            }
            
            return _mapper.Map<UpdateEmployeeDto>(employee);
        }
        private string GenratePassword()
        {
            return Guid.NewGuid().ToString().Substring(1, 8);
        }
    }
}
