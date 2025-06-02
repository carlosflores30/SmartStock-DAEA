using System;
using System.Collections.Generic;

namespace Smartstock.Infrastructure.PersistenceModels;

public partial class Sale
{
    public Guid Id { get; set; }

    public Guid? Userid { get; set; }

    public decimal Total { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Saledetail> Saledetails { get; set; } = new List<Saledetail>();

    public virtual User? User { get; set; }
}
