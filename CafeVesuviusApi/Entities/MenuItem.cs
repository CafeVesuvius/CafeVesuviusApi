using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Entities;

public partial class MenuItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public bool IsActive { get; set; }

    public string? ImagePath { get; set; }

    public int MenuId { get; set; }
}
