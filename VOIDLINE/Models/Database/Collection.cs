using System;
using System.Collections.Generic;

namespace VOIDLINE.Models.Database;

public partial class Collection
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Partnername { get; set; }

    public DateOnly Releasedate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
