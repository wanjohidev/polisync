using polisync.Models;

namespace polisync.Repositories
{
    public interface IPolicyInterface
    {
        Task<List<Policy>> GetPolicies();
    }
}