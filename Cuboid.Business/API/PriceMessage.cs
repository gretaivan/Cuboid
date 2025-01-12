using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuboid.Business.API;
public class PriceMessage
{
    public Guid Id { get; internal set; }
    public int Value { get; internal set; }
    public string? Broker { get; internal set; }
    public string Trader { get; internal set; }
}
