using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using polisync.Models.DTOs;
using polisync.Services;

namespace polisync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly ClaimsService _claimsService;
        private readonly PolicyService _policyService;
        public AdminsController(ClaimsService claimsService, PolicyService policyService)
        {
            _claimsService = claimsService;
            _policyService = policyService;
        }

        // === Endpoints ===

        // View all policies
        [HttpGet("policies")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllPoliciesEndpoint()
        {
            var policies = await _policyService.GetAllPolicies();
            return Ok(policies);
        }

        // View all claims
        [HttpGet("claims")]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> GetAllClaimsEndpoint()
        {
            var claims = await _claimsService.GetAllClaims();
            return Ok(claims);
        }

        // View one claim
        [HttpGet("claims/{claimId}")]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> GetClaimByIdEndpoint([FromRoute] int claimId)
        {
            var claim = await _claimsService.GetClaimById(claimId);
            return Ok(claim);
        }


        // Update claim
        [HttpPut("claims/{claimId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateClaimStatusEndpoint([FromRoute] int claimId, [FromBody] UpdateClaimDto dto)
        {
            await _claimsService.UpdateClaimStatus(claimId, dto.Status);
            return NoContent();
        }

        // Delete claim
        [HttpDelete("claims/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteClaimEndpoint(int id)
        {
            await _claimsService.DeleteClaim(id);
            return NoContent();
        }


    }
}