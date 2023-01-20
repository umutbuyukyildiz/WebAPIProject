using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Domain.Responses;
using WebAPIProject.Domain.Services;
using WebAPIProject.Security.Token;

namespace WebAPIProject.Services
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly IUserService userService;

        private readonly ITokenHandler tokenHandler;

        public AuthenticationService(IUserService userService,ITokenHandler tokenHandler)
        {
            this.userService = userService;
            this.tokenHandler = tokenHandler;
        }

        public AccessTokenResponse CreateAccessToken(string email, string password)
        {
            UserResponse userResponse = userService.FindEmailandPassword(email, password);

            if (userResponse.Success)
            {
                AccessToken accessToken = tokenHandler.CreateAccessToken(userResponse.user);

                userService.SaveRefreshToken(userResponse.user.Id,accessToken.RefreshToken);

                return new AccessTokenResponse(accessToken);


            }
            else
            {
                return new AccessTokenResponse(userResponse.Message);
            }


        }

        public AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken)
        {
            UserResponse userResponse = userService.GetUserWithRefreshToken(refreshToken);
            if (userResponse.Success)
            {
                if (userResponse.user.RefreshTokenEndDate > DateTime.Now)
                {
                    AccessToken accessToken = tokenHandler.CreateAccessToken(userResponse.user);

                    userService.SaveRefreshToken(userResponse.user.Id, accessToken.RefreshToken);

                    return new AccessTokenResponse(accessToken);


                }
                else
                {
                    return new AccessTokenResponse("RefreshToken süresi dolmuştur.");
                }
            }
            else
            {
                return new AccessTokenResponse("RefreshToken bulunamadı");
            }
        }

        public AccessTokenResponse RevokeRefreshToken(string refreshToken)
        {
            UserResponse userResponse = userService.GetUserWithRefreshToken(refreshToken);
            if (userResponse.Success)
            {
                userService.RemoveRefreshToken(userResponse.user);

                return new AccessTokenResponse(new AccessToken());

            }
            else
            {
                return new AccessTokenResponse("refreshtoken bulunamadı.");
            }
        }
    }
}
