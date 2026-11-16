using System;
using System.Collections.Generic;

namespace VOIDLINE.Models.Database;

public partial class Productmaterial
{
    public Guid Productid { get; set; }

    public Guid Materialid { get; set; }

    public decimal Percentage { get; set; }

    public virtual Material Material { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
