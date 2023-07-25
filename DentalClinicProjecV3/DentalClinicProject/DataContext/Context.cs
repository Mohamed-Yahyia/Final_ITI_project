using DentalClinicProject.Models;
using Microsoft.EntityFrameworkCore;
using DentalClinicProject.ViewModels;

namespace DentalClinicProject.DataContext
{
    public class Context : DbContext
    {
        public Context() : base()
        {

        }
        public Context(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Employee> employees { set; get; }

        public virtual DbSet<Appointments> appointments { set; get; }
        public virtual DbSet<Invoices> invoices { set; get; }
        public virtual DbSet<MedicalRecords> records { set; get; }
        public virtual DbSet<Patients> patients { set; get; }
        public virtual DbSet<Reportscs> reportscs { set; get; }
        public DbSet<DentalClinicProject.ViewModels.EmployeeVM> EmployeeVM { get; set; } = default!;

    }
}
