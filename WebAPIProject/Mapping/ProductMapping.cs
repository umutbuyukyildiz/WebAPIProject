using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAPIProject.Domain;
using WebAPIProject.Resources;

namespace WebAPIProject.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductResource,Product>();
            CreateMap<Product, ProductResource>();


        }

    }
}
