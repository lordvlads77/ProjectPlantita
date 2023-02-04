using HealthSystem;
using UnityEngine;

namespace Platforms
{
    public class WaterPlatform : MonoBehaviour
    {
        public int HealingFactor { get; set; }
        public int TotalHeal { get; set; }
        public float HealingSpeed { get; set; }

        private HealingWater _healingWater;

        private void Awake()
        {
            var healingFactor = new LifePoint(HealingFactor);
            var totalHeal = new LifePoint(TotalHeal);
            
            _healingWater = new HealingWater(healingFactor, totalHeal, HealingSpeed);
        }

        public void OnTriggerStay2D(Collider2D col)
        {
            var target = col.GetComponent<CharacterLife>();
            _healingWater.Heal(target.Health, Time.deltaTime);
        }
    }
}

