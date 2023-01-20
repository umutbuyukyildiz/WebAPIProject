using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Domain;
using WebAPIProject.Resources;

namespace WebAPIProject.Mapping
{
    public class UserMapping:Profile
    {

        public UserMapping()
        {
            CreateMap<UserResource,User>();
            CreateMap<User,UserResource>();

        }

    }
}
