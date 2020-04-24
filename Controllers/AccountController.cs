using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using nswag_liquid_slim.ViewModels.Account;

namespace nswag_liquid_slim.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public ActionResult<UserProfileViewModel> Login([FromBody]LoginViewModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Log user out
        /// </summary>
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Request a password reset token sent via email
        /// </summary>
        [HttpPost("SendReset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult SendReset([FromBody]ForgotPasswordViewModel model)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Reset account password given a valid reset code
        /// </summary>
        [HttpPut("ResetPassword/{email}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public ActionResult<UserProfileViewModel> ResetPassword(string email,
                                                                [FromQuery]string code,
                                                                [FromBody]PasswordResetViewModel reset)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check if invitation valid/exists
        /// </summary>
        [AllowAnonymous]
        [HttpGet, Route("ViewInvite/{code:required}")]
        public ActionResult<ViewInviteViewModel> ViewInvite(string code, [FromQuery, BindRequired]string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Accept an invitation to create a user account
        /// </summary>
        [HttpPost("AcceptInvite/{code:required}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult AcceptInvite(string code, [FromForm]ViewInviteViewModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve user's profile information
        /// </summary>
        [HttpGet("Profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public ActionResult<UserProfileViewModel> GetProfile()
        {
            throw new NotImplementedException();
        }

         /// <summary>
        /// Update user profile
        /// </summary>
        [HttpPut("Profile")]
        public IActionResult PutProfile([FromForm]UserProfileViewModel input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change user password
        /// </summary>
        [HttpPut("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult ChangePassword(PasswordChangeViewModel input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ping to test if user still logged in
        /// </summary>
        /// <response code="204">Session still active</response>
        /// <response code="401">Session has ended</response>
        [HttpPost("Ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Ping() => NoContent();
    }
}