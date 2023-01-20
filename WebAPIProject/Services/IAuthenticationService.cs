using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Domain.Responses;

namespace WebAPIProject.Services
{
    public interface IAuthenticationService
    {
        AccessTokenResponse CreateAccessToken(string email, string password);

        AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken);

        AccessTokenResponse RevokeRefreshToken(string refreshToken);



    }
}
