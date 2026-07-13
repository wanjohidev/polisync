namespace polisync.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        // Collection Navigation Properties
        // One User is related to Many Claims
        // One User is related to Many Policies
        public ICollection<InsuranceClaim> Claims { get; set; }
        public ICollection<Policy> Policies { get; set; }
    }
}   