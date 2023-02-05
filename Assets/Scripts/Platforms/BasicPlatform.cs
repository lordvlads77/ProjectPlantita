using System;
using CMF;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platforms
{
    public class BasicPlatform : MonoBehaviour, IMovementPlatform
    {
        [FormerlySerializedAs("Speed")] 
        public float speed = 2;
        [FormerlySerializedAs("StopTime")]
        public float stopTime = 1;
        [FormerlySerializedAs("LeftOffset")]
        public float leftOffset;
        [FormerlySerializedAs("RightOffset")]
        public float rightOffset;
        [FormerlySerializedAs("UpOffset")]
        public float upOffset;
        [FormerlySerializedAs("DownOffset")]
        public float downOffset;
        
        [FormerlySerializedAs("Direction")]
        public Vector2 direction = Vector2.right;
        
        private Vector2 _initialPosition;
        private float _maxLeftOffset;
        private float _maxRightOffset;
        private float _maxUpOffset;
        private float _maxDownOffset;
        private float _startMoving = 0;
        private Transform _player;
        private SidescrollerController _character;

        private void Awake()
        {
            _initialPosition = transform.position;

            _maxLeftOffset = _initialPosition.x - leftOffset;
            _maxRightOffset = _initialPosition.x + rightOffset;
            _maxUpOffset = _initialPosition.y + leftOffset;
            _maxDownOffset = _initialPosition.y - leftOffset;
        }

        public void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            var gameTime = Time.time;
            if(!CanMove(gameTime)) return;
            
            switch (direction)
            {
                case var v when v.Equals(Vector2.right): 
                    CheckRightOffsetPosition(gameTime);
                    break;
                case var v when v.Equals(Vector2.left):
                    CheckLeftOffsetPosition(gameTime);
                    break;
                case var v when v.Equals(Vector2.up):
                    CheckUpOffsetPosition(gameTime);
                    break;
                case var v when v.Equals(Vector2.down):
                    CheckDownOffsetPosition(gameTime);
                    break;
            }
            
            transform.Translate( direction * (speed * Time.deltaTime));
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

        private bool CanMove(float gameTime)
        {
            return gameTime >= _startMoving;
        }

        private void CheckRightOffsetPosition(float gameTime)
        {
            if (!(transform.position.x >= _maxRightOffset)) return;
            _startMoving = gameTime + stopTime;
            transform.position = new Vector2(_maxRightOffset, _initialPosition.y);
            direction = Vector2.left;
        }

        private void CheckLeftOffsetPosition(float gameTime)
        {
            if (!(transform.position.x <= _maxLeftOffset)) return;
            
            _startMoving = gameTime + stopTime;
            transform.position = new Vector2(_maxLeftOffset, _initialPosition.y);
            direction = Vector2.right;
        }

        private void CheckUpOffsetPosition(float gameTime)
        {
            if (!(transform.position.y >= _maxUpOffset)) return;
            
            _startMoving = gameTime + stopTime;
            transform.position = new Vector2(_initialPosition.x, _maxUpOffset);
            direction = Vector2.down;
        }
        
        private void CheckDownOffsetPosition(float gameTime)
        {
            if (!(transform.position.y <= _maxDownOffset)) return;
            
            _startMoving = gameTime + stopTime;
            transform.position = new Vector2(_initialPosition.x, _maxDownOffset);
            direction = Vector2.up;
        }
    }
}