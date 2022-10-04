using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Database;
using Model.Request;
using Model.Response;
using Service.APIServices;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pairLegendsCore.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMatchService _matchService;

        public UserController(IUserService userService, IMatchService matchService)
        {
            _userService = userService;
            _matchService = matchService;
        }

        Guid GetUserTokenId()
        {
            return new Guid(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <returns>The User</returns>
        [HttpGet("@me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetById(GetUserTokenId());
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Update User Profile
        /// </summary>
        /// <param name="updateUserRequest">Update Information</param>
        /// <returns>Update Status</returns>
        [HttpPut("@me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Update(request);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Get User paged list
        /// </summary>
        /// <param name="pagingRequest">Paging params</param>
        /// <returns>The User paged list</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPagedList([FromQuery] PagingRequest request)
        {
            var result = await _userService.GetUserPagingList(request);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Register an account 
        /// </summary>
        /// <param name="signUpModel">Register information</param>
        /// <returns>Register Status</returns>
        [AllowAnonymous]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Register(request);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Get User information by UserName
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User's information</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.GetById(id);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Authenticate your Login
        /// </summary>
        /// <param name="loginModel">Login Information</param>
        /// <returns>Your token</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var authResult = await _userService.Authenticate(request);
            if (string.IsNullOrEmpty(authResult.ResultObject))
                return NotFound(authResult);
            return Ok(authResult);
        }

        /// <summary>
        /// Delete an account
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns>Delete Status</returns>
        [HttpDelete("delete/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string userName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Delete(userName);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Get User information by UserName
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns>User's information</returns>
        [AllowAnonymous]
        [HttpGet("username/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.GetByUserName(userName);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Assign Roles for User
        /// </summary>
        /// <param name="roleAssignRequest">Role Assign Information</param>
        /// <returns>Assign Status</returns>
        // WARNING: AFTER DEPLOY MUST UNCOMMENT THIS
        //[Authorize(Roles = "Admin")]
        [HttpPut("@me/Role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.RoleAssign(request);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        // /// <summary>
        // /// Get Email Confirm Code
        // /// </summary>
        // /// <param name="request">Get Confirm Code</param>
        // /// <returns>Confirm Code</returns>
        // [HttpPost("get-confirm-code")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public async Task<IActionResult> GetConfirmCode(GetConfirmCodeRequest request)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);
        //     var result = await _userService.GetConfirmCode(request);
        //     if (result.Succeeded)
        //         return Ok(result);
        //     return BadRequest(result);
        // }

        // [HttpPost("confirm-email")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);
        //     var result = await _userService.ConfirmEmail(request);
        //     if (result.Succeeded)
        //         return Ok(result);
        //     return BadRequest(result);
        // }
    }
}