namespace VOIDLINE.Models;

public class ProductAmountModel
{
    public int _amount = 0;

    void Increment()
    {
        _amount++;
    }

    void Decrement()
    {
        if (_amount > 0)
        {
            _amount--;
        }
    }
}
