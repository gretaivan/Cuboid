using Cuboid.Business.Application.DTOs;
using Cuboid.Business.Application.Services;
using Cuboid.Business.Domain.Entities;
using Cuboid.Business.Infrastructure.Services;

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

        var sentMessages = priceService._notificationService.GetSentMessages();
        Assert.Equal(4, sentMessages.Count);
    }

    [Fact]
    public void Test2CancelPrice()
    {
        var priceService = new PriceService();

        var addPriceRequest = new AddPriceRequest
        {
            Value = 50,
            Broker = "Broker1",
            Submitter = "Trader3",
        };

        var addPriceRequest2 = new AddPriceRequest
        {
            Value = 60,
            Broker = "Broker2",
            Submitter = "Trader1",
        };

        priceService.Process(addPriceRequest, true);
        priceService.Process(addPriceRequest2, true);

        priceService.CancelPrice(0, "Trader3");

        var cancelledPrice = priceService._dataStore.GetPriceById(0);
        var activePrice = priceService._dataStore.GetPriceById(1);

        Assert.Equal(PriceStatus.Cancelled, cancelledPrice.Status);
        Assert.Equal(PriceStatus.Working, activePrice.Status);
    }
}