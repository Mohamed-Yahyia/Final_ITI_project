using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace Project_Api.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public DateTime AppointDate { get; set; }
        public string Status { get; set; }
        public DateTime AppointTime { get; set; }

        [ForeignKey("Patients")]
        [JsonIgnore]
        public int? Idpatient { get; set; }

        [ForeignKey("Employee")]
        [JsonIgnore]
        public int? IdEmp { get; set; }

        [JsonIgnore]
        public virtual Patients patients { get; set; }

        [JsonIgnore]
        public virtual Employee employee { get; set; }

    }
}
