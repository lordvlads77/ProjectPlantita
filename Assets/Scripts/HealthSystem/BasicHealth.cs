namespace HealthSystem
{
    public class BasicHealth : IHealth
    {
        private readonly IHealthEvents _healthEvents;

        public BasicHealth(LifePoint initialLife, LifePoint maxLife, IHealthEvents healthEvents)
        {
            _healthEvents = healthEvents;
            InitialLife = initialLife;
            MaxLife = maxLife;
            CurrentLife = InitialLife;
        }

        public LifePoint InitialLife { get; }

        public LifePoint MaxLife { get; }

        public LifePoint CurrentLife { get; private set; }

        public LifePoint Damage(LifePoint damage)
        {
            CurrentLife.ChangeValue(CurrentLife.Value - damage.Value);

            HurtEvent();
            DeathEvent();

            return CurrentLife;
        }

        private void HurtEvent()
        {
            _healthEvents.Hurt();
        }

        private void DeathEvent()
        {
            if (CurrentLife.Value > 0) return;

            _healthEvents.Death();
        }

        public LifePoint Heal(LifePoint heal)
        {
            CurrentLife.ChangeValue(CurrentLife.Value + heal.Value);

            if (CurrentLife.Value >= MaxLife.Value) CurrentLife.ChangeValue(MaxLife.Value);

            return CurrentLife;
        }

        public LifePoint GetCurrentLife()
        {
            return CurrentLife;
        }
    }
}