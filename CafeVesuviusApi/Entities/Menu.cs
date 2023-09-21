using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Entities;

public partial class Menu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Season { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime ChangedDate { get; set; }

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}
