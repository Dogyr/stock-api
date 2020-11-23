using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_Service.Controllers;
using WebAPI_Service.Core.DataModels;
using WebAPI_Service.Core.Interfaces;
using WebAPI_Service.DTO;
using Xunit;

namespace WebAPI_Service.Tests.UnitTests
{
    public class UomControllerTests
    {
        private ProductUomController uomController;
        private Mock<IUomRepository> repository;
        private IFixture fixture;

        public UomControllerTests()
        {
            fixture = new Fixture();
            repository = new Mock<IUomRepository>();
            uomController = new ProductUomController(repository.Object);
        }

        [Fact]
        public async Task TestGetAllLines()
        {
            repository
                .Setup(x => x.GetUomAsync())
                .Returns(() => Task.FromResult(fixture.Build<ProductUom>().Without(x => x.Products).CreateMany()));

            var result = await uomController.Get();

            repository.Verify(x => x.GetUomAsync(),
                Times.Once);
            var viewResult = Assert.IsType<ActionResult<IEnumerable<ProductUomDto>>>(result);
        }

        [Fact]
        public async Task TestGetByIdIsOk()
        {
            repository
                .Setup(x => x.GetUomAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult(fixture.Build<ProductUom>().Without(x => x.Products).Create()));

            var result = await uomController.Get(2);

            repository.Verify(x => x.GetUomAsync(It.IsAny<int>()),
                Times.Once);
            var viewResult = Assert.IsType<ActionResult<ProductUomDto>>(result);
        }

        [Fact]
        public async Task TestGetByIdIsNotFound()
        {
            repository
                .Setup(x => x.GetUomAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult<ProductUom>(default));

            var result = await uomController.Get(2);
            repository.Verify(x => x.GetUomAsync(It.IsAny<int>()),
                Times.Once);
            var viewResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task TestPostUomIsBadReq()
        {
            var result = await uomController.Post(fixture.Build<CreateUomDto>().Without(x => x.Title).Create());

            repository.Verify(x => x.AddUomAsync(It.IsAny<ProductUom>()), Times.Never);
            var viewResult = Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task TestPostUomIsOk()
        {
            repository
                .Setup(x => x.AddUomAsync(It.IsAny<ProductUom>()))
                .Returns(() => Task.FromResult(fixture.Build<ProductUom>().Without(x => x.Products).Create()));

            var title = new SpecimenContext(fixture)
                .Resolve(new RegularExpressionRequest("^[А-ЯЁа-яё]+$"));
            fixture.Customize<CreateUomDto>(x => x.With(x => x.Title, title));

            var result = await uomController.Post(fixture.Create<CreateUomDto>());
            repository.Verify(x => x.AddUomAsync(It.IsAny<ProductUom>()), Times.Once);

            var viewResult = Assert.IsType<ActionResult<ProductUomDto>>(result);
        }

        [Fact]
        public async void TestPutUomIsOk()
        {
            repository
                .Setup(x => x.UpdateUomAsync(It.IsAny<ProductUom>()))
                .Returns(() => Task.FromResult<bool>(true));
            var title = new SpecimenContext(fixture)
                .Resolve(new RegularExpressionRequest("^[А-ЯЁа-яё]+$"));

            var result = await uomController.Put(fixture.Build<ProductUomDto>().With(x => x.Id, 2).With(x => x.Title, title).Create());
            repository.Verify(x => x.UpdateUomAsync(It.IsAny<ProductUom>()),
                Times.Once());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void TestPutUomIsBadReq()
        {
            var result = await uomController.Put(fixture.Build<ProductUomDto>().Without(x => x.Id).Create());
            repository.Verify(x => x.UpdateUomAsync(It.IsAny<ProductUom>()),
                Times.Never());

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task TestDelUomIsNotFound()
        {
            repository
                .Setup(x => x.DeleteUomAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult<bool>(default));

            var result = await uomController.Delete(1);
            repository.Verify(x => x.DeleteUomAsync(It.IsAny<int>()), Times.Once);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestDelUomIsOk()
        {
            repository
                .Setup(x => x.DeleteUomAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult<bool>(true));

            var result = await uomController.Delete(1);
            repository.Verify(x => x.DeleteUomAsync(It.IsAny<int>()), Times.Once);

            Assert.IsType<OkResult>(result);
        }
    }
}
