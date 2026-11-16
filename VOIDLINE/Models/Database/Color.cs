using System;
using System.Collections.Generic;
using VOIDLINE.Models.Database;

namespace VOIDLINE.Models;

public partial class Color
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Productvariant> Productvariants { get; set; } = new List<Productvariant>();
}
