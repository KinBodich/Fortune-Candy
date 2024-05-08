using UnityEngine;

namespace Common.Managers
{
    public class SoundManager : BaseSingleton<SoundManager>
    {
        private const string SOUND_VOLUME = "SoundVolume";

        private GameObject[] _soundObjects;
        private AudioSource[] _soundSources;

        private void Start()
        {
            _soundObjects = GameObject.FindGameObjectsWithTag(SOUND_VOLUME);
            _soundSources = new AudioSource[_soundObjects.Length];
            for (int i = 0; i < _soundObjects.Length; i++)
            {
                _soundSources[i] = _soundObjects[i].GetComponent<AudioSource>();
            }

            foreach (var sound in _soundSources)
            {
                sound.volume = PlayerPrefs.GetFloat(SOUND_VOLUME, 0);
            }
        }

        public void SetVolume(float volume)
        {
            PlayerPrefs.SetFloat(SOUND_VOLUME, volume);
            foreach (var sound in _soundSources)
            {
                sound.volume = volume;
            }
        }
    }
}