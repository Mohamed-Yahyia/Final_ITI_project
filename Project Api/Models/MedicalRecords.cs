using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Api.Models
{
    public class MedicalRecords
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime VisitDate { get; set; }

        [ForeignKey("Patients")]
        public int? Idpatient { get; set; }
        [ForeignKey("Employee")]
        public int? IdEmp { get; set; }

        public virtual Patients patients { get; set; }
        public virtual Employee employee { get; set; }


    }
}
