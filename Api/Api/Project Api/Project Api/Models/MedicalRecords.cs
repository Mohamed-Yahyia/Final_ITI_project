using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Project_Api.Models
{
    public class MedicalRecords
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime VisitDate { get; set; }

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
