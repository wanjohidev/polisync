using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using polisync.Models.Enums;

namespace polisync.Models.DTOs
{
    public class ClaimsListResponseForAdminDto
    {
        // Properties derived from Claims table
        public int ClaimId { get; set; }

        public string Claimant { get; set; } = string.Empty;


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PolicyTypeEnum PolicyType { get; set; }

        public string IncidentDescription { get; set; } = string.Empty;

        public DateTime IncidentDate { get; set; }

        public decimal ClaimAmount { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ClaimsStatusEnum Status { get; set; }

    }
}