using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace DentalClinicProject.ViewModels
{
    public class PatientsVM
    {
        public int Id { get; set; }
        [StringLength(15)]
        [Display(Description = "Name")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Gender is REQUIRED !")]
        
        public string  Gender { get; set; }
        [DataType(DataType.EmailAddress)]
        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress(ErrorMessage = "You must enter a valid email")]
        public string Email { get; set; }
        [Display(Name = "BirthDate")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^01[0-2]\d{1,8}$", ErrorMessage = "Not a valid phone number")]
        public int Number { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = " Address is REQUIRED !")]
        public string Address { get; set; }
    }
}
