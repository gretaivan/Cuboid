using Cuboid.Business.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuboid.Business;
public class DownstreamService
{
    public List<(string Recipient, PriceMessage Msg)> SentMessages { get; set; } = [];

    internal void Send(string user, PriceMessage priceMsg)
    {
        SentMessages.Add((user, priceMsg));
    }
}
