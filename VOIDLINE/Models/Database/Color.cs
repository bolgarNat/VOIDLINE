using System;
using System.Collections.Generic;

namespace VOIDLINE.Models.Database;

public partial class Color
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Productvariant> Productvariants { get; set; } = new List<Productvariant>();
}
