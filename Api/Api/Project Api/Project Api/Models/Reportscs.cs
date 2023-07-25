using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Project_Api.Models
{
    public class Reportscs
    {
        public int Id { get; set; }
        public DateTime RepDate { get; set; }
        public string RepType { get; set; }
        public string Description { get; set; }


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
