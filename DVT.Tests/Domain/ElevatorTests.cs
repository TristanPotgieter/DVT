using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DVT.Tests.Domain
{
    public class ElevatorTests
    {
        [Theory]
        [InlineData(3, 2, 3, 0, Moving.Up, false)]
        [InlineData(0, 1, 0, 1, Moving.Down, false)]
        public void TestMoveToFloor(int floor, int peopleOnboarding, int expectedCurrentFloor,
            int expectedAvailableCapacity, Moving expectedMovingDirection, bool expectedCanMove)
        {
            // Arrange
            var elevator = new Elevator(1, 2);

            // Act
            elevator.MoveToFloor(floor, peopleOnboarding);

            // Assert
            Assert.Equal(expectedCurrentFloor, elevator.CurrentFloor);
            Assert.Equal(expectedAvailableCapacity, elevator.AvailableCapacity);
            Assert.Equal(expectedMovingDirection, elevator.MovingDirection);
            Assert.Equal(expectedCanMove, elevator.CanMove);
        }

        [Fact]
        public void TestPrintState()
        {
            // Arrange
            var elevator = new Elevator(1, 2);
            var output = new StringWriter();

            Console.SetOut(output);

            // Act
            elevator.PrintState(1);

            // Assert
            var expectedOutput = "elevator 1 is currently on floor 1, with 0 on board and is currently stationairy\r\n";
            Assert.Contains(expectedOutput, output.ToString());

            //// Act
            output.GetStringBuilder().Clear();

            elevator.MoveToFloor(2, 1);
            elevator.PrintState(1);

            // Assert
            expectedOutput = "elevator 1 is currently on floor 2, with 1 on board and is currently moving up\r\n";
            Assert.Contains(expectedOutput, output.ToString());
        }
    }
}
