using System;
using HealthSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platforms
{
    public class WaterSource : MonoBehaviour
    {
        [FormerlySerializedAs("HealingFactor")] 
        public int healingFactor;
        [FormerlySerializedAs("TotalHeal")] 
        public int totalHeal;
        [FormerlySerializedAs("HealingSpeed")] 
        public float healingSpeed;

        private HealingWater _healingWater;
        private int _drainPowerID;
        private Material _skyMaterial;
        private SpriteRenderer _sprite;

        private void Awake()
        {
            var healing = new LifePoint(healingFactor);
            var healPoints = new LifePoint(totalHeal);

            _skyMaterial = GetComponent<Renderer>().material;
            _sprite = GetComponent<SpriteRenderer>();
            _drainPowerID = Shader.PropertyToID("_DrainPower");
            _healingWater = new HealingWater(healing, healPoints, healingSpeed);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            var target = other.GetComponent<CharacterLife>();
            
            if(target == null || target.Health.IsCure()) return;
            
            _healingWater.Heal(target.Health, Time.time);
            var drainPower = (totalHeal - _healingWater.GetLeftHealPoints().Value);
            
            _skyMaterial.SetFloat(_drainPowerID, drainPower);

            if (_healingWater.GetLeftHealPoints().Value <= 0)
            {
                _sprite.color = Color.clear;
            }
        }
    }
}

