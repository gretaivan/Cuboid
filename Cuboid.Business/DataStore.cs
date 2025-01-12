using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuboid.Business;
public class DataStore
{
    internal IEnumerable<string> GetUsers(Price price, bool isFromTrader)
    {
        if (isFromTrader)
        {
            yield return price.Broker;
        }

        if (isFromTrader)
        {
            //Return all traders
            yield return "Trader1";
            yield return "Trader2";
            yield return "Trader3";
        }
        else
        {
            yield return price.Submitter;
            yield return "Trader1";
            yield return "Trader2";
            yield return "Trader3";
        }
    }

    internal void Store(Price price)
    {
        //No storage is done currently. Please only add storage (in memory) if it helps
        //with your solution
    }

    internal void Cancel(Price price)
    {
    }
}
