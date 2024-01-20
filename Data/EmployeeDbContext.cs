#nullable disable
using Admin_Models;
using Microsoft.EntityFrameworkCore;
using Salary_Models;
using BonusModel;
using Complaint_Models;
using Leaves_Models;
using ActionFilterTrace.Models;
using RoleSalary_Models;

namespace PayrollManagementSystem.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() 
        {
        }

        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
            
        }
          public DbSet<Admin> employeeinformation{get;set;}
          public DbSet<Salary> Salarydata{get;set;}
          public DbSet<RoleSalary>RoleSalaries{get;set;}
          public DbSet<Complaint> Complaints{get;set;}
          public DbSet<Leave> RequestLeavetbl{get;set;}
          public DbSet<TraceActivity> TraceActivity{get;set;}
        public object Complaadmiints { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<RoleSalary>().HasKey(roleSalary => roleSalary.Roleid);
        modelBuilder.Entity<Admin>().HasKey(employee => employee.Employeeid);
        modelBuilder.Entity<Leave>().HasKey(leave => leave.id);
        modelBuilder.Entity<Salary>().HasKey(salary => salary.Salaryid);
        modelBuilder.Entity<Complaint>().HasKey(complaint => complaint.id);
        modelBuilder.Entity<TraceActivity>().HasKey(trace => trace.traceid);
        modelBuilder.Entity<Admin>().HasMany(salary => salary.roles).WithOne(employee=>employee.myadmin).HasForeignKey(r=>r.Employeeid);
    }

        internal string GetUserData()
        {
            throw new NotImplementedException();
        }
    }
}
