using System;
using System.Collections.Generic;

namespace VOIDLINE.Models.Database;

public partial class Productvariant
{
    public Guid Id { get; set; }

    public Guid Productid { get; set; }

    public Guid Sizeid { get; set; }

    public Guid Colorid { get; set; }

    public int Stock { get; set; }

    public decimal Sale { get; set; }

    public string? UrlAdress { get; set; }

    public virtual Color Color { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Size Size { get; set; } = null!;
}
