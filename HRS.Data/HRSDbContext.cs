using HRS.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRS.Data
{
    public class HRSDbContext : IdentityDbContext<Employee>
    {
        public HRSDbContext(DbContextOptions<HRSDbContext> options)
            : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Taask> Taasks { get; set; }
        public DbSet<ContentChangeLog> ContentChangeLogs { get; set; }
        public DbSet<Salary> Salaries { get; set; }
    }
}
