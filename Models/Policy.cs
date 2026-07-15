using polisync.Models.Enums;

namespace polisync.Models
{
    public class Policy
    {
        public string PolicyName { get; set; } = string.Empty;
        public PolicyTypeEnum PolicyType { get; set; }
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