using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Managers
{
    public class MusicManager : BaseSingleton<MusicManager>
    {
        private const string MUSIC_VOLUME = "MusicVolume";

        public AudioSource MusicSource { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            MusicSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            MusicSource.volume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0);
        }
    }
}