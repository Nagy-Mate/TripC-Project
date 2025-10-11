namespace Trip.Data.DbModels;

public class Destination
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Description { get; set; }
    public virtual List<Trip> Trips { get; set; } = new List<Trip>();
}
