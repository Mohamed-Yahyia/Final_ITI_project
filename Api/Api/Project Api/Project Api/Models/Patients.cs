using System.Text.Json.Serialization;

namespace Project_Api.Models
{
    public class Patients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }

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
