namespace polisync.Models
{
    public class InsuranceClaim
    {
        public int ClaimId { get; set; }
        public int UserId { get; set; }
        public int PolicyId { get; set; }
        public string IncidentDescription { get; set; } = string.Empty;
        public DateTime IncidentDate { get; set; }
        public decimal ClaimAmount { get; set; }
        public ClaimsStatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Collection Navigation Properties
        // One Claim is related to One User
        // One Claim is related to One Policy
        public UserModel User { get; set; }
        public Policy Policy { get; set; }
    }
}