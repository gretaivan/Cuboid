using Cuboid.Business.Application.DTOs;
using Cuboid.Business.Application.Interfaces;
using Cuboid.Business.Domain.Entities;
using Cuboid.Business.Infrastructure.Data;
using Cuboid.Business.Infrastructure.Services;

namespace Cuboid.Business.Application.Services;

public class PriceService
{
    public IDataStore _dataStore = new DataStore();
    public INotificationService _notificationService = new NotificationService();

    public void Process(AddPriceRequest addPriceRequest, bool isFromTrader)
    {
        ValidatePriceRequest(addPriceRequest, isFromTrader);

        var price = ToPrice(addPriceRequest, isFromTrader);
        _dataStore.Store(price);

        var defaultPriceMsg = new PriceMessage
        {
            Id = price.Id,
            Value = price.Value,
        };

        var users = _dataStore.GetUsers(price, isFromTrader);

        // below can definitely be reformatted to a shorter statement should use method CompletePriceMessage
        if (isFromTrader)
        {
            var priceMsg1 = new PriceMessage
            {
                Id = price.Id,
                Value = price.Value,
                Broker = price.Broker,
            };        

            // Assume x is flag that the user is a broker
            var x = true;
            foreach (var user in users)
            {
                if (x)
                {
                    _notificationService.Send(user, defaultPriceMsg);
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
            foreach (var user in users)
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
                    _notificationService.Send(user, defaultPriceMsg);
                }
            }
        }
    }

    public void CancelPrice(int priceId, string submitter)
    { 
        var price = _dataStore.GetPriceById(priceId);

        if (price == null) 
        {
            throw new ArgumentException("Price not found.");
        }

        if (price.Submitter != submitter)
        {
            throw new UnauthorizedAccessException("Only the price submitter is allowed to cancel");
        }

        price.Status = PriceStatus.Cancelled;
        _dataStore.Cancel(priceId, price);

        NotifyPriceCancellation(price); 
    }

    private void NotifyPriceCancellation(Price price)
    {
        var recipients = new List<string> { price.Broker };
        recipients.AddRange(_dataStore.GetAllTraders());

        var priceMsg = new PriceMessage { Id = price.Id };

        // Notify broker and all traders
        foreach (var recipient in recipients)
        {
            _notificationService.Send(recipient, priceMsg);
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

    /// <summary>
    /// Adds addition information to a default price message if required based on the sender and the recipient.
    /// </summary>
    /// <param name="price">Price Entity</param>
    /// <param name="user">The recipient of the message</param>
    /// <param name="isFromTrader">Indicates if price has been submitted by the trader</param>
    /// <param name="defaultMessage">Default message for the price</param>
    /// <returns>Price message with completed trader or broker fields where appropriate</returns>
    private PriceMessage CompletePriceMessage(Price price, string user, bool isFromTrader, PriceMessage defaultMessage)
    {
        return defaultMessage;
    }
}
