namespace polisync.Models.DTOs
{
    public class CreateClaimDto
    {
        public int PolicyId { get; set; }
        public string IncidentDescription { get; set; } = string.Empty;
        public DateTime IncidentDate { get; set; }  
        public decimal ClaimAmount { get; set; }
    }
}