using DVT.Domain.Interfaces;

namespace DVT.Domain
{
    public class ElevatorBase : IElevatorBase
    {
        public ElevatorBase(int currentFloor, int capacity)
        {
            CurrentFloor = currentFloor;
            Capacity = capacity;
            AvailableCapacity = capacity;
        }

        public int CurrentFloor { get; protected set; }
        public int Capacity { get; protected set; }
        public int AvailableCapacity { get; protected set; }

        public bool HasCapacity() => (AvailableCapacity > 0);

        public int DistanceFromFloor(int floor)
        {
            return Math.Abs(CurrentFloor - floor);
        }

        public void MoveToFloor(int floor, int peopleOnboarding)
        {
            CurrentFloor = floor;
            AvailableCapacity -= peopleOnboarding;
        }

        public void PrintState(int elevatorDesignation)
        {
            string message = $"elevator {elevatorDesignation} is currently on floor {CurrentFloor}, with {Capacity - AvailableCapacity} on board.";
            Console.WriteLine(message);
        }
    }
}
