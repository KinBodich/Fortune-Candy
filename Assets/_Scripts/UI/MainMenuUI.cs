using Common.DataPersistence;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private SettingsUI _settingsUI;

        private int _currentLevel;

        private void Awake()
        {
            _playButton.onClick.AddListener(() => PlayGame());
            _settingsButton.onClick.AddListener(() => _settingsUI.ShowPanel());
        }

        private void Start()
        {
            _currentLevelText.SetText($"Current lvl: {_currentLevel}");
        }

        private void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadData(GameData data)
        {
            _currentLevel = data.CurrentLevel;
        }

        public void SaveData(GameData data)
        {

        }
    }
}