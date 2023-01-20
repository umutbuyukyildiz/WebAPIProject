using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Resources;
using WebAPIProject.Services;
using WebAPIProject.Extensions;
using WebAPIProject.Domain.Responses;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        [HttpPost]
        public IActionResult AccessToken(LoginResource loginResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
               AccessTokenResponse accesTokenResponse=  authenticationService.CreateAccessToken(loginResource.Email, loginResource.Password);

                if (accesTokenResponse.Success)
                {
                    return Ok(accesTokenResponse.accessToken);
                }
                else
                {
                    return BadRequest(accesTokenResponse.Message);
                }

            }




        }

        [HttpPost]
        public IActionResult RefreshToken(TokenResource tokenResource)
        {
          AccessTokenResponse accessTokenResponse  = authenticationService.CreateAccessTokenByRefreshToken(tokenResource.RefreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.accessToken);
            }
            else
            {
                return BadRequest(accessTokenResponse.Message);
            }
        }

        [HttpPost]
        public IActionResult RevokeRefreshToken(TokenResource tokenResource)
        {

            AccessTokenResponse accessTokenResponse =authenticationService.RevokeRefreshToken(tokenResource.RefreshToken);

            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.accessToken);
            }
            else
            {
                return BadRequest(accessTokenResponse.Message);
            }

            
        }





    }
}
