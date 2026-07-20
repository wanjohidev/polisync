using Microsoft.EntityFrameworkCore;
using polisync.Data;
using polisync.Models;
using polisync.Models.DTOs;

namespace polisync.Repositories
{
    public class PolicyRepository : IPolicyInterface
    {
        private readonly AppDbContext _context;
        public PolicyRepository(AppDbContext context)
        {
            _context = context;
        }

        // === Implementing interface ===
        
        public async Task<List<PoliciesListResponseForAdminDto>> GetPolicies()
        {
            var policies = await _context.Policies.ToListAsync();

            return policies.Select(p => new PoliciesListResponseForAdminDto
            {
                PolicyName = p.PolicyName,
                PolicyType = p.PolicyType,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                PolicyLimit = p.PolicyLimit
            }).ToList();
        }
    }
}