using Cuboid.Business.Application.DTOs;
using Cuboid.Business.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuboid.Business.Infrastructure.Services;
public class NotificationService : INotificationService
{
    public List<(string Recipient, PriceMessage Msg)> SentMessages { get; private set; } = [];

    public void Send(string user, PriceMessage priceMsg)
    {
        SentMessages.Add((user, priceMsg));
    }
    public List<(string Recipient, PriceMessage Msg)> GetSentMessages()
    {
        return SentMessages;
    }
}
