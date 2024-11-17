using UnityEngine;

namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform player;

        [SerializeField]
        private Vector3 offset;

        void LateUpdate()
        {
            Vector3 targetPosition = new Vector3(transform.position.x, player.position.y + offset.y, player.position.z + offset.z);
            transform.position = targetPosition;
        }
    }
}
