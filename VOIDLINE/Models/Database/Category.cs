using System;
using System.Collections.Generic;

namespace VOIDLINE.Models.Database;

public partial class Category
{
    public Guid Id { get; set; }

    public Guid? Categoryvariantid { get; set; }

    public string Name { get; set; } = null!;

    public virtual Categoryvariant? Categoryvariant { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
