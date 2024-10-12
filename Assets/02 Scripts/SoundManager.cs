using UnityEngine;
using UnityEngine.Audio;
using SurvivalShooter.Event;

namespace SurvivalShooter
{
    public class SoundManager : GenericSingleton<SoundManager>
    {
        public float m_MasterVolume;
        public float m_MusicVolume;
        public float m_EffectsVolume;

        [SerializeField] private AudioMixer m_MasterMixer;

        public void InitialSoundSetting()
        {
            m_MasterMixer.GetFloat("Master", out m_MasterVolume);
            m_MasterMixer.GetFloat("Music", out m_MusicVolume);
            m_MasterMixer.GetFloat("Effects", out m_EffectsVolume);
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