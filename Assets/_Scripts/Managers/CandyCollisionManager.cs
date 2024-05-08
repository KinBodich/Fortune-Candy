using Common;
using Common.Managers;
using System;
using UnityEngine;

namespace Mechanics
{
    public class CandyCollisionManager : MonoBehaviour
    {
        [SerializeField] private CandyType _requiredType;

        [SerializeField] private Sprite _standartCandy;
        [SerializeField] private Sprite _crackedCandy;
        private CandyDrag[] requiredCandies;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem _particleSystem;

        private int _health;

        public static event Action OnDestroy;
        public static event Action OnHit;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            CheckRequiredCandies();
        }

        private void CheckRequiredCandies()
        {
            _spriteRenderer.sprite = _standartCandy;
            requiredCandies = FindObjectsOfType<CandyDrag>();
            foreach (var candy in requiredCandies)
            {
                if (candy.CandyType == _requiredType)
                {
                    _health++;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.TryGetComponent(out CandyDrag candyDrag)) return;
            if (candyDrag.IsDragging) return;
            if (candyDrag.CandyType != _requiredType)
            {
                //_gameManager.UpdateGameState(GameState.Failed);
                return;
            }

            _health = _health - 1;

            _spriteRenderer.sprite = _crackedCandy;

            Destroy(candyDrag.gameObject);
            _particleSystem.Play();

            if (_health <= 0)
            {
                OnDestroy?.Invoke();
                gameObject.SetActive(false);
                return;
            }

            OnHit?.Invoke();
        }
    }
}