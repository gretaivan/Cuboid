using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuboid.Business.Application.DTOs;

namespace Cuboid.Business.Application.Interfaces
{
    internal interface INotificationService
    { 
        void Send(string user, PriceMessage priceMsg);
    }
}
