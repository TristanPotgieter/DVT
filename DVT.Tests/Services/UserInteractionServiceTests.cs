using DVT.Domain;
using DVT.Services;
using DVT.Services.Interfaces;
using Moq;
using Xunit;

namespace DVT.Tests.Services
{
    public class UserInteractionServiceTests
    {
        [Fact]
        public void ItSHould_InstanciateElevatorsBasedOnUserInputs()
        {
            // Arrange
            var inputServiceMock = new Mock<IInputService>();
            inputServiceMock.SetupSequence(x => x.InputInterger(It.IsAny<string>()))
                .Returns(2)
                .Returns(5)
                .Returns(1)
                .Returns(3);

            var userInteractionService = new UserInteractionService(inputServiceMock.Object);

            // Act
            var result = userInteractionService.GetElevatorStartPositions(5).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].CurrentFloor);
            Assert.Equal(5, result[0].Capacity);
            Assert.Equal(3, result[1].CurrentFloor);
            Assert.Equal(5, result[1].Capacity);
        }

        [Fact]
        public void ItSHould_InstanciateFloorsBasedOnUserInputs()
        {
            // Arrange
            var inputServiceMock = new Mock<IInputService>();
            inputServiceMock.SetupSequence(x => x.InputInterger(It.IsAny<string>()))
                .Returns(2)
                .Returns(3)
                .Returns(4);

            var userInteractionService = new UserInteractionService(inputServiceMock.Object);

            // Act
            var result = userInteractionService.GetFloorStates(2);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(2, result[0].PeopleWaiting);
            Assert.Equal(3, result[1].PeopleWaiting);
            Assert.Equal(4, result[2].PeopleWaiting);
        }       
    }
}

