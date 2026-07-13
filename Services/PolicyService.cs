using polisync.Models;
using polisync.Repositories;

namespace polisync.Services
{
    public class PolicyService
    {
        private readonly IPolicyInterface _policyRepo;
        public PolicyService(IPolicyInterface policyRepo)
        {
            _policyRepo = policyRepo;
        }

        // === Job ===
        public Task<List<Policy>> GetAllPolicies()
        {
            return _policyRepo.GetPolicies();
        }
    }
}