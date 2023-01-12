using System;
using System.Collections;
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
    public class ProductService : IProductService
    {

        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProductResponse> AddProduct(Product product)
        {
            try
            {
                await productRepository.AddProductAsync(product);


                await unitOfWork.CompleteAsync();

                return new ProductResponse(product);

            }
            catch (Exception ex)
            {

                return new ProductResponse($"Ürün eklenirken bir hata meydana geldi::{ex.Message}");
            }
        }

        public async Task<ProductResponse> FindByIdAsync(int productId)
        {
            try
            {
                Product product = await productRepository.FindByIdAsync(productId);

                if (product==null)
                {
                    return new ProductResponse("Ürün Bulunamamıştır.");
                }

                return new ProductResponse(product);


            }
            catch (Exception ex)
            {

                return new ProductResponse($"Ürün bulunurken bir hata meydana geldi::{ex.Message }");
            }
        }

        public async Task<ProductListResponse> ListAsync()
        {
            try
            {
                IEnumerable<Product> products = await productRepository.ListAsync();

                return new ProductListResponse(products);

            }
            catch (Exception ex)
            {

                return new ProductListResponse($"Ürün Listelenirken bir hata meydana geldi::{ ex.Message}");
            }
        }

        public async Task<ProductResponse> RemoveProduct(int productId)
        {
            try
            {
                Product product = await productRepository.FindByIdAsync(productId);

                if (product == null)
                {
                    return new ProductResponse("Silmeye çalıştığınız ürün bulunamamıştır");
                }
                else
                {
                    await productRepository.RemoveProductAsync(productId);
                    await unitOfWork.CompleteAsync();
                    return new ProductResponse(product);

                }
            }
            catch (Exception ex)
            {

                return new ProductResponse($"Ürün silinmeye çalışırken bir hata meydana geldi::{ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateProduct(Product product, int productId)
        {
            try
            {
                var firstProduct = await productRepository.FindByIdAsync(productId);

                if (firstProduct==null)
                {
                    return new ProductResponse("Güncellemeye çalıştığınız ürün bulunamadı");
                }
                firstProduct.Name = product.Name;
                firstProduct.Category = product.Category;
                firstProduct.Price = product.Price;

                productRepository.UpdateProduct(firstProduct);


                await unitOfWork.CompleteAsync();

                return new ProductResponse(firstProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün güncellenmeye çalışırken bir hata meydana geldi::{ex.Message}");

            }
        }
    }
}
