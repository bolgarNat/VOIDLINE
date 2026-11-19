using System.ComponentModel;

namespace VOIDLINE.Enums;

enum ValidSeasonEnum
{
    Winter,

    [Description("Winter/Spring")]
    Winter_Spring, 
    Spring,

    [Description("Spring/Summer")]
    Spring_Summer, 
    Summer,

    [Description("Summer/Autumn")]
    Summer_Autumn, 
    Atumn,

    [Description("Autumn/Winter")]
    Autumn_Winter
}
