using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffHubAPI.Helper.Attributes;
using StaffHubAPI.Helper.Constants;
using StaffHubAPI.Services.Interfaces;

namespace StaffHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleClaimController : ControllerBase
    {
        private readonly IRoleClaimService _roleClaimService;

        public RoleClaimController(IRoleClaimService roleClaimService)
        {
            _roleClaimService = roleClaimService;
        }

        [HttpPost("add-claim/{roleId}/{claimId}")]
        [Authorize]
        [AuthorizeClaim(AppConstants.CLAIM_ADD_CLAIM_FOR_ROLE)]
        public IActionResult AddClaimToRole(int roleId, int claimId)
        {
            try
            {
                _roleClaimService.AddClaimToRole(roleId, claimId);
                return Ok("Claim added to role successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Return specific error message from service
            }
        }

        [HttpDelete("remove-claims/{roleId}")]
        [Authorize]
        [AuthorizeClaim(AppConstants.CLAIM_REMOVE_CLAIM_FROM_ROLE)]
        public IActionResult RemoveClaimFromRole(int roleId, int claimId)
        {
            try
            {
                _roleClaimService.RemoveClaimFromRole(roleId, claimId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Return specific error message from service
            }
        }

    }
}
