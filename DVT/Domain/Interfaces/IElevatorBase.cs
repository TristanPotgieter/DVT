namespace DVT.Domain.Interfaces
{
    public interface IElevatorBase
    {
        void MoveToFloor(int floor, int peopleOnboarding);
        int DistanceFromFloor(int floor);
        bool HasCapacity();
        void PrintState(int elevatorDesignation);
    }
}
