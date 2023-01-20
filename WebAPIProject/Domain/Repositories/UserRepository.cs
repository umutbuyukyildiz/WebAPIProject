
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Security.Token;

namespace WebAPIProject.Domain.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly TokenOptions tokenOptions;

        public UserRepository(WebAPIProjectContext context,IOptions<TokenOptions> tokenOptions) : base(context)
        {
            this.tokenOptions = tokenOptions.Value;
        }



        public void AddUser(User user)
        {
            context.User.Add(user);
        }

        public User FindByEmailandPassword(string email, string password)
        {
           return context.User.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        }

        public User FindById(int userId)
        {
            return context.User.Find(userId);
        }

        public User GetUserWithRefreshToken(string refreshToken)
        {
            return context.User.FirstOrDefault(u => u.RefreshToken == refreshToken);
        }

        public void RemoveRefreshToken(User user)
        {
            User newUser = this.FindById(user.Id);
            newUser.RefreshToken = null;
            newUser.RefreshTokenEndDate = null;
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {

            User newUser = this.FindById(userId);
            newUser.RefreshToken = refreshToken;

            newUser.RefreshTokenEndDate = DateTime.Now.AddMinutes(tokenOptions.RefreshTokenExpiration);

        }
    }

  

}
