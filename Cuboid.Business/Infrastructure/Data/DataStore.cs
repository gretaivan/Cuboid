using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuboid.Business.Application.Interfaces;
using Cuboid.Business.Domain.Entities;

namespace Cuboid.Business.Infrastructure.Data;

/// <summary>
/// Provides storage and management for Price Entities
/// </summary>
public class DataStore : IDataStore
{

    /// <summary>
    /// Retrieves traders and a broker associated with the price
    /// </summary>
    /// <param name="price"></param>
    /// <param name="isFromTrader"></param>
    /// <returns>An enumerable of users associated to the price</returns>
    public IEnumerable<string> GetUsers(Price price, bool isFromTrader) 
    {
        if (isFromTrader)
        {
            yield return price.Broker;                   
        }
        else
        {
            yield return price.Submitter;            
        }

        // Return all traders
        foreach (var trader in GetAllTraders())
        {
            yield return trader;
        }
    }

    public void Store(Price price)
    {
        //No storage is done currently. Please only add storage (in memory) if it helps
        //with your solution
    }

    public void Cancel(Price price)
    {
    }

    /// <summary>
    /// Gets all traders
    /// </summary>
    /// <returns>An enumerable of traders</returns>
    private IEnumerable<string> GetAllTraders()
    {
        // mimics a database query
        return new List<string> { "Trader1", "Trader2", "Trader3" };
    }
     
}
