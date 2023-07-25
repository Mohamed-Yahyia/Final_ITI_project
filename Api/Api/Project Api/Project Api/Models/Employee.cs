using Castle.Components.DictionaryAdapter;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual HashSet<Reportscs> reportscs { get; set; } = new();
        [JsonIgnore]
        public virtual HashSet<Appointments> appointments { get; set; } = new();
        [JsonIgnore]
        public virtual HashSet<Invoices> invoices { get; set; } = new();
        [JsonIgnore]
        public virtual HashSet<MedicalRecords> records { get; set; } = new();

    }
}
