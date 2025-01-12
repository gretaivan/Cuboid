using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuboid.Business.Application.DTOs;
public class AddPriceRequest
{
    public int Value { get; set; }
    public string Trader { get; set; }
    public string Broker { get; set; }
    public string Submitter { get; set; }
}
