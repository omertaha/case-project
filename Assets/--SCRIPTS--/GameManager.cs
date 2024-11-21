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
        public static Action TriggerPaintingMode;
        public static Action TriggerGameEnd;

        public static Action CoinCollected;

        public static Action<int> Failed;
        //Private variables
        private static int _fails = 0;
        public int CollectedCoins { get; private set; } = 0;

        [SerializeField]
        private List<ManagerBase> _managers = new List<ManagerBase>();
        private int _readyCount = 0;

        [SerializeField]
        private GameObject _player;   

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

            }
            else if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        #endregion

        private void Start()
        {
            Failed?.Invoke(_fails);//To refresh fail int in the beginning.
        }

        //I wait until necessary managers say they are ready. Then I can start the game.
        private void OnEnable()
        {
            foreach (var manager in _managers)
            {
                manager.Ready += OnAllManagersReady;
            }

            CoinCollected += IncrementCoin;
            RestartLevel += RestartMyLevel;
            TriggerGameEnd += GameEnd;
        }

        private void OnDisable()
        {
            foreach (var manager in _managers)
            {
                manager.Ready -= OnAllManagersReady;
            }

            CoinCollected -= IncrementCoin;
            RestartLevel -= RestartMyLevel;
            TriggerGameEnd -= GameEnd;
        }

        private void OnAllManagersReady()
        {
            _readyCount++;
            if(_readyCount >= _managers.Count)
            {
                AllManagersReady?.Invoke();
                EnablePlayer();
            }
        }

        private void EnablePlayer()
        {
            _player.SetActive(true);
        }

        private void IncrementCoin()
        {
            CollectedCoins++;
        }


        private void RestartMyLevel()
        {
            _fails++;
            Failed?.Invoke(_fails);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void GameEnd()
        {
            //Do Nothing.
        }
    }
}

