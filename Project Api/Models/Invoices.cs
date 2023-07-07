using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Api.Models
{
    public class Invoices
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public DateTime inviocDate { get; set; }

        [ForeignKey("Patients")]
        public int? Idpatient { get; set; }
        [ForeignKey("Employee")]
        public int? IdEmp { get; set; }

        public virtual Patients patients { get; set; }
        public virtual Employee employee { get; set; }


    }
}
