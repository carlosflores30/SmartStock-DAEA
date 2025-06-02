using System;
using System.Collections.Generic;

namespace Smartstock.Infrastructure.PersistenceModels;

public partial class Saledetail
{
    public Guid Id { get; set; }

    public Guid? Saleid { get; set; }

    public Guid? Productid { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Sale? Sale { get; set; }
}
