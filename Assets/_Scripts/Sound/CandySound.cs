using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class CandySound : MonoBehaviour
    {
        [SerializeField] private bool _onHit;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (_onHit)
            {
                CandyCollisionManager.OnHit += OnHit;
            }
            else
            {
                CandyCollisionManager.OnDestroy += OnHit;
            }
        }

        private void OnDisable()
        {
            CandyCollisionManager.OnHit -= OnHit;
            CandyCollisionManager.OnDestroy -= OnHit;
        }

        private void OnHit()
        {
            _audioSource.Play();
        }
    }
}