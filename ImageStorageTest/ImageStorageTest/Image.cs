namespace ImageStorageTest;

public class Image : IEntity
{
    public int Id { get; set; }
    
    public int AuctionId {get; set;}
    
    // public Auction Auction {get; set;}
    
    // public ImageFile ImageFile {get; set;}
    
    public string ImageFileName {get; set;}
    
    public string FileType { get; set; }
    
    public byte[] DataFiles {get; set;}
}