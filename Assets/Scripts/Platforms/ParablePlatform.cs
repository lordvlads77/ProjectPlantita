using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platforms
{
    public class ParablePlatform : MonoBehaviour, IMovementPlatform
    {
        [FormerlySerializedAs("ParableOffset")] 
        public float parableOffset = 4;
        public float height = -5;
        public float speed = 1;
        private bool _moveToRight = true;
        private float _animation;
        private Vector2 _initPosition;

        private void Awake()
        {
            _initPosition = transform.position;
        }

        public void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            _animation += Time.deltaTime * speed;
            _animation %= 5f;

            Vector2 position;

            if (_moveToRight)
            {
                position = Parabola(
                    _initPosition, 
                    new(_initPosition.x + parableOffset, _initPosition.y), 
                    height, 
                    _animation / 5f);
            }
            else
            {
                position = Parabola(
                    new(_initPosition.x + parableOffset, _initPosition.y), 
                    _initPosition, 
                    height, 
                    _animation / 5f);
            }

            if (Math.Abs(position.y - _initPosition.y) < .4f)
            {
                _moveToRight = !_moveToRight;
            }
            else
            {
                transform.position = position;
            }
        }
        
        public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
        {
            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

            var mid = Vector2.Lerp(start, end, t);

            return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
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
    }
}