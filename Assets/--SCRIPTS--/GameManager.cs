using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        //Public variables
        public static Action RestartLevelOnDeath;
        public static Action RestartLevel;

        public static Action AllManagersReady;


        //Private variables
        [SerializeField]
        private List<ManagerBase> _managers = new List<ManagerBase>();
        private int readyCount = 0;

        [SerializeField]
        private GameObject player;

        

        #region Turn into Singleton
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        Debug.LogError("GameManager instance is required in the scene but not present.");
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject); // Keep the GameManager across scenes
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        #endregion


        private void OnEnable()
        {
            foreach (var manager in _managers)
            {
                manager.Ready += OnAllManagersReady;
            }

            RestartLevel += RestartMyLevel;
        }

        private void OnDisable()
        {
            foreach (var manager in _managers)
            {
                manager.Ready -= OnAllManagersReady;
            }

            RestartLevel -= RestartMyLevel;
        }

        private void OnAllManagersReady()
        {
            readyCount++;
            if(readyCount >= _managers.Count)
            {
                AllManagersReady?.Invoke();//Can be used in the future.
                EnablePlayer();
            }
        }

        private void EnablePlayer()
        {
            player.SetActive(true);
        }

        private void Death()
        {

        }

        private void RestartMyLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

