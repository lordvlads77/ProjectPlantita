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
        
        

        private void Awake()
        {
            _boxCollider = gameObject.GetComponent<BoxCollider2D>();
            _boxCollider.enabled = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var life = collision.gameObject.GetComponent<CharacterLife>();
            
            if(life == null) return;

            life.Health.Damage(new LifePoint(damage));
        }
    }
}