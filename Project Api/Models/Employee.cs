using Castle.Components.DictionaryAdapter;

namespace Project_Api.Models
{
    public class Employee
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public double Salary { get; set; }
        public int Number { get; set; }
        public string Addreess { get; set; }

        public virtual HashSet<Reportscs> reportscs { get; set; } = new();
        public virtual HashSet<Appointments> appointments { get; set; } = new();
        public virtual HashSet<Invoices> invoices { get; set; } = new();
        public virtual HashSet<MedicalRecords> records { get; set; } = new();

    }
}
