using DVT.Domain;
using DVT.Services.Interfaces;

namespace DVT.Services
{
    public class ElevatorService
    {
        private readonly IInputService _inputService;
        private readonly IUserInteractionService _userInteractionService;
        public ElevatorService(IInputService inputService, IUserInteractionService userInteractionService)
        {
            _inputService = inputService;
            _userInteractionService = userInteractionService;
        }

        public void RunElevators()
        {
            Console.WriteLine("Hello, welcome to the elevator app.");
            var floorCount = _inputService.InputInterger("Please enter how many floors your building has.");

            var elevators = _userInteractionService.GetElevatorStartPositions(floorCount);
            var continueExecution = true;

            while (continueExecution)
            {
                var floorStates = _userInteractionService.GetFloorStates(floorCount);

                foreach (var floor in floorStates)
                {
                    while (floor.Value.PeopleWaiting > 0)
                    {
                        var eligibleElevator = GetNextElegibleElevator(elevators, floor.Key);

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
                _userInteractionService.PrintCurrentState(elevators, floorStates);

                Console.WriteLine("Would you like to continue running this application y/N");
                var continueResponse = Console.ReadLine().ToLower();
                continueExecution = continueResponse == "y";
            }
        }

        private ElevatorBase? GetNextElegibleElevator(IEnumerable<Elevator> elevators, int floor)
        {
            return elevators.Where(x => x.HasCapacity() && x.CanMove &&
                                     (
                                        (x.MovingDirection == Moving.Stationairy) ||
                                        (x.MovingDirection == Moving.Up && x.CurrentFloor < floor) ||
                                        (x.MovingDirection == Moving.Down && x.CurrentFloor > floor)
                                     )
                                )
                                .OrderBy(x => x.DistanceFromFloor(floor))
                                .FirstOrDefault();
        }
    }
}
