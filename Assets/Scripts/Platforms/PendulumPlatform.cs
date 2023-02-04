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