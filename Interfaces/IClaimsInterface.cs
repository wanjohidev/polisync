using polisync.Models;
using polisync.Models.DTOs;

namespace polisync.Repositories
{
    public interface IClaimsInterface
    {
        Task<List<ClaimsListResponseForCustomerDto>> GetMyClaims(int userId);
        Task<List<ClaimsListResponseForAdminDto>> GetAllClaims();
        Task<InsuranceClaim?> GetClaimById(int claimId);
        Task<ClaimDetailsDtoForAdmin?> GetClaimDetails(int claimId);
        Task CreateClaim(InsuranceClaim claim);
        Task UpdateClaim(InsuranceClaim claim);
        Task DeleteClaim(int claimId);
    }
}