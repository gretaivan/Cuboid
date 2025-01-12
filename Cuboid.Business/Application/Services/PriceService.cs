using Cuboid.Business.Application.DTOs;
using Cuboid.Business.Application.Interfaces;
using Cuboid.Business.Domain.Entities;
using Cuboid.Business.Infrastructure.Data;
using Cuboid.Business.Infrastructure.Services;

namespace Cuboid.Business.Application.Services;

public class PriceService
{
    private readonly IDataStore _dataStore = new DataStore();
    private readonly INotificationService _notificationService = new NotificationService();

    public void Process(AddPriceRequest addPriceRequest, bool isFromTrader)
    {
        ValidatePriceRequest(addPriceRequest, isFromTrader);

        var price = ToPrice(addPriceRequest, isFromTrader);
        _dataStore.Store(price);

        if (isFromTrader)
        {
            var priceMsg1 = new PriceMessage
            {
                Id = price.Id,
                Value = price.Value,
                Broker = price.Broker,
            };
            var priceMsg2 = new PriceMessage
            {
                Id = price.Id,
                Value = price.Value,
            };

            var users = _dataStore.GetUsers(price, isFromTrader);

            var x = true;
            foreach (var user in users)
            {
                if (x)
                {
                    _notificationService.Send(user, priceMsg2);
                    x = false;
                }
                else
                {
                    priceMsg1.Broker = price.Submitter == user ? price.Broker : default;
                    _notificationService.Send(user, priceMsg1);
                }
            }
        }
        else
        {
            foreach (var user in _dataStore.GetUsers(price, isFromTrader))
            {
                if (user == price.Submitter)
                {
                    var priceMsg = new PriceMessage
                    {
                        Id = price.Id,
                        Value = price.Value,
                        Trader = price.Trader,
                    };
                    _notificationService.Send(user, priceMsg);
                }
                else
                {
                    var priceMsg = new PriceMessage
                    {
                        Id = price.Id,
                        Value = price.Value,
                    };
                    _notificationService.Send(user, priceMsg);
                }
            }
        }
    }
    private void ValidatePriceRequest(AddPriceRequest addPriceRequest, bool isFromTrader)
    {
        if (isFromTrader)
        {
            if (string.IsNullOrEmpty(addPriceRequest.Broker)) throw new ArgumentNullException("Broker needs to be set");
        }
        else if (!string.IsNullOrEmpty(addPriceRequest.Broker))
        {
            throw new ArgumentException("Broker must not be set");
        }

        if (addPriceRequest.Value < 0) throw new ArgumentOutOfRangeException("Value must be > 0");
    }

    public Price ToPrice(AddPriceRequest addPriceRequest, bool isFromTrader)
    {
        return new Price
        {
            Id = new Guid(),
            Value = addPriceRequest.Value,
            Trader = isFromTrader ? "" : addPriceRequest.Trader,
            Broker = isFromTrader ? addPriceRequest.Broker : "",
            Status = PriceStatus.Working,
            Submitter = addPriceRequest.Submitter,
        };
    }
}
