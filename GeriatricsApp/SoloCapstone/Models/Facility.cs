using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class Facility
    {
        [Key]
        public int FacilityId { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Display(Name = "Facility Website")]
        public string URL { get; set; }
        public string Description { get; set; }
        [Display(Name = "Overall Employee Rating")]
        public string EmployeeRating { get; set; }
        [Display(Name = "Overall Facility Rating")]
        public string FacilityRating { get; set; }
    }
}