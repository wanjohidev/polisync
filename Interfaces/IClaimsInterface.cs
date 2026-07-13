using polisync.Models;
using polisync.Models.DTOs;

namespace polisync.Repositories
{
    public interface IClaimsInterface
    {
        Task<List<InsuranceClaim>> GetMyClaims(int userId);
        Task<List<ClaimsResponseDto>> GetAllClaims();
        Task<InsuranceClaim?> GetClaimById(int claimId);
        Task CreateClaim(InsuranceClaim claim);
        Task UpdateClaim(InsuranceClaim claim);
        Task DeleteClaim(int claimId);
    }
}