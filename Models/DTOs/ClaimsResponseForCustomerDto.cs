using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using polisync.Models.Enums;

namespace polisync.Models.DTOs
{
    public class ClaimsResponseForCustomerDto
    {
        // Properties derived from Claims table
        public int ClaimId { get; set; }
        public PolicyTypeEnum PolicyType { get; set; }
        public string IncidentDescription { get; set; } = string.Empty;
        public DateTime IncidentDate { get; set; }
        public decimal ClaimAmount { get; set; }
        public ClaimsStatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
