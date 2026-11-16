using System;
using System.Collections.Generic;

namespace VOIDLINE.Models;

public partial class Categoryvariant
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
