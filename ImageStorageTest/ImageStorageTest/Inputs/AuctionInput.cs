namespace ImageStorageTest.Inputs;

public class AuctionInput
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StartingPrice { get; set; }
    public int SellerId { get; set; }
    public ICollection<IFormFile> Photos { get; set; }
}