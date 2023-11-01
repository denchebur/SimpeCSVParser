using System;
using System.Collections.Generic;

namespace Service.Database;

public class SampleCabDatum // this is model for client, who want to write a query to database
{
    public DateTime? TpepPickupDatetime { get; set; }

    public DateTime? TpepDropoffDatetime { get; set; }

    public int? PassengerCount { get; set; }

    public float? TripDistance { get; set; }

    public string? StoreAndFwdFlag { get; set; }

    public int? PUlocationId { get; set; }

    public int? DOlocationId { get; set; }

    public float? FareAmount { get; set; }

    public float? TipAmount { get; set; }
}
