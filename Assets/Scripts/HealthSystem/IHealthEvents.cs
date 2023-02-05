namespace HealthSystem
{
    public interface IHealthEvents
    {
        public void Death();

        public void Hurt();

        public void Healing();
    }
}