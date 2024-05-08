using Common.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class FailUI : MonoBehaviour
    {
        [SerializeField] private GameObject _failPanel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        private GameManager _gameManager;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _gameManager = GameManager.Instance;
            _gameManager.OnGameFailed += () =>
            {
                _failPanel.SetActive(true);
                _audioSource.Play();
            };

            _restartButton.onClick.AddListener(() => SceneManager.LoadScene(1));
            _menuButton.onClick.AddListener(()=>SceneManager.LoadScene(0));
        }
    }
}