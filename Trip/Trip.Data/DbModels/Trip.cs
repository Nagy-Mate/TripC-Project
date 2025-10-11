namespace Trip.Data.DbModels;

public class Trip
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int DestinationId { get; set; }
    public Destination Destination { get; set; }
}
