using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace DentalClinicProject.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public DateTime AppointDate { get; set; }
        public string Status { get; set; }
        public DateTime AppointTime { get; set; }

        [ForeignKey("Patients")]
        public int? Idpatient { get; set; }

        [ForeignKey("Employee")]
        public int? IdEmp { get; set; }

        public virtual Patients patients { get; set; }
        public virtual Employee employee { get; set; }

    }
}
