using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Platforms
{
    public class WaterPlatform : MonoBehaviour
    {
        public LifePoint HealingFactor { get; set; }
        public float HealingSpeed { get; set; }

        private IHealth _target;
        private float _nextHealing = 0;

        public void OnTriggerEnter2D(Collider2D col)
        {
            _nextHealing = Time.deltaTime + HealingSpeed;
            _target = col.GetComponent<IHealth>();
        }

        public void OnTriggerStay(Collider other)
        {
            if(Time.deltaTime < _nextHealing) return;

            _target.Heal(HealingFactor);
        }
    }
}

