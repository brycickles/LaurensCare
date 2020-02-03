using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class ConsultationRequest
    {
        public int ClientId { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ConsultingMessage { get; set; }
    }
}