using UnityEngine;
using System;
using TMPro;
using Characters;
using UnityEngine.UI;


namespace Managers
{
    public class CanvasManager : Manager<CanvasManager>
    {
        [Header("Panels")]
        [SerializeField]
        private GameObject _mainPanel;
        [SerializeField]
        private GameObject _paintPanel;
        [SerializeField]
        private GameObject _winScreenPanel;

        [Header("Coin")]
        [SerializeField]
        private TextMeshProUGUI _coinText;
        public Transform CoinTransform;

        [Header("Rank")]
        [SerializeField] 
        private TextMeshProUGUI _rankText;

        [SerializeField] 
        private Character _player;

        [Header("Fail")]
        [SerializeField]
        private TextMeshProUGUI failsText;

        [Header("Painting")]
        [SerializeField]
        private Slider _brushSizeSlider;

        private void Start()
        {
            Ready?.Invoke();
        }

        private void OnEnable()
        {
            GameManager.TriggerPaintingMode += ActivatePaintingPanel;
            GameManager.TriggerGameEnd += ActivateWinScreenPanel;
            GameManager.CoinCollected += RefreshCoinText;
            GameManager.Failed += RefreshFailsText;
            _brushSizeSlider.onValueChanged.AddListener((value) => SetBrushSize(value));
        }

        private void OnDisable()
        {
            GameManager.TriggerGameEnd -= ActivateWinScreenPanel;
            GameManager.TriggerPaintingMode -= ActivatePaintingPanel;
            GameManager.CoinCollected -= RefreshCoinText;
            GameManager.Failed -= RefreshFailsText;
        }

        private void Update()
        {
            ShowRanking();
        }

        private void ShowRanking()
        {
            if (RankingManager.Instance != null && _player != null)
            {
                int rank = RankingManager.Instance.GetPlayerRank(_player);
                _rankText.text = "Rank: " + rank.ToString();
            }
        }

        private void SetBrushSize(float value)
        {
            WallPainterManager.Instance.AdjustBrushSize(value);
        }

        private void RefreshCoinText()
        {
            _coinText.text = GameManager.Instance.CollectedCoins.ToString();
        }

        private void RefreshFailsText(int value)
        {
            failsText.text = "Fails: " + value.ToString();
        }

        private void ActivatePaintingPanel()
        {
            _mainPanel.SetActive(false);
            _paintPanel.SetActive(true);
        }

        private void ActivateWinScreenPanel()
        {
            _mainPanel.SetActive(false);
            _paintPanel.SetActive(false);
            _winScreenPanel.SetActive(true);
        }
    }
}

