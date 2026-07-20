using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using polisync.Models.Enums;

namespace polisync.Models.DTOs
{
    public class PoliciesListResponseForAdminDto
    {
        public string PolicyName { get; set; } = string.Empty;


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PolicyTypeEnum PolicyType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        public decimal PolicyLimit { get; set; }
    }
}