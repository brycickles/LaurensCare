using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class EmployeeCustomerJunction
    {
        [Key, Column(Order = 1), ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }


        [Key, Column(Order = 2), ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}