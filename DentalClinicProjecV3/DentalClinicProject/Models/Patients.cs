using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DentalClinicProject.Models
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

        public virtual HashSet<Reportscs> reportscs { get; set; } = new();
        public virtual HashSet<Appointments> appointments { get; set; } = new();
        public virtual HashSet<Invoices> invoices { get; set; } = new();
        public virtual HashSet<MedicalRecords> records { get; set; } = new();

    }
}
