public record LifePoint
{
    public int Value { get; }

    public LifePoint(int initialLife)
    {
        Value = initialLife < 0 ? 0 : initialLife;
    }
}