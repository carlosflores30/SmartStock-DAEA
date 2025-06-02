using System;
using System.Collections.Generic;

namespace Smartstock.Infrastructure.PersistenceModels;

public partial class Stockmovement
{
    public Guid Id { get; set; }

    public Guid? Productid { get; set; }

    public int Quantity { get; set; }

    public string Movementtype { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Product? Product { get; set; }
}
