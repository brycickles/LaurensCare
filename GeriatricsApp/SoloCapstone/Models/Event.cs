using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        [Display(Name = "Start Date - DD/MM/YYYY HH:mm AM/PM")]
        public DateTime Start { get; set; }
        [Display(Name = "End Date - DD/MM/YYYY HH:mm AM/PM")]
        public DateTime? End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
        public string EmpApplicationId { get; set; }
    }
}