using System;
using System.Collections.Generic;
using VOIDLINE.Models.Database;

namespace VOIDLINE.Models;

public partial class Category
{
    public Guid Id { get; set; }

    public Guid? Categoryvariantid { get; set; }

    public string Name { get; set; } = null!;

    public virtual Categoryvariant? Categoryvariant { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
