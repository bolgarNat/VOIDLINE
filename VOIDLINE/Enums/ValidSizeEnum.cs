using System.ComponentModel;

namespace VOIDLINE.Enums;

enum ValidSizeEnum
{
    XXS,
    XS,
    S,

    [Description("S-M")]
    S_M, 
    M,

    [Description("M-L")]
    M_L, 
    L,

    [Description("L-XL")]
    L_XL, 
    XL, 
    XXL, 
    XXXL
}
