using System;
using System.Collections.Generic;

namespace Smartstock.Infrastructure.PersistenceModels;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int Threshold { get; set; }

    public Guid? Categoryid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Saledetail> Saledetails { get; set; } = new List<Saledetail>();

    public virtual ICollection<Stockmovement> Stockmovements { get; set; } = new List<Stockmovement>();

    public virtual ICollection<Stockreport> Stockreports { get; set; } = new List<Stockreport>();
}
