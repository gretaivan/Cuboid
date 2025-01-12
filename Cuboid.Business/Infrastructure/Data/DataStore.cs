using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

    private readonly Dictionary<int, Price> _prices = new();
    private int _priceId = 0;

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
        _prices[_priceId] = price;
        _priceId++; 
    }

    public Price GetPriceById(int priceId)
    {
        _prices.TryGetValue(priceId, out var price);
        return price; 
    }

    public void Cancel(int priceId, Price price)
    {
        if(_prices.ContainsKey(priceId))
        {
            _prices[priceId] = price;             
        }
        else
        {
            throw new KeyNotFoundException("Price not found");            
        }
    }

    /// <summary>
    /// Gets all traders
    /// </summary>
    /// <returns>An enumerable of traders</returns>
    public IEnumerable<string> GetAllTraders()
    {
        // mimics a database query
        return new List<string> { "Trader1", "Trader2", "Trader3" };
    }     
}
