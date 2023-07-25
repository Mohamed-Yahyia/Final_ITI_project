using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Project_Api.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string operation_type { get; set; }
        public string description { get; set; }
        public string Procedures { get; set; }
        public string attachments { get; set; }
        public DateTime operation_Time { get; set; }
        public DateTime operation_Date { get; set; }

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
