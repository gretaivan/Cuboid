using Cuboid.Business.API;

namespace Cuboid.Business.Tests;

public class PriceServiceTests
{
    [Fact]
    public void Test1AddsAPrice()
    {
        var priceService = new PriceService();

        var addPriceRequest = new AddPriceRequest
        {
            Value = 50,
            Broker = "Broker1",
            Submitter = "Trader3",
        };
        priceService.Process(addPriceRequest, true);

        Assert.Equal(4, priceService.DownstreamService.SentMessages.Count);
    }
}