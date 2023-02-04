using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platforms
{
    public class BasicPlatform : MonoBehaviour, IMovementPlatform
    {
        [FormerlySerializedAs("Speed")] 
        public float speed = 2;
        [FormerlySerializedAs("Offset")]
        public Vector2 offset;
        [FormerlySerializedAs("Direction")]
        public Vector2 direction = Vector2.right;
        
        private Vector2 _initialPosition;
        private Vector2 _maxOffset;

        private void Awake()
        {
            _initialPosition = transform.position;
            //_maxOffset.x = _initialPosition.x + _maxOffset;
        }

        public void Move()
        {
            if (direction == Vector2.right)
            {
                MoveToRight();
            }
        }

        private void MoveToRight()
        {
            //if(transform.position.x > _initialPosition )
        }
    }
}