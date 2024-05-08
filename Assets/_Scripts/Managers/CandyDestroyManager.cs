using Mechanics;
using UnityEngine;

namespace Common.Managers
{
    public class CandyDestroyManager : MonoBehaviour
    {
        private int _destroyedParts = 0;

        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
        }

        private void OnEnable()
        {
            CandyCollisionManager.OnDestroy += OnCandyDestroy;
        }

        private void OnDisable()
        {
            CandyCollisionManager.OnDestroy -= OnCandyDestroy;
        }

        private void OnCandyDestroy()
        {
            _destroyedParts++;

            if (_destroyedParts >= 5)
            {
                _gameManager.UpdateGameState(GameState.Success);
            }
        }
    }
}