namespace polisync.Models
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public string PolicyType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PolicyLimit { get; set; }

        // Collection Navigation Properties
        // One Policy is related to Many Users
        // One Policy is related to Many Claims
        public ICollection<UserModel> Users { get; set; }
        public ICollection<InsuranceClaim> InsuranceClaims { get; set; }
    }
}