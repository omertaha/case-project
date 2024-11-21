using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Managers
{
    public class CameraManager : Manager<CameraManager>
    {
        [SerializeField]
        private CinemachineVirtualCamera mainCamera;

        [SerializeField]
        private CinemachineVirtualCamera paintCamera;


        private void OnEnable()
        {
            GameManager.TriggerPaintingMode += SwitchCamera;
        }

        private void OnDisable()
        {
            GameManager.TriggerPaintingMode -= SwitchCamera;
        }

        public void SwitchCamera()
        {
            mainCamera.Priority = 10;
            paintCamera.Priority = 20;
        }
    }
}

