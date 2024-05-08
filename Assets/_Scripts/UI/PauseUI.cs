using Common;
using Common.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _homeButton;

        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GameManager.Instance;

            _pauseButton.onClick.AddListener(() =>
            {
                _gameManager.UpdateGameState(GameState.Paused);
                ShowPanel();
            });

            _resumeButton.onClick.AddListener(() =>
            {
                _gameManager.UpdateGameState(GameState.Resumed);
                HidePanel();
            });

            _homeButton.onClick.AddListener(() =>
            {
                _gameManager.UpdateGameState(GameState.Menu);
                SceneManager.LoadScene(0);
            });

            HidePanel();
        }

        private void ShowPanel()
        {
            _pausePanel.SetActive(true);
        }

        private void HidePanel()
        {
            _pausePanel.SetActive(false);
        }
    }
}