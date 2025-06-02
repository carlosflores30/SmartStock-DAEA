using System;
using System.Collections.Generic;

namespace Smartstock.Infrastructure.PersistenceModels;

public partial class Stockreport
{
    public Guid Id { get; set; }

    public Guid? Productid { get; set; }

    public DateOnly Reportdate { get; set; }

    public int? Totalentrada { get; set; }

    public int? Totalsalida { get; set; }

    public int? Stockfinal { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Product? Product { get; set; }
}
