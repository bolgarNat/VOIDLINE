using System;
using System.Collections.Generic;

namespace VOIDLINE.Models.Database;

public partial class Size
{
    public Guid Id { get; set; }

    public virtual ICollection<Productvariant> Productvariants { get; set; } = new List<Productvariant>();
}
