using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPriceDownloader.Models.FuelPriceResponse
{
    public class FuelPriceResponse
    {
        public Request Request { get; set; }
        public Series[] Series { get; set; }
    }
}
