using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;


namespace Characters
{
    internal class PlayerControllerInputLayer
    {
        private Transform _transform;
        private float _startingTouchX;
        private bool _isTouching = false;

        private float _minX;
        private float _maxX;

        internal PlayerControllerInputLayer(Transform transfom)
        {
            _transform = transfom;

            float _platformWidth = PlatformManager.Instance.platformWidth;

            _minX = -_platformWidth / 2;
            _maxX = _platformWidth / 2;
        }

        public void Tick()
        {
#if UNITY_ANDROID || UNITY_IOS
            TouchInput();
#else
            MouseInput();
#endif
        }

        private void TouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _startingTouchX = touch.position.x;
                    _isTouching = true;
                }
                else if (_isTouching && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
                {
                    // Calculate the difference from the initial touch position
                    float deltaX = touch.position.x - _startingTouchX;

                    // Move the player based on the difference
                    MovePlayer(deltaX);

                    // Update the initial position for smoother tracking
                    _startingTouchX = touch.position.x;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    // Reset touch tracking
                    _isTouching = false;
                }
            }
        }

        private void MouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startingTouchX = Input.mousePosition.x;
                _isTouching = true;
            }
            else if (_isTouching && Input.GetMouseButton(0))
            {
                float deltaX = Input.mousePosition.x - _startingTouchX;
                MovePlayer(deltaX);
                _startingTouchX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
            }
        }

        private void MovePlayer(float deltaX)
        {
             
            Vector3 moveDirection = Vector3.right * deltaX * 0.25f * Time.deltaTime;
            Vector3 newPosition = _transform.position + moveDirection;

            // Clamp the x position between minX and maxX
            newPosition.x = Mathf.Clamp(newPosition.x, _minX + 0.05f , _maxX - 0.05f);

            _transform.position = newPosition;
        }
    }
}
