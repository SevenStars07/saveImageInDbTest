namespace ImageStorageTest.Outputs;

public class AuctionOutput
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StartingPrice { get; set; }
    public int SellerId { get; set; }
    public List<byte[]> Photos { get; set; }
}