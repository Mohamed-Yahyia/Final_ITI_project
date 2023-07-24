using System.ComponentModel.DataAnnotations.Schema;
using System;
using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace DentalClinicProject.ViewModels
{
    public class AppointmentssVM
    {
        public int Id { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [Display(Name = "AppointDate")]
        public DateTime AppointDate { get; set; }
        [Required]
        [Display(Name= "Status")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        
        public string Status { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Time)]
        [Display(Name = "AppointTime")]
        public DateTime AppointTime { get; set; }

    }
}
