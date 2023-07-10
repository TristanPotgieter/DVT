using DVT.Domain;
using DVT.Services.Interfaces;

namespace DVT.Services
{
    public class UserInteractionService : IUserInteractionService
    {

        private readonly IInputService _inputService;
        public UserInteractionService(IInputService inputService)
        {
            _inputService = inputService;
        }

        public IEnumerable<Elevator> GetElevatorStartPositions(int numberOfFloors)
        {
            var numberOfElevators = _inputService.InputInterger("Please enter how many elevators your building has.");
            var elevatorCapacity = _inputService.InputInterger("Please enter how many people each elevator can carry.");

            var elevatorList = new List<Elevator>();
            var counter = 0;

            while (counter < numberOfElevators)
            {
                var elevatorFloor = _inputService.InputInterger($"On which floor is elevator {counter + 1}");

                if (elevatorFloor > numberOfFloors || elevatorFloor < 0)
                {
                    Console.WriteLine("The floor you entered is not valid");
                    continue;
                }

                elevatorList.Add(new Elevator(elevatorFloor, elevatorCapacity));
                counter++;
            }

            return elevatorList;
        }

        public Dictionary<int, Floor> GetFloorStates(int numberOfFloors)
        {
            var counter = 0;
            var dictionaryOfFloors = new Dictionary<int, Floor>();

            while (counter <= numberOfFloors)
            {
                var peopleWaiting = _inputService.InputInterger($"How many people are waiting on floor number {counter}");

                dictionaryOfFloors.Add(counter, new Floor(peopleWaiting));
                counter++;
            }

            return dictionaryOfFloors;
        }

        public void PrintCurrentState(IEnumerable<ElevatorBase> elevators, IDictionary<int, Floor> floors)
        {
            var elevatorCounter = 1;
            foreach (var elevator in elevators)
            {
                elevator.PrintState(elevatorCounter);
                elevatorCounter++;
            }
            foreach (var floor in floors)
            {
                if (floor.Value.PeopleWaiting > 0)
                {
                    Console.WriteLine($"There are still {floor.Value.PeopleWaiting} waiting on floor {floor.Key}");
                }
            }
        }
    }
}
