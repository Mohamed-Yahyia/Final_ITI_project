using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Project_Api.Models
{
    public class Invoices
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public DateTime inviocDate { get; set; }

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
