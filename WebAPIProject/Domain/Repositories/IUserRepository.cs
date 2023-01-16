using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject.Domain.Repositories
{
    interface IUserRepository
    {
        Task AddUser(User user);

        User FindById(int userId);

        User FindByEmailandPassword(string email, string password);

        void SaveRefreshToken(int userId, string refreshToken);

        User GetUserWithRefreshToken(string RefreshToken);

        void RemoveRefreshToken(User user);




    }
}
