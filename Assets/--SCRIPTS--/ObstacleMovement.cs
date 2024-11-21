using UnityEngine;


namespace Obstacles
{
    public class ObstacleMovement : MonoBehaviour
    {
        public float moveDistance = 5f;
        public float moveSpeed = 2f;
        public float smoothTime = 0.3f;

        private Vector3 startPosition;
        private Vector3 targetPosition;
        private Vector3 velocity = Vector3.zero;

        private bool movingRight = true;

        void Start()
        {
            startPosition = transform.position;
            targetPosition = startPosition + new Vector3(moveDistance, 0f, 0f);// Initial target position
        }

        void Update()
        {
            SmoothMoveObstacle();
        }

        private void SmoothMoveObstacle()
        {
            //Smoothly move to the target position using SmoothDamp for a smooth, decelerating movement
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            //Check if the obstacle has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f) // Tolerance to check if it has arrived
            {
                // Reverse direction
                if (movingRight)
                {
                    targetPosition = startPosition - new Vector3(moveDistance, 0f, 0f); // Move to the left
                }
                else
                {
                    targetPosition = startPosition + new Vector3(moveDistance, 0f, 0f); // Move to the right
                }

                // Toggle movement direction
                movingRight = !movingRight;
            }
        }
    }
}

