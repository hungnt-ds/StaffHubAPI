using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffHubAPI.DataAccess.DTOs;
using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.Helper.Attributes;
using StaffHubAPI.Helper.Constants;
using StaffHubAPI.Services.Interfaces;

namespace StaffHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;
        private readonly IRoleClaimService _roleClaimService;
        private readonly IMapper _mapper;

        public ClaimController(IClaimService claimService, IRoleClaimService roleClaimService, IMapper mapper)
        {
            _claimService = claimService;
            _roleClaimService = roleClaimService;
            _mapper = mapper;
        }

        [HttpGet("get-all-claims")]
        [Authorize]
        [AuthorizeClaim(AppConstants.CLAIM_VIEW_ALL_CLAIM)]
        public IActionResult GetAllClaims()
        {
            var claims = _claimService.GetAllClaims();
            var claimDtos = _mapper.Map<IEnumerable<ClaimDTO>>(claims);
            return Ok(claimDtos);
        }

        [HttpPost("new-claim")]
        [Authorize]
        [AuthorizeClaim(AppConstants.CLAIM_CREATE_CLAIM)]
        public IActionResult CreateNewClaim(string claimName)
        {
            var newClaim = new Claim { ClaimName = claimName };
            return Ok(_claimService.CreateClaim(newClaim));
        }

        [HttpPut("update-claim")]
        [Authorize]
        [AuthorizeClaim(AppConstants.CLAIM_UPDATE_CLAIM)]
        public IActionResult UpdateClaim(int id, string updateClaimName)
        {
            var existClaim = _claimService.GetClaim(id);
            if (existClaim == null)
            {
                return BadRequest("Claim does not exist");
            }

            existClaim.ClaimName = updateClaimName;
            return Ok(_claimService.UpdateClaim(existClaim));
        }

        [HttpDelete("delete-claim")]
        [Authorize]
        [AuthorizeClaim(AppConstants.CLAIM_DELETE_CLAIM)]
        public IActionResult DeleteClaim(int id)
        {
            var existClaim = _claimService.GetClaim(id);
            if (_roleClaimService.IsClamUsed(existClaim.ClaimId))
            {
                return BadRequest("Claim is used cannot be remove");
            }
            if (existClaim == null)
            {
                return BadRequest("Claim does not exist");
            }

            return Ok(_claimService.RemoveClaim(existClaim));
        }        
    }
}
