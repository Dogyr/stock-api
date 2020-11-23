using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebAPI_Service.Controllers;
using WebAPI_Service.Core.DataModels;
using WebAPI_Service.Core.Interfaces;
using WebAPI_Service.DTO;
using Xunit;

namespace WebAPI_Service.Tests.UnitTests
{
    public class ProductControllerTests
    {
        private ProductController productController;
        private Mock<IProductRepository> repository;
        private IFixture fixture;

        public ProductControllerTests()
        {
            fixture = new Fixture();
            repository = new Mock<IProductRepository>();
            productController = new ProductController(repository.Object);
        }

        [Fact]
        public async void TestGetAllLines()
        {
            repository
                .Setup(x => x.GetProductAsync())
                .Returns(() => Task.FromResult(fixture.Build<Product>().Without(x => x.ProductMovements).Without(c => c.Uom).CreateMany()));

            var result = await productController.Get();

            repository.Verify(x => x.GetProductAsync(),
                Times.Once());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void TestGetByIdIsOk()
        {
            repository
                .Setup(x => x.GetProductAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult(fixture.Build<Product>().Without(x => x.ProductMovements).Without(c => c.Uom).Create()));

            var result = await productController.Get(1);
            repository.Verify(x => x.GetProductAsync(It.IsAny<int>()),
                Times.Once());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void TestGetByIdIsNotFound()
        {
            repository
                .Setup(x => x.GetProductAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult<Product>(default));

            var result = await productController.Get(1);
            repository.Verify(x => x.GetProductAsync(It.IsAny<int>()),
                Times.Once());

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void TestPostProductIsOk()
        {
            repository
                .Setup(x => x.AddProductAsync(It.IsAny<Product>()))
                .Returns(() => Task.FromResult<Product>(fixture.Build<Product>().Without(x => x.ProductMovements).Without(c => c.Uom).Create()));
            var name = new SpecimenContext(fixture)
                .Resolve(new RegularExpressionRequest("^[А-ЯЁа-яё0-9]+$"));

            var result = await productController.Post(fixture.Build<CreateProductDto>().With(x => x.UomId, 2).With(x => x.Name, name).Create());
            repository.Verify(x => x.AddProductAsync(It.IsAny<Product>()),
                Times.Once());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void TestPostProductIsBadReq()
        {
            var result = await productController.Post(fixture.Build<CreateProductDto>().With(x => x.UomId, 2).Without(x => x.Name).Create());
            repository.Verify(x => x.AddProductAsync(It.IsAny<Product>()),
                Times.Never());

            Assert.IsType<BadRequestResult>(result.Result);
        }
        
        [Fact]
        public async void TestPutProductIsOk()
        {
            repository
                .Setup(x => x.UpdateProductAsync(It.IsAny<Product>()))
                .Returns(() => Task.FromResult<bool>(true));
            var name = new SpecimenContext(fixture)
                .Resolve(new RegularExpressionRequest("^[А-ЯЁа-яё0-9]+$"));

            var result = await productController.Put(fixture.Build<ProductDto>().With(x => x.UomId, 2).With(x => x.Name, name).With(x => x.Id, 4).Create());
            repository.Verify(x => x.UpdateProductAsync(It.IsAny<Product>()),
                Times.Once());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void TestPutProductIsBadReq()
        {
            var result = await productController.Put(fixture.Build<ProductDto>().Without(x => x.Id).Create());
            repository.Verify(x => x.UpdateProductAsync(It.IsAny<Product>()),
                Times.Never());

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task TestDelProductIsOk()
        {
            repository
                .Setup(x => x.DeleteProductAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult<bool>(true));

            var result = await productController.Delete(1);
            repository.Verify(x => x.DeleteProductAsync(It.IsAny<int>()), Times.Once);

            Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public async Task TestDelProductIsNotFound()
        {
            repository
                .Setup(x => x.DeleteProductAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult<bool>(false));

            var result = await productController.Delete(1);
            repository.Verify(x => x.DeleteProductAsync(It.IsAny<int>()), Times.Once);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
