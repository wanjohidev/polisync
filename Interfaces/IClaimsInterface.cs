using polisync.Models;
using polisync.Models.DTOs;

namespace polisync.Repositories
{
    public interface IClaimsInterface
    {
        Task<List<ClaimsResponseForCustomerDto>> GetMyClaims(int userId);
        Task<List<ClaimsResponseForAdminDto>> GetAllClaims();
        Task<InsuranceClaim?> GetClaimById(int claimId);
        Task CreateClaim(InsuranceClaim claim);
        Task UpdateClaim(InsuranceClaim claim);
        Task DeleteClaim(int claimId);
    }
}