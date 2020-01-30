using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        //note - C designates that this property belongs to the customer, R designates actual resident cared for

        //client customer information 
        [Display(Name = "First Name")]
        public string CFirstName { get; set; }
        [Display(Name = "Last Name")]
        public string CLastName { get; set; }
        [Display(Name = "Email Address")]
        public string CEmail { get; set; }
        [Display(Name = "Phone Number")]
        public string CPhoneNumber { get; set; }
        [Display(Name = "City")]
        public string CCity { get; set; }
        [Display(Name = "Street")]
        public string CStreet { get; set; }
        [Display(Name = "State")]
        public string CState { get; set; }
        [Display(Name = "Zip Code")]
        public string CZipCode { get; set; }

        //properties for resident address
        [Display(Name = "Resident First Name")]
        public string RFirsName { get; set; }
        [Display(Name = "Resident Last Name")]
        public string RLastName { get; set; }
        [Display(Name = "Resident City")]
        public string RCity { get; set; }
        [Display(Name = "Resident Street")]
        public string RStreet { get; set; }
        [Display(Name = "Resident State")]
        public string RState { get; set; }
        [Display(Name = "Resident Zip Code")]
        public string RZipCode { get; set; }

        //properties for initial consultation 
        public string ConsultMessage { get; set; }
        public bool HasBeenConsulted { get; set; }


        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }


}