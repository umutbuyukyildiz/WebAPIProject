using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Domain;

namespace WebAPIProject.Security.Token
{
    interface ITokenHandler
    {

        AccessToken CreateAccessToken(User user);


       void RevokeRefreshToken(User user);
        
    }
}
