using System;
using System.Collections.Generic;
using System.Text;

namespace CommunityPortal.Models
{
    class WorkOrder
    {
        public string Id { get; set; } //n
        public string ServiceProviderID { get; set; }
        public string WorkOrderSubject { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } //mycreation
        public string Workplace { get; set; }
    }
}
