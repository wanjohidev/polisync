using Microsoft.EntityFrameworkCore;
using polisync.Data;
using polisync.Models;

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
        
        public async Task<List<Policy>> GetPolicies()
        {
            return await _context.Policies.ToListAsync();
        }
    }
}