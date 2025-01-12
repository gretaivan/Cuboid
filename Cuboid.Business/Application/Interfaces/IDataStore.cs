using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuboid.Business.Domain.Entities;

namespace Cuboid.Business.Application.Interfaces
{
    public interface IDataStore
    {
        IEnumerable<string> GetUsers(Price price, bool isFromTrader);
        void Store(Price price);
        Price GetPriceById(int priceId);
        void Cancel(int priceId, Price price);
        IEnumerable<string> GetAllTraders();
    }
}
