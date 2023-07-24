using System.ComponentModel.DataAnnotations.Schema;
using System;
using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace DentalClinicProject.ViewModels
{
    public class MedicalRecordsVMcs
    {
        public int Id { get; set; }
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        
        [Display(Name = "Diagnosis ")]
        public string Diagnosis { get; set; }
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        [Display(Name = "Treatment")]
        public string Treatment { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [Display(Name = "VisitDate")]
        public DateTime VisitDate { get; set; }

    
    }
}
