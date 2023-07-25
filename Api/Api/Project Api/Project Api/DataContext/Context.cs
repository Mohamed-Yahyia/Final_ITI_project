using Microsoft.EntityFrameworkCore;
using Project_Api.Models;

namespace Project_Api.DataContext
{
    public class Context : DbContext
    {
        public Context():base()
        {
            
        }
        public Context(DbContextOptions options):base(options)
        {
            
        }
        public virtual DbSet<Employee> employees { set;get;}

        public virtual DbSet<Appointments> appointments { set; get; }
        public virtual DbSet<Invoices> invoices { set; get; }
        public virtual DbSet<MedicalRecords> records { set; get; }
        public virtual DbSet<Patients> patients { set; get; }
        public virtual DbSet<Reportscs> reportscs { set; get; }
        public virtual DbSet<Operation> operations { set; get; }

    }
}
