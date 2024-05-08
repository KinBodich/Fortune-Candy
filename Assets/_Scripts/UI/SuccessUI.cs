using Common;
using Common.DataPersistence;
using Common.Managers;
using Mechanics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class SuccessUI : MonoBehaviour
    {
        [SerializeField] private GameObject _successPanel;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Timer _timer;

        private GameManager _gameManager;
        private CandySpawnManager _candySpawnManager;
        private ObstacleSpawnManager _obstacleSpawnManager;
        private AudioSource _audioSource;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _candySpawnManager = CandySpawnManager.Instance;
            _obstacleSpawnManager = ObstacleSpawnManager.Instance;
            _audioSource = GetComponent<AudioSource>();

            _menuButton.onClick.AddListener(() => SceneManager.LoadScene(0));
            _nextLevelButton.onClick.AddListener(() => NextLevel());
        }

        private void OnEnable()
        {
            _gameManager.OnGameSuccess += () =>
            {
                _successPanel.SetActive(true);
                _audioSource.Play();
                _candySpawnManager.CurrentLevel++;

                // Check if the current level is a multiple of 5 to increase the number of obstacles
                if (_candySpawnManager.CurrentLevel % 5 == 0 || _candySpawnManager.CurrentLevel == 3)
                {
                    _obstacleSpawnManager.NumObstacles++;
                }
                else
                {
                    _candySpawnManager.NumCandies++;
                }

                // Check if the current level is a multiple of 10 to increase MaxTimer
                if (_candySpawnManager.CurrentLevel % 10 == 0)
                {
                    // Increase MaxTimer by 5
                    _timer.MaxTimer += 5f;
                }
            };
        }



        private void OnDisable()
        {
            _gameManager.OnGameSuccess -= () => _successPanel.SetActive(true);
        }

        private void NextLevel()
        {
            _gameManager.UpdateGameState(Common.GameState.Playing);

            SceneManager.LoadScene(1);
        }
    }
}