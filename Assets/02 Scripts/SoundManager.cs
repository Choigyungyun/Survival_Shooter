using UnityEngine;
using UnityEngine.Audio;
using SurvivalShooter.Event;

namespace SurvivalShooter
{
    public class SoundManager : GenericSingleton<SoundManager>
    {
        [SerializeField] private AudioMixer m_MasterMixer;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void SetMasterVolume(float volume)
        {
            m_MasterMixer.SetFloat("Master", volume);
        }

        public void SetMusicVolume(float volume)
        {
            m_MasterMixer.SetFloat("Music", volume);
        }

        public void SetEffectVolume(float volume)
        {
            m_MasterMixer.SetFloat("Effects", volume);
        }
    }
}