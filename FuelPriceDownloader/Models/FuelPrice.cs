using System;
using System.Collections.Generic;

namespace FuelPriceDownloader.Models;

public partial class FuelPrice
{
    public DateTime RecordDate { get; set; }

    public double Price { get; set; }
}
