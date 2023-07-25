using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace DentalClinicProject.ViewModels
{
    public class ReportscsVM
    {
        public int Id { get; set; }
        public DateTime RepDate { get; set; }
        public string RepType { get; set; }
        public string Description { get; set; }

    }
}
