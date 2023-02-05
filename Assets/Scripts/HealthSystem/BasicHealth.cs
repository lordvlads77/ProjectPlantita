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

        public LifePoint CurrentLife { get; }

        public void Damage(LifePoint damage)
        {
            CurrentLife.ChangeValue(CurrentLife.Value - damage.Value);

            HurtEvent();
            DeathEvent();
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

        public void Heal(LifePoint heal)
        {
            CurrentLife.ChangeValue(CurrentLife.Value + heal.Value);

            if (CurrentLife.Value >= MaxLife.Value) CurrentLife.ChangeValue(MaxLife.Value);

            HealEvent();
        }

        private void HealEvent()
        {
            _healthEvents.Healing();
        }

        public LifePoint GetCurrentLife()
        {
            return CurrentLife;
        }

        public bool IsCure()
        {
            return CurrentLife == MaxLife;
        }
    }
}