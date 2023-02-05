using System;
using CMF;
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

        private Transform _player;
        private CharacterKeyboardInput _characterKeyboardInput;
        private SidescrollerController _character;

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
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<CharacterLife>() == null && _player == null) return;
            
            AttachPlayer(other.transform);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if(_player != null && other.gameObject.GetComponent<CharacterLife>().GetInstanceID() != _player.GetInstanceID()) return;
            
            DetachPlayer();
        }

        public void AttachPlayer(Transform player)
        {
            player.transform.parent = transform;
            _player = player;

            _character = player.GetComponent<SidescrollerController>();
            _character.OnJump += _ => { DetachPlayer(); };
        }

        public void DetachPlayer()
        {
            if(_player == null) return;
            
            _player.transform.parent = null;
            _player = null;
            _character.OnJump -= _ => { DetachPlayer(); };
            _character = null;
        }
    }
}