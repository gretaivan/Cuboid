using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuboid.Business.Application.DTOs;

namespace Cuboid.Business.Application.Interfaces
{
    public interface INotificationService
    {       
        void Send(string user, PriceMessage priceMsg);
        List<(string Recipient, PriceMessage Msg)> GetSentMessages();
    }
}
