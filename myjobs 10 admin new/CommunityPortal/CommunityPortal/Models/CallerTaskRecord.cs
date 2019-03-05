using System;
using System.Collections.Generic;
using System.Text;

namespace CommunityPortal.Models
{
    class CallerTaskRecord
    {
        public string Id { get; set; }
        public string pholdername { get; set; }
        public string Address { get; set; }
        public string ServiceType { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public float Budget { get; set; }
        public string callerID { get; set; }
        public string AssignStatus { get; set; }
        public DateTime Duedate { get; set; }


    }
}
