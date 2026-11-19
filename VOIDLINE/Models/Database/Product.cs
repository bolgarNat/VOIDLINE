using System;
using System.Collections.Generic;

namespace VOIDLINE.Models.Database;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Categoryid { get; set; }

    public Guid Genderid { get; set; }

    public Guid? Collectionid { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Collection? Collection { get; set; }

    public virtual Gender Gender { get; set; } = null!;

    public virtual ICollection<Productmaterial> Productmaterials { get; set; } = new List<Productmaterial>();

    public virtual ICollection<Productvariant> Productvariants { get; set; } = new List<Productvariant>();
}
