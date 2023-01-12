using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Domain.Services;
using AutoMapper;
using WebAPIProject.Domain.Responses;
using WebAPIProject.Resources;
using WebAPIProject.Extensions;
using WebAPIProject.Domain;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        // get localhost:3333/api/product/2
        // post localhost:3333/api/product
        //put  localhost:3333/api/product request body
        //delete localhost:3333/api/product/removeProduct/



        private readonly IProductService productService;
        private readonly IMapper mapper;
        public ProductController(IProductService productService,IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }
    

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            ProductListResponse productListResponse = await productService.ListAsync();

            if (productListResponse.Success)
            {
                return Ok(productListResponse.productList);
            }
            else
            {
                return BadRequest(productListResponse.Message);
            }
        } 

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFindById(int id)
        {
            ProductResponse productResponse = await productService.FindByIdAsync(id);

            if (productResponse.Success)
            {
                return Ok(productResponse.Product);
            }
            else
            {
                return BadRequest(productResponse.Message);
            }
        } 


        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductResource productResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());

            }
            else
            {
                Product product = mapper.Map<ProductResource, Product>(productResource);

                var Response = await productService.AddProduct(product);

                if (Response.Success)
                {
                    return Ok(Response.Product);
                }
                else
                {
                    return BadRequest(Response.Message);
                }

            }

        } 

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(ProductResource productResource,int id)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Product product = mapper.Map<ProductResource, Product>(productResource);

                var response = await productService.UpdateProduct(product, id);

                if (response.Success)
                {
                    return Ok(response.Product);
                }
                else
                {
                    return BadRequest(response.Message);
                }

            }
        }  

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            ProductResponse response = await productService.RemoveProduct(id);

            if (response.Success)
            {
                return Ok(response.Product);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    



    }
}
