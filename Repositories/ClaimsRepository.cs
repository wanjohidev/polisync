using Microsoft.EntityFrameworkCore;
using polisync.Data;
using polisync.Models;
using polisync.Models.DTOs;

namespace polisync.Repositories
{
    public class ClaimsRepository : IClaimsInterface
    {
        private readonly AppDbContext _context;
        public ClaimsRepository(AppDbContext context)
        {
            _context = context;
        }

        // === Implementing Interface ===

        public async Task<List<ClaimsResponseDto>> GetAllClaims()          // to be used inside Admin Controllers
        {
            var claims = await _context.Claims
                                .Include(c => c.User)
                                .Include(c => c.Policy)
                                .ToListAsync();
            
            return claims.Select(c => new ClaimsResponseDto
            {
                ClaimId = c.ClaimId,
                IncidentDescription = c.IncidentDescription,
                ClaimAmount = c.ClaimAmount,

                UserName = c.User != null ? $"{c.User.Name}" : "Unknown",
                PolicyNumber = c.Policy?.PolicyNumber ?? "N/A",
                PolicyType = c.Policy?.PolicyType ?? "N/A"
            }).ToList();
        }

        public async Task<List<InsuranceClaim>> GetMyClaims(int userId)           // to be used inside Customer Controller
        {

            return await _context.Claims
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<InsuranceClaim?> GetClaimById(int claimId)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            return claim;
        }

        public async Task CreateClaim(InsuranceClaim claim)
        {
            await _context.Claims.AddAsync(claim);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClaim(InsuranceClaim claim)
        {
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClaim(int claimId)
        {
            var claim = await _context.Claims.FirstOrDefaultAsync(c => c.ClaimId == claimId);

            if (claim == null)
                return;
            
            _context.Claims.Remove(claim);
            await _context.SaveChangesAsync();
        }
    }
}