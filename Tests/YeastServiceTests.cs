using Contracts;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using Services;

namespace Tests
{
    public class YeastServiceTests
    {

        private readonly YeastService _systemUnderTest;

        private readonly Mock<IRepositoryManager> _repoManagerMock = new Mock<IRepositoryManager>();

        
        public YeastServiceTests()
        {
            _systemUnderTest = new YeastService(_repoManagerMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnYeast_WhenYeastExists()
        {
            //Arrange
            var yeastId = Guid.NewGuid();
            var mockYeast = new Yeast("Test", "Test", "Test")
            {
                Id = yeastId,
                NutrientReqMult = 0.5,
                IsDeleted = false,
            };

            _repoManagerMock.Setup(x => x.YeastRepository.GetByIdAsync(yeastId, new CancellationToken()))
                .ReturnsAsync(mockYeast);

            //Act
            var YeastDto = await _systemUnderTest.GetByIdAsync(yeastId);

            //Assert
            Assert.Equal(yeastId, mockYeast.Id);
        }
    }
}