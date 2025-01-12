using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuboid.Business.Domain.Entities;

namespace Cuboid.Business.Application.Interfaces
{
    internal interface IDataStore
    {
        IEnumerable<string> GetUsers(Price price, bool isFromTrader);
        void Store(Price price);
        void Cancel(Price price);
    }
}
