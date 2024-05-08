using UnityEngine;
using TMPro;
using Common.Managers;
using Common.DataPersistence;

namespace Common
{
    public class Timer : MonoBehaviour, IDataPersistence
    {
        public float MaxTimer = 15;
        public float timeRemaining = 60;
        [SerializeField] private TextMeshProUGUI timerText;

        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
        }

        private void Start()
        {
            timeRemaining = MaxTimer;
        }

        void Update()
        {
            if (_gameManager.GameState != GameState.Playing) return;
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timeRemaining = 0;
                _gameManager.UpdateGameState(GameState.Failed);
            }
        }

        void UpdateTimerUI()
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timerString;
        }

        public void LoadData(GameData data)
        {
            MaxTimer = data.MaxTimer;
        }

        public void SaveData(GameData data)
        {
            data.MaxTimer = MaxTimer;
        }
    }
}