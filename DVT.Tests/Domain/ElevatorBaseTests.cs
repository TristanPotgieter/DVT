using DVT.Domain;
using System.Text;
using Xunit;

namespace DVT.Tests.Domain
{
    public class ElevatorBaseTests
    {
        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public void TestHasCapacity(int peopleOnboarding, bool expectedHasCapacity)
        {
            // Arrange
            var elevator = new ElevatorBase(1, 2);

            // Act
            elevator.MoveToFloor(2, peopleOnboarding);

            // Assert
            Assert.Equal(expectedHasCapacity, elevator.HasCapacity());
        }

        [Fact]
        public void TestDistanceFromFloor()
        {
            var elevator = new ElevatorBase(1, 2);

            Assert.Equal(0, elevator.DistanceFromFloor(1));
            Assert.Equal(1, elevator.DistanceFromFloor(2));
            Assert.Equal(2, elevator.DistanceFromFloor(3));
        }

        [Theory]
        [InlineData(2, 1, 2, 1)]
        [InlineData(3, 2, 3, 0)]
        public void TestMoveToFloor(int floor, int peopleOnboarding, int expectedCurrentFloor, int expectedAvailableCapacity)
        {
            // Arrange
            var elevator = new ElevatorBase(1, 2);

            // Act
            elevator.MoveToFloor(floor, peopleOnboarding);

            // Assert
            Assert.Equal(expectedCurrentFloor, elevator.CurrentFloor);
            Assert.Equal(expectedAvailableCapacity, elevator.AvailableCapacity);
        }

        [Fact]
        public void TestPrintState()
        {
            var elevator = new ElevatorBase(1, 2);
            var output = new StringWriter();
            Console.SetOut(output);

            elevator.PrintState(1);

            var expectedOutput = "elevator 1 is currently on floor 1, with 0 on board.";
            Assert.Contains(expectedOutput, output.ToString().Replace(System.Environment.NewLine, string.Empty));
        }
    }
}
