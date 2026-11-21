using System.ComponentModel.DataAnnotations;

namespace VOIDLINE.Attributes;

public class NameAttribute : DataTypeAttribute
{
    public NameAttribute() : base(DataType.Text) { }

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

        if (strValue.Any(char.IsDigit))
        {
            return false;
        }

        return true;
    }
}
