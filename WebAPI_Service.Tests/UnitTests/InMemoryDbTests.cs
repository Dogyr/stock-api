using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_Service.Controllers;
using WebAPI_Service.Core.Interfaces;
using WebAPI_Service.DTO;
using Xunit;

namespace WebAPI_Service.Tests.UnitTests
{
    public class InMemoryDbTests
    {
        private readonly IUomRepository repo;
        private readonly FakeDbContext fakeDbContext;
        private readonly ProductUomController uomController;

        public InMemoryDbTests()
        {
            fakeDbContext = new FakeDbContext();
            fakeDbContext.Database.EnsureDeleted();
            repo = new FakeUomRepository(fakeDbContext);
            uomController = new ProductUomController(repo);
        }

        [Fact]
        public async Task TestGetAllLines()
        {
            var result = await uomController.Get();

            Assert.IsType<ActionResult<IEnumerable<ProductUomDto>>>(result);
        }
    }
}
