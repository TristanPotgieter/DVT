public class Floor
{
    public Floor(int peopleWaiting)
    {
        PeopleWaiting = peopleWaiting;
    }
    public int PeopleWaiting { get; private set; }

    public void ElevatorArrived(int availableCapacity)
    {
        PeopleWaiting -= availableCapacity;
    }
}
