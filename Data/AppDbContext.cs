using Microsoft.EntityFrameworkCore;
using polisync.Models;
using polisync.Models.Enums;

namespace polisync.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<InsuranceClaim> Claims { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === 1. USERS TABLE ===

            modelBuilder.Entity<UserModel>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Claims)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        
            modelBuilder.Entity<UserModel>()
                .HasData(new List<UserModel>
                {
                    new UserModel 
                    { 
                        UserId = 1, Name = "Allan Doe", 
                        Email = "allan.doe@email.com", 
                        Password = "Allan@123!", 
                        Role = "Customer"
                    },
                    new UserModel 
                    { 
                        UserId = 2, Name = "Beth Doe", 
                        Email = "beth.doe@email.com", 
                        Password = "Beth@123!", 
                        Role = "Customer"
                    },
                    new UserModel 
                    { 
                        UserId = 3, Name = "Charles Doe", 
                        Email = "charles.doe@email.com", 
                        Password = "Charles@123!", 
                        Role = "Customer"
                    },
                    new UserModel 
                    { 
                        UserId = 4, Name = "Diana Doe", 
                        Email = "diana.doe@email.com", 
                        Password = "Diana@123!", 
                        Role = "Customer"
                    },
                    new UserModel 
                    {
                        UserId = 5, Name = "Edward Doe", 
                        Email = "edward.doe@email.com", 
                        Password = "Edward@123!", 
                        Role = "Customer"
                    },
                    new UserModel 
                    { 
                        UserId = 6, Name = "Faith Doe", 
                        Email = "faith.doe@email.com", 
                        Password = "Faith@123!", 
                        Role = "Customer"
                    },
                    new UserModel 
                    { 
                        UserId = 7, Name = "Gerald Doe", 
                        Email = "gerald.doe@email.com", 
                        Password = "Gerald@123!", 
                        Role = "Customer"
                    },
                    new UserModel 
                    { 
                        UserId = 8, Name = "Hannah Doe", 
                        Email = "hannah.doe@email.com", 
                        Password = "Hannah@123!", 
                        Role = "Customer"
                    },
                    new UserModel 
                    { 
                        UserId = 9, Name = "Ivy Doe", 
                        Email = "ivy.doe@email.com", 
                        Password = "Ivy@123!", 
                        Role = "Customer"
                    },
                    new UserModel 
                    { 
                        UserId = 10, Name = "John Doe", 
                        Email = "john.doe@email.com", 
                        Password = "John@123!", 
                        Role = "Customer"
                    },              
                    new UserModel 
                    {
                        UserId = 11, Name = "Kellen Smith",
                        Email = "kellen.smith@polisync.com",
                        Password = "Kellen@123!",
                        Role = "Administrator",
                    },
                    new UserModel 
                    {
                        UserId = 12, Name = "Larry Smith",
                        Email = "larry.smith@polisync.com",
                        Password = "Larry@123!",
                        Role = "Administrator",
                    }
                });

            // === 2. POLICIES TABLE ===
            modelBuilder.Entity<Policy>()
                .HasKey(p => (int)p.PolicyType);
            modelBuilder.Entity<Policy>()
                .HasMany(p => p.InsuranceClaims)
                .WithOne(c => c.Policy)
                .HasForeignKey(c => (int)c.PolicyType);

            modelBuilder.Entity<Policy>()
                .HasData(new List<Policy>
                {
                    new Policy 
                    { 
                        PolicyName = "HEALTH/4/2023", PolicyType = PolicyTypeEnum.Health, 
                        StartDate = new DateTime(2026,1,1,0,0,0), 
                        EndDate = new DateTime(2027,1,1,0,0,0),
                        PolicyLimit = 15000000m 
                    },
                    new Policy 
                    { 
                        PolicyName = "MOTOR/1/2024", PolicyType = PolicyTypeEnum.Motor,
                        StartDate = new DateTime(2026,2,1,0,0,0), 
                        EndDate = new DateTime(2027,2,1,0,0,0), 
                        PolicyLimit = 25000000m 
                    },
                    new Policy 
                    { 
                        PolicyName = "PROPERTY/2/2023", PolicyType = PolicyTypeEnum.Property,
                        StartDate = new DateTime(2025,1,1,0,0,0), 
                        EndDate = new DateTime(2026,7,1,0,0,0), 
                        PolicyLimit = 7500000m 
                    },
                    new Policy 
                    { 
                        PolicyName = "TRAVEL/4/2022", PolicyType = PolicyTypeEnum.Travel,
                        StartDate = new DateTime(2026,1,1,0,0,0), 
                        EndDate = new DateTime(2026,7,1,0,0,0),  
                        PolicyLimit = 2500000m 
                    },
                    new Policy 
                    { 
                        PolicyName = "WIBA/1/2025", PolicyType = PolicyTypeEnum.WIBA,
                        StartDate = new DateTime(2025,12,1,0,0,0), 
                        EndDate = new DateTime(2026,12,1,0,0,0),
                        PolicyLimit = 10000000m 
                    }
                });

            // === 3. CLAIMS TABLE ===
            modelBuilder.Entity<InsuranceClaim>()
                .HasKey(c => c.ClaimId);
            
            modelBuilder.Entity<InsuranceClaim>()
                .HasData(new List<InsuranceClaim>
                {
                    new InsuranceClaim
                    {
                        ClaimId = 1, 
                        UserId = 3, PolicyType = PolicyTypeEnum.Property,
                        IncidentDescription = "Laptop Theft",
                        IncidentDate = new DateTime(2026,4,13,22,50,0),
                        ClaimAmount = 250000m,
                        Status = ClaimsStatusEnum.Submitted,
                        CreatedAt = new DateTime(2026,7,7,0,0,0)
                    },
                    new InsuranceClaim
                    {
                        ClaimId = 2, 
                        UserId = 1, PolicyType= PolicyTypeEnum.WIBA,
                        IncidentDescription = "Bodily Injury At Work",
                        IncidentDate = new DateTime(2026,5,29,15,3,0),
                        ClaimAmount = 120000m,
                        Status = ClaimsStatusEnum.Submitted,
                        CreatedAt = new DateTime(2026,7,7,0,0,0)
                    },
                    new InsuranceClaim
                    {
                        ClaimId = 3, 
                        UserId = 2, PolicyType = PolicyTypeEnum.Motor,
                        IncidentDescription = "Car Accident",
                        IncidentDate = new DateTime(2026,7,1,10,32,0),
                        ClaimAmount = 1000000m,
                        Status = ClaimsStatusEnum.Submitted,
                        CreatedAt = new DateTime(2026,7,7,0,0,0)
                    },
                    new InsuranceClaim
                    {
                        ClaimId = 4, 
                        UserId = 8, PolicyType = PolicyTypeEnum.Travel,
                        IncidentDescription = "Baggage Loss",
                        IncidentDate = new DateTime(2026,7,5,10,32,0),
                        ClaimAmount = 1000000m,
                        Status = ClaimsStatusEnum.Submitted,
                        CreatedAt = new DateTime(2026,7,7,0,0,0)
                    },
                    new InsuranceClaim
                    {
                        ClaimId = 5, 
                        UserId = 2, PolicyType = PolicyTypeEnum.Motor,
                        IncidentDescription = "Motor Windshield Replacement",
                        IncidentDate = new DateTime(2026,5,26,12,8,0),
                        ClaimAmount = 50000m,
                        Status = ClaimsStatusEnum.Submitted,
                        CreatedAt = new DateTime(2026,7,7,0,0,0)
                    },
                    new InsuranceClaim
                    {
                        ClaimId = 6, 
                        UserId = 4, PolicyType = PolicyTypeEnum.Health,
                        IncidentDescription = "Medical Surgery",
                        IncidentDate = new DateTime(2026,4,7,21,33,0),
                        ClaimAmount = 200000m,
                        Status = ClaimsStatusEnum.Submitted,
                        CreatedAt = new DateTime(2026,7,7,0,0,0)
                    },
                    new InsuranceClaim
                    {
                        ClaimId = 7, 
                        UserId = 1, PolicyType = PolicyTypeEnum.Property,
                        IncidentDescription = "Equipment Damage",
                        IncidentDate = new DateTime(2026,5,29,14,17,0),
                        ClaimAmount = 100000m,
                        Status = ClaimsStatusEnum.Submitted,
                        CreatedAt = new DateTime(2026,7,7,0,0,0)
                    },
                    new InsuranceClaim
                    {
                        ClaimId = 8, 
                        UserId = 8, PolicyType = PolicyTypeEnum.Travel,
                        IncidentDescription = "Cancelled Flight",
                        IncidentDate = new DateTime(2026,6,29,20,4,0),
                        ClaimAmount = 75000m,
                        Status = ClaimsStatusEnum.Submitted,
                        CreatedAt = new DateTime(2026,7,7,0,0,0)
                    }
                });
        }
    }
}