using DVT.Services.Interfaces;

namespace DVT.Services
{
    public class InputService : IInputService
    {
        public int InputInterger(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                var intValue = Console.ReadLine();
                var canParse = int.TryParse(intValue, out int response);
                if (canParse)
                {
                    return response;
                }
                Console.WriteLine("Input was not valid.");
                return InputInterger(message);
            }
        }
    }
}
