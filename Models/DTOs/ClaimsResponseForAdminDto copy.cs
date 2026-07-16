using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using polisync.Models.Enums;

namespace polisync.Models.DTOs
{
    public class ClaimsResponseForAdminDto
    {
        // Properties derived from Claims table
        public int ClaimId { get; set; }
        public string IncidentDescription { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; } 

        // Properties derived from other tables
        public string Claimant { get; set; } = string.Empty;
        public PolicyTypeEnum PolicyType { get; set; }

    }
}