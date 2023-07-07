using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Api.Models
{
    public class Reportscs
    {
        public int Id { get; set; }
        public DateTime RepDate { get; set; }
        public string RepType { get; set; }
        public string Description { get; set; }

        [ForeignKey("Patients")]
        public int? Idpatient { get; set; }
        [ForeignKey("Employee")]
        public int? IdEmp { get; set; }

        public virtual Patients patients { get; set; }

        public virtual Employee employee { get; set; }


    }
}
