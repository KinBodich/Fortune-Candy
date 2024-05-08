using Common.Managers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace UI
{
    public class SettingsUI : MonoBehaviour
    {
        private const string MUSIC_VOLUME = "MusicVolume";
        private const string SOUND_VOLUME = "SoundVolume";

        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        [SerializeField] private Button _closeButton;

        private SoundManager _soundManager;
        private MusicManager _musicManager;

        private void Awake()
        {
            _soundManager = SoundManager.Instance;
            _musicManager = MusicManager.Instance;

            _closeButton.onClick.AddListener(() => HidePanel());
        }

        private void Start()
        {
            _musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0);
            _soundSlider.value = PlayerPrefs.GetFloat(SOUND_VOLUME, 0);
        }

        public void SetSoundVolume()
        {
            _soundManager.SetVolume(_soundSlider.value);
        }

        public void SetMusicVolume()
        {
            _musicManager.MusicSource.volume = _musicSlider.value;
            PlayerPrefs.SetFloat(MUSIC_VOLUME, _musicSlider.value);
        }

        public void ShowPanel()
        {
            gameObject.SetActive(true);
        }

        public void HidePanel()
        {
            gameObject.SetActive(false);
        }
    }
}