using Microsoft.AspNetCore.Mvc;
using DepsWebApp.Filters;
using System;
using DepsWebApp.Models;
using System.Net;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Authorization controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [CustomExceptionFilter]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Register method that register a new account
        /// </summary>
        /// <param name="loginData">LoginData model that contains login and password</param>
        /// <exception cref="NotImplementedException"> Temporary: action does not have implementation.</exception>
        [HttpPost]
        [Route("/register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Register([FromBody] LoginData loginData)
        {
            throw new NotImplementedException();
        }
    }
}
