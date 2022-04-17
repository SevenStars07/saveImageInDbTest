using ImageStorageTest.Inputs;
using ImageStorageTest.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace ImageStorageTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionController
{
    public TestContext Context;
    
    public AuctionController(TestContext context)
    {
        Context = context;
    }
    
    [HttpGet]
    public IActionResult GetAuction([FromQuery] int id)
    {
        var auction = Context.Auctions.FirstOrDefault(a => a.Id == id);
        
        if (auction == null)
        {
            return new NotFoundResult();
        }
        
        var images = Context.Images.Where(i => i.AuctionId == id).ToList();

        var output = new AuctionOutput
        {
            Name = auction.Name,
            Description = auction.Description,
            Photos = images.Select(i => i.DataFiles).ToList(),
            StartingPrice = auction.StartingPrice,
            StartDate = auction.StartDate,
            EndDate = auction.EndDate,
            SellerId = auction.SellerId
        };

        return new OkObjectResult(output);

    }
    
    [HttpPost]
    public IActionResult CreateAuction([FromForm] AuctionInput auctionInput)
    {
        var auction = new Auction
        {
            Name = auctionInput.Name,
            Description = auctionInput.Description,
            StartDate = auctionInput.StartDate,
            EndDate = auctionInput.EndDate,
            StartingPrice = auctionInput.StartingPrice,
            SellerId = auctionInput.SellerId,
        };

        Context.Add(auction);
        Context.SaveChanges();
        
        foreach (var image in auctionInput.Photos)
        {
            var fileName = Path.GetFileName(image.FileName);
            var fileExtension = Path.GetExtension(image.FileName);
            var filePath = String.Concat(fileName, fileExtension);
            
            var photoEntity = new Image
            {
                AuctionId = auction.Id,
                ImageFileName = filePath,
                FileType = fileExtension
            };

            using (var target = new MemoryStream())
            {
                image.CopyTo(target);
                photoEntity.DataFiles = target.ToArray();
            }
            
            Context.Add(photoEntity);
            Context.SaveChanges();
        }
        
        return new OkObjectResult(auction);
    }
}