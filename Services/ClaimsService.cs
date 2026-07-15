using polisync.Data;
using polisync.Models;
using polisync.Models.DTOs;
using polisync.Repositories;

namespace polisync.Services
{
    public class ClaimsService
    {
        private readonly IClaimsInterface _claimsRepo;
        private readonly AppDbContext _context;
        public ClaimsService(IClaimsInterface claimsRepo, AppDbContext context)
        {
            _claimsRepo = claimsRepo;
            _context = context;
        }

        // === Jobs ===

        public Task<List<ClaimsResponseDto>> GetAllClaims()
        {
            return _claimsRepo.GetAllClaims();
        }

        public Task<List<InsuranceClaim>> GetMyClaims(int userId)
        {
            return _claimsRepo.GetMyClaims(userId);
        }

        public Task<InsuranceClaim?> GetClaimById(int id)
        {
            return _claimsRepo.GetClaimById(id);
        }

        public async Task<InsuranceClaim> CreateClaim(int userId, CreateClaimDto dto)
        {
            // Validation checks
            var policy = _context.Policies.Find((int)dto.PolicyType);

            // Does policy exist?
            if (policy == null)
                throw new Exception($"Policy with ID {(int)dto.PolicyType} was not found.");
            // If policy exists, is it active?
            if (dto.IncidentDate < policy.StartDate || dto.IncidentDate > policy.EndDate)
                throw new Exception($"The incident date falls outside the active policy period.");
            // If policy is active, is the claim amount under the policy limit?
            if (dto.ClaimAmount > policy.PolicyLimit)
                throw new Exception($"Claim amount exceeds the policy limit of {policy.PolicyLimit}.");
            
            // Creating claim
            var claim = new InsuranceClaim
            {
                UserId = userId,
                PolicyType = dto.PolicyType,
                IncidentDescription = dto.IncidentDescription,
                IncidentDate = dto.IncidentDate,
                ClaimAmount = dto.ClaimAmount,
                Status = ClaimsStatusEnum.Submitted,
                CreatedAt = DateTime.Now
            };

            await _claimsRepo.CreateClaim(claim);
            return claim;
        }

        public async Task<InsuranceClaim?> UpdateClaimStatus(int claimId, ClaimsStatusEnum newStatus)
        {
            var claimToUpdate = await _claimsRepo.GetClaimById(claimId);

            if (claimToUpdate == null)
                throw new KeyNotFoundException();

            // Validation checks
            
            claimToUpdate.Status = newStatus;

            await _claimsRepo.UpdateClaim(claimToUpdate);

            return claimToUpdate;
        }

        public async Task DeleteClaim(int claimId)
        {
            await _claimsRepo.DeleteClaim(claimId);
        }
    }
}