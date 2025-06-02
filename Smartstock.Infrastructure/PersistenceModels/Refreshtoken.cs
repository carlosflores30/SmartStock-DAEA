﻿using System;
using System.Collections.Generic;

namespace Smartstock.Infrastructure.PersistenceModels;

public partial class Refreshtoken
{
    public Guid Id { get; set; }

    public Guid? Userid { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expiresat { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Revokedat { get; set; }

    public virtual User? User { get; set; }
}
