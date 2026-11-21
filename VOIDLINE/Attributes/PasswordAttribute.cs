using System.ComponentModel.DataAnnotations;

namespace VOIDLINE.Attributes;

public sealed class ValidPasswordAttribute : DataTypeAttribute
{

    public ValidPasswordAttribute() : base(DataType.Password) { }

    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }

        if (!(value is string text))
        {
            return false;
        }

        var strValue = value.ToString()!;

        if (strValue.Length < 8)
        {
            return false;
        }

        if (!strValue.Any(char.IsDigit))
        {
            return false;
        }

        return true;
    }
}
