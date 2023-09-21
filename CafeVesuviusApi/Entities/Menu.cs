using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Models;

public partial class Menu
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Season { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime Changed { get; set; }

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}
