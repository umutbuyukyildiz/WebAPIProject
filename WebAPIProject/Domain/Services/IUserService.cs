using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Domain.Responses;

namespace WebAPIProject.Domain.Services
{
   public interface IUserService
    {
        UserResponse AddUser(User user);
        UserResponse FindById(int userId);

        UserResponse FindEmailandPassword(string email, string password);

        void SaveRefreshToken(int userId, string refreshToken);

        UserResponse GetUserWithRefreshToken(string refreshToken);
        void RemoveRefreshToken(User user);

    }
}
