using System;
using System.Collections.Generic;
using System.Text;

namespace CommunityPortal.Models
{
    class Quotation
    {
        public string Id { get; set; } //n
        public string QuotationSubject { get; set; }
        public string RefferenceId { get; set; }
        public string JobDescription { get; set; }
        public float EstimatedServiceFee { get; set; }
        public float EstimatedOtherFee { get; set; } //mycreation
        public string WorkPlace { get; set; }  //mycreation

        public float EstimatedTotal { get; set; }
        public string ServiceProviderId { get; set; }
        public int QuotationStatus { get; set; }
         public DateTime Duedate { get; set; }
        public string servicetype { get; set; }


        //  public string EstimatedQuentity { get; set; }
        //  public string ApprovedAdminId { get; set; } //n
        //  public string EstimatedSubTotal { get; set; }




    }
}
