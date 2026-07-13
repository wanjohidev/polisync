using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using polisync.Models.DTOs;
using polisync.Services;

namespace polisync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ClaimsService _claimsService;
        public CustomerController(ClaimsService claimsService)
        {
            _claimsService = claimsService;
        }

        // === Endpoints ===
        // SubmitClaim
        [HttpPost("claims")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateClaimEndpoint([FromBody] CreateClaimDto dto)
        {
            int userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );
            
            var claim = await _claimsService.CreateClaim(userId, dto);

            return CreatedAtAction(
                nameof(GetMyClaimsEndpoint),
                new { claimId = claim.ClaimId},
                claim
            );
        }

        // GetMyClaims
        [HttpGet("claims")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyClaimsEndpoint()
        {
            int userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var claims = await _claimsService.GetMyClaims(userId);

            return Ok(claims);
        }
    }
}