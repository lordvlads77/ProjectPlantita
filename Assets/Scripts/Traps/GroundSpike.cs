using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Traps
{
    public class GroundSpike : MonoBehaviour
    {
        [FormerlySerializedAs("Damage")] 
        public int damage = 1;
        
        [FormerlySerializedAs("HiddenTime")]
        public float hiddenTime = 2;
        
        [FormerlySerializedAs("ActiveTime")] 
        public float activeTime = 2;

        private BoxCollider2D _boxCollider;
        private float _nextActive;
        private float _nextHidden;

        private void Awake()
        {
            _boxCollider = gameObject.GetComponent<BoxCollider2D>();
            _boxCollider.enabled = false;
            _nextActive = Time.time + hiddenTime;
            _nextHidden = 0;
        }

        public void FixedUpdate()
        {
            if (_nextActive < Time.time && !_boxCollider.enabled)
            {
                _boxCollider.enabled = true;
                _nextHidden = Time.time + activeTime;
            }
            else if(_nextHidden < Time.time && _boxCollider.enabled)
            {
                _boxCollider.enabled = false;
                _nextActive = Time.time + hiddenTime;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var life = other.GetComponent<CharacterLife>();
            
            if(life == null) return;

            life.Health.Damage(new LifePoint(damage));
        }
    }
}