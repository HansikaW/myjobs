using System;
using System.Collections.Generic;
using System.Text;

namespace CommunityPortal.Models
{
    class Invoice
    {

        public string Id { get; set; } //n
        public string InvoiceSubject { get; set; }
        public string Description { get; set; }
        public string ServiceProvideId { get; set; } //n
       // public string Quentity { get; set; }
       // public string SubTotal { get; set; }
        public float ServiceFee { get; set; }
        public float Total { get; set; }
        public int InvoiceStatus { get; set; }

        public string WorkPlace { get; set; }  //mycreation
        public float OtherFee { get; set; }   //mycreation

    }
}
