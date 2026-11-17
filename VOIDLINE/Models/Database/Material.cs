using System;
using System.Collections.Generic;

namespace VOIDLINE.Models.Database;

public partial class Material
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Productmaterial> Productmaterials { get; set; } = new List<Productmaterial>();
}
