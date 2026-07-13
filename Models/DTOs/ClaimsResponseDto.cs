using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace polisync.Models.DTOs
{
    public class ClaimsResponseDto
    {
        // Properties derived from Claims table
        public int ClaimId { get; set; }
        public string IncidentDescription { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; } 

        // Properties derived from other tables
        public string UserName { get; set; } = string.Empty;
        public string PolicyNumber { get; set; } = string.Empty;
        public string PolicyType { get; set; } = string.Empty;

    }
}