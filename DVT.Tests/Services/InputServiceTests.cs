using DVT.Services;
using Xunit;

namespace DVT.Tests.Services
{
    public class InputServiceTests
    {
        [Fact]
        public void ItShouldReadInput_AndParseToInt()
        {
            // Arrange
            var inputServicve = new InputService();
            var input = new StringReader("5\n");
            Console.SetIn(input);

            // Act
            var result = inputServicve.InputInterger("Enter a number: ");

            // Assert
            Assert.Equal(5, result);
        }
    }
}
