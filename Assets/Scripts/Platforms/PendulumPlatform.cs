using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platforms
{
    public class PendulumPlatform : MonoBehaviour, IMovementPlatform
    {
        private Rigidbody2D _rigidbody2D;
        [FormerlySerializedAs("RightPushRange")] 
        public float rightPushRange;
        [FormerlySerializedAs("VelocityThreshold")] 
        public float velocityThreshold;
        [FormerlySerializedAs("LeftPushRange")] 
        public float leftPushRange;

        private void Awake()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _rigidbody2D.angularVelocity = velocityThreshold;
        }

        private void FixedUpdate()
        {
            Move();
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.GetComponent<CharacterLife>() == null) return;
            
            AttachPlayer(other.transform);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if(other.gameObject.GetComponent<CharacterLife>() == null) return;
            
            DetachPlayer(other.transform);
        }

        public void AttachPlayer(Transform player)
        {
            player.parent = transform;
        }

        public void DetachPlayer(Transform player)
        {
            player.parent = null;
        }

        public void Move()
        {
            if (transform.rotation.z > 0
                && transform.rotation.z < rightPushRange
                && (_rigidbody2D.angularVelocity > 0)
                && _rigidbody2D.angularVelocity < velocityThreshold)
            {
                _rigidbody2D.angularVelocity = velocityThreshold;
            }
            else if (transform.rotation.z < 0
                     && transform.rotation.z > leftPushRange
                     && (_rigidbody2D.angularVelocity > 0)
                     && _rigidbody2D.angularVelocity < velocityThreshold * -1)
            {
                _rigidbody2D.angularVelocity = velocityThreshold * -1;
            }
        }
    }
}