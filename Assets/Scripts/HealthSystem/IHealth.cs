public interface IHealth
{
    public LifePoint Reduce(LifePoint damage);

    public LifePoint Heal(LifePoint heal);
}

