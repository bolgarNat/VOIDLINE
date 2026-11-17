using System;
using System.Collections.Generic;

namespace VOIDLINE.Models.Database;

public partial class Gender
{
    public Guid Id { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
