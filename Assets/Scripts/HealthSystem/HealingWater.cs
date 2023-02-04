namespace HealthSystem
{
    public class HealingWater
    {
        private readonly float _healingSpeed;
        private float _nextHealing = 0;
        
        private readonly LifePoint _healingFactor;
        private readonly LifePoint _healPoints;

        public HealingWater(LifePoint healingFactor, LifePoint healPoints, float healingSpeed)
        {
            _healingFactor = healingFactor;
            _healPoints = healPoints;
            _healingSpeed = healingSpeed;
        }
        
        public void Heal(IHealth health, float currentTime)
        {
            if(!CanHeal(currentTime)) return;
            
            health.Heal(_healingFactor);
            
            _nextHealing = currentTime + _healingSpeed;
            _healPoints.ChangeValue(_healPoints.Value - _healingFactor.Value);
        }

        private bool CanHeal(float currentTime)
        {
            return _nextHealing <= currentTime && _healPoints.Value > 0;
        }

        public LifePoint GetLeftHealPoints()
        {
            return _healPoints;
        }
    }
}