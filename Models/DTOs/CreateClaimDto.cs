using polisync.Models.Enums;

namespace polisync.Models.DTOs
{
    public class CreateClaimDto
    {
        public PolicyTypeEnum PolicyType { get; set; }
        public string IncidentDescription { get; set; } = string.Empty;
        public DateTime IncidentDate { get; set; }  
        public decimal ClaimAmount { get; set; }
    }
}