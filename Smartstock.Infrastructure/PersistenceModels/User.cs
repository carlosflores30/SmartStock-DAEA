using System;
using System.Collections.Generic;

namespace Smartstock.Infrastructure.PersistenceModels;

public partial class User
{
    public Guid Id { get; set; }

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool? Isactive { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<Refreshtoken> Refreshtokens { get; set; } = new List<Refreshtoken>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
