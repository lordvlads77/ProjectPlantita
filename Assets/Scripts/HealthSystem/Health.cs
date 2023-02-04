namespace HealthSystem
{
    public class Health : IHealth
    {
        private readonly IHealthEvents _healthEvents;

        public Health(LifePoint initialLife, IHealthEvents healthEvents)
        {
            _healthEvents = healthEvents;
            InitialLife = initialLife;
            CurrentLife = InitialLife;
        }

        public LifePoint InitialLife { get; }
        
        public LifePoint CurrentLife { get; private set; }
        
        public LifePoint Reduce(LifePoint damage)
        {
            CurrentLife = new LifePoint(CurrentLife.Value - damage.Value);
            
            DeathEvent();
            
            return CurrentLife;
        }

        private void DeathEvent()
        {
            if(CurrentLife.Value > 0) return;
            
            _healthEvents.Death();
        }

        public LifePoint Heal(LifePoint heal)
        {
            CurrentLife = new LifePoint(CurrentLife.Value + heal.Value);;

            return CurrentLife;
        }
    }
}