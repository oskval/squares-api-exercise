using Application.Repositories;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence;

namespace IntegrationTests
{
    [TestFixture]
    public class CoordinatesServiceTests
    {
        private readonly DataContext _dataContext;
        private readonly CoordinatesRepository _coordinatesRepository;
        private readonly CoordinatesService _coordinatesService;

        public CoordinatesServiceTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options;

            _dataContext = new DataContext(options);
            _coordinatesRepository = new CoordinatesRepository(_dataContext);
            _coordinatesService = new CoordinatesService(_coordinatesRepository);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}