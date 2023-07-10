using DVT.Domain;

namespace DVT.Services.Interfaces
{
    public interface IUserInteractionService
    {
        IEnumerable<Elevator> GetElevatorStartPositions(int numberOfFloors);

        Dictionary<int, Floor> GetFloorStates(int numberOfFloors);

        void PrintCurrentState(IEnumerable<ElevatorBase> elevators, IDictionary<int, Floor> floors);
    }
}