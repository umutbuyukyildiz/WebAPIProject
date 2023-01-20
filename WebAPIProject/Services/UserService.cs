using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Domain;
using WebAPIProject.Domain.Repositories;
using WebAPIProject.Domain.Responses;
using WebAPIProject.Domain.Services;
using WebAPIProject.Domain.UnitOfWork;

namespace WebAPIProject.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;

        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository userRepository,IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }        


        public UserResponse AddUser(User user)
        {
            try
            {
                userRepository.AddUser(user);
                unitOfWork.Complete();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {

                return new UserResponse($"Kullanıcı eklenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public UserResponse FindById(int userId)
        {
            try
            {
                User user = userRepository.FindById(userId);

                if (user==null)
                {
                    return new UserResponse("Kullanıcı bulunamadı");
                }

                return new UserResponse(user);



            }
            catch (Exception ex)
            {

                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public UserResponse FindEmailandPassword(string email, string password)
        {
            try
            {
                User user = userRepository.FindByEmailandPassword(email, password);

                if (user == null)
                {
                    return new UserResponse("Kullanıcı Bulunamadı");
                }
                else
                {
                    return new UserResponse(user);
                }
            }
            catch (Exception ex)
            {

                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public UserResponse GetUserWithRefreshToken(string refreshToken)
        {
            try
            {
                User user = userRepository.GetUserWithRefreshToken(refreshToken);

                if (user == null)
                {
                    return new UserResponse("Kullanıcı Bulunamadı");
                }
                else
                {
                    return new UserResponse(user);
                }



            }
            catch (Exception ex)
            {

                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public void RemoveRefreshToken(User user)
        {
            try
            {
                userRepository.RemoveRefreshToken(user);
                unitOfWork.Complete();
            }
            catch (Exception )
            {

               //loglama yapılacaktır.
            }
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            try
            {
                userRepository.SaveRefreshToken(userId, refreshToken);
                unitOfWork.Complete();
            }
            catch (Exception)
            {

                //loglama yapılacaktır..
            }
        }
    }
}
