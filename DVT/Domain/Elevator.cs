using DVT.Domain;

public sealed class Elevator : ElevatorBase
{
    public Elevator(int currentFloor, int capacity) : base(currentFloor, capacity)
    {
        MovingDirection = Moving.Stationairy;
        CanMove = true;
    }

    public Moving MovingDirection { get; private set; }
    public bool CanMove { get; private set; }


    new public void MoveToFloor(int floor, int peopleOnboarding)
    {
        MovingDirection = floor > CurrentFloor ? Moving.Up : Moving.Down;
        base.MoveToFloor(floor, peopleOnboarding);
        CanMove = false;
    }

    public void Stop()
    {
        MovingDirection = Moving.Stationairy;
    }

    new public void PrintState(int elevatorDesignation)
    {
        string message = $"elevator {elevatorDesignation} is currently on floor {CurrentFloor}, " +
                         $"with {Capacity - AvailableCapacity} on board and is currently ";

        message += MovingDirection == Moving.Stationairy ? "stationairy" : $"moving {MovingDirection.ToString().ToLower()}";
        Console.WriteLine(message);
    }
}