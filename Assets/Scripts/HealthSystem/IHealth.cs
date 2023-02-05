public interface IHealth
{
    public void Damage(LifePoint damage);

    public void Heal(LifePoint heal);

    public LifePoint GetCurrentLife();

    public bool IsCure();
}

