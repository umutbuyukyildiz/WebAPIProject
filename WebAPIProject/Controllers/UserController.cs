using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPIProject.Domain;
using WebAPIProject.Domain.Responses;
using WebAPIProject.Domain.Services;
using WebAPIProject.Resources;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService,IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }


        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            IEnumerable<Claim> claims = User.Claims;

            string userId = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

            UserResponse userResponse = userService.FindById(int.Parse(userId));

            if (userResponse.Success)
            {
                return Ok(userResponse.user);

            }
            else
            {
                return BadRequest(userResponse.Message);
            }





        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser(UserResource userResource)
        {
            User user = mapper.Map<UserResource, User>(userResource);

            UserResponse userResponse = userService.AddUser(user);

            if (userResponse.Success)
            {
                return Ok(userResponse.user);

            }
            else
            {
                return BadRequest(userResponse.Message);
            }


        }



    }
}
