using static Program;

namespace DVT.Services
{
    internal class ElevatorService
    {
        private readonly IInputService _inputService;
        public ElevatorService(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void RunElevators()
        {
            Console.WriteLine("Hello, welcome to the elevator app.");
            var elevatorCount = _inputService.InputInterger("Please enter how many elevators your building has.");
            var elevatorCapacity = _inputService.InputInterger("Please enter how many people each elevator can carry.");
            var floorCount = _inputService.InputInterger("Please enter how many floors your building has.");

            var elevators = SetElevatorStartPositions(elevatorCount, elevatorCapacity, floorCount);

            while (true)
            {
                var floorStates = SetFloorStates(floorCount);

                foreach (var floor in floorStates)
                {
                    while (floor.Value.PeopleWaiting > 0)
                    {
                        var eligibleElevator = elevators
                            .Where(x => x.HasCapacity() && x.CanMove &&
                                    (
                                        (x.MovingDirection == Moving.Stationairy) ||
                                        (x.MovingDirection == Moving.Up && x.CurrentFloor < floor.Key) ||
                                        (x.MovingDirection == Moving.Down && x.CurrentFloor > floor.Key)
                                    )
                            )
                            .OrderBy(x => x.DistanceFromFloor(floor.Key))
                            .FirstOrDefault();

                        if (eligibleElevator == null)
                        {
                            Console.WriteLine($"There are no elevators available for floor{floor.Key}");
                            break;
                        }
                        var sizeFlag = eligibleElevator.AvailableCapacity - floor.Value.PeopleWaiting;
                        if (sizeFlag > 0)
                        {
                            floor.Value.ElevatorArrived(floor.Value.PeopleWaiting);
                            eligibleElevator.MoveToFloor(floor.Key, floor.Value.PeopleWaiting);
                        }
                        else
                        {
                            floor.Value.ElevatorArrived(eligibleElevator.AvailableCapacity);
                            eligibleElevator.MoveToFloor(floor.Key, eligibleElevator.AvailableCapacity);
                        }
                    };

                }
                PrintCurrentState(elevators, floorStates);
            }
        }
        private IEnumerable<Elevator> SetElevatorStartPositions(int numberOfElevators, int elevatorCapacity, int numberOfFloors)
        {
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

        private Dictionary<int, Floor> SetFloorStates(int numberOfFloors)
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

        private static void PrintCurrentState(IEnumerable<Elevator> elevators, IDictionary<int, Floor> floors)
        {
            var elevatorCounter = 1;
            foreach (var elevator in elevators)
            {
                string message = $"elevator {elevatorCounter} is currently on floor {elevator.CurrentFloor}, " +
                    $"with {elevator.Capacity - elevator.AvailableCapacity} on board and is currently ";

                message += elevator.MovingDirection == Moving.Stationairy ? "stationairy" : $"moving {elevator.MovingDirection.ToString().ToLower()}";
                Console.WriteLine(message);
                elevator.Stop();
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
