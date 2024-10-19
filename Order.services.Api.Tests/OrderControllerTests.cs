using Moq;
using Order.Services.Api.Controllers;
using Order.Services.Api.Data;
using Order.Services.Api.Services.IService;
namespace Order.services.Api.Tests
{
    public class OrderControllerTests
    {
        private readonly Mock<IProductSerivce> _productSerivce;
        private readonly Mock<AppDBContext> _mockOrderService;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrderController(_mockOrderService.Object);
        }
        [Fact]
        public void Test1()
        {
            //TRIPLE AAA
            //ARRANGE



        }
    }
}