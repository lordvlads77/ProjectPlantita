public record LifePoint
{
    public int Value { get; private set; }

    public LifePoint(int value)
    {
        ChangeValue(value);
    }

    public LifePoint(LifePoint initialLife)
    {

        Value = initialLife.Value;
    }

    public void ChangeValue(int value)
    {
        Value = value < 0 ? 0 : value;
    }
}