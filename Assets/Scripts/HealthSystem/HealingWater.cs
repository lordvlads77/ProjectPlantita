namespace HealthSystem
{
    public class HealingWater
    {
        private readonly float _healingSpeed;
        private float _nextHealing = 0;
        
        private readonly LifePoint _healingFactor;
        private readonly LifePoint _totalHeal;

        public HealingWater(LifePoint healingFactor, LifePoint totalHeal, float healingSpeed)
        {
            _healingFactor = healingFactor;
            _totalHeal = totalHeal;
            _healingSpeed = healingSpeed;
        }
        
        public void Heal(IHealth health, float currentTime)
        {
            if(CanHeal(currentTime)) return;
            
            health.Heal(_healingFactor);
            
            _nextHealing = currentTime + _healingSpeed;
            _totalHeal.ChangeValue(_totalHeal.Value - _healingFactor.Value);
        }

        private bool CanHeal(float currentTime)
        {
            return _nextHealing > currentTime || _totalHeal.Value <= 0;
        }
    }
}