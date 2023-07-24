using System.ComponentModel.DataAnnotations.Schema;
using System;
using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace DentalClinicProject.ViewModels
{
    public class InvoicesVM
    {
        public int Id { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        [Display(Name = "Status")]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = " REQUIRED !")]
        public string Status { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = " REQUIRED !")]
        public int Amount { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [Display(Name = "inviocDate")]
        public DateTime inviocDate { get; set; }

      
    }
}
