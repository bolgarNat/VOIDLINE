using System;
using System.Collections.Generic;

namespace VOIDLINE.Models.Database;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] Password { get; set; } = null!;
}
