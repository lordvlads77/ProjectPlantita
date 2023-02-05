using UnityEngine;

namespace Platforms
{
    public interface IMovementPlatform
    {
        public void Move();

        public void AttachPlayer(Transform player);

        public void DetachPlayer(Transform player);
    }
}