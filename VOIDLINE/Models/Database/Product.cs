using System;
using System.Collections.Generic;

namespace VOIDLINE.Models.Database;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Categoryid { get; set; }

    public string? Description { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Productmaterial> Productmaterials { get; set; } = new List<Productmaterial>();

    public virtual ICollection<Productvariant> Productvariants { get; set; } = new List<Productvariant>();
}
