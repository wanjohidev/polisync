using polisync.Models;
using polisync.Models.DTOs;

namespace polisync.Repositories
{
    public interface IPolicyInterface
    {
        Task<List<PoliciesListResponseForAdminDto>> GetPolicies();
    }
}