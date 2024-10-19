using AutoMapper;
using Moq;
using Product.Api.Model;
using Product.Api.Data;
using Product.Services.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Service.Api.Tests.Dbset;


namespace Product.Service.Api.Tests
{
    public class ProductsControllerTest
    {
        private readonly Mock<AppDBContext?> _db;
        private Mock<IMapper?> _mapper;
        ResponseDTO? _response = new ResponseDTO();
        private readonly ProductsController _controller;
        public ProductsControllerTest()
        {
            _db = new Mock<AppDBContext>();
            _mapper = new Mock<IMapper>();
            _controller = new ProductsController(_db.Object, _mapper.Object);
        }

        [Fact]
        public async Task Test1()
        {
            //ARRANGE
            var productId = 1;
            var product = new Products { ProductId = productId, Name = "Test Product" };

            // Mocking DbSet for products
            var mockProductsDbSet = new List<Products> { product }.CreateDbSetMock();
            _db.Setup(db => db.products).Returns(mockProductsDbSet.Object);

            // Act
            var result = await _controller.GetProductsById(productId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(productId, ((Products)result.Data).ProductId);
        }
    }
}