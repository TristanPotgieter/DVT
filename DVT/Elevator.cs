internal partial class Program
{
    private class Elevator
    {
        public Elevator(int currentFloor, int capacity)
        {
            CurrentFloor = currentFloor;
            Capacity = capacity;
            AvailableCapacity = capacity;
            MovingDirection = Moving.Stationairy;
            CanMove = true;
        }

        public int CurrentFloor { get; private set; }
        public int Capacity { get; private set; }
        public int AvailableCapacity { get; private set; }
        public Moving MovingDirection { get; private set; }
        public bool CanMove { get; set; }

        public int DistanceFromFloor(int floor)
        {
            return Math.Abs(CurrentFloor - floor);
        }

        public bool HasCapacity() => (AvailableCapacity > 0);

        public void MoveToFloor(int floor, int peopleOnboarding )
        {
            MovingDirection = floor == 0? Moving.Stationairy:(floor > CurrentFloor? Moving.Up : Moving.Down);
            CurrentFloor = floor;
            AvailableCapacity -= peopleOnboarding;
            CanMove = false;
        }

        public void Stop()
        {
            MovingDirection = Moving.Stationairy;
        }
    }
    public enum Moving{
        Stationairy,
        Up,
        Down
    }
}