using Xunit;

namespace DVT.Tests.Domain
{
    public class FloorTests
    {
        [Theory]
        [InlineData(3, 1, 2)]
        [InlineData(3, 2, 1)]
        [InlineData(3, 3, 0)]
        public void TestElevatorArrived(int initialPeopleWaiting, int availableCapacity, int expectedPeopleWaiting)
        {
            // Arrange
            var floor = new Floor(initialPeopleWaiting);

            // Act
            floor.ElevatorArrived(availableCapacity);

            // Assert
            Assert.Equal(expectedPeopleWaiting, floor.PeopleWaiting);
        }
    }
}
