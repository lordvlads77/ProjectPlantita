public interface IHealth
{
    public LifePoint Damage(LifePoint damage);

    public LifePoint Heal(LifePoint heal);

    public LifePoint GetCurrentLife();
}

