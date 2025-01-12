using Cuboid.Business.Application.DTOs;
using Cuboid.Business.Application.Services;

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

        var sentMessages = priceService.DownstreamService.GetSentMessages();
        Assert.Equal(4, sentMessages.Count);
    }
}