using UnityEngine;
using UnityEngine.Audio;
using SurvivalShooter.Event;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer m_MasterMixer;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        UIEvent.MasterSoundEvent += SetMasterVolume;
        UIEvent.MusicSoundEvent += SetMusicVolume;
        UIEvent.EffectsSoundEvent += SetEffectVolume;
    }

    private void OnDisable()
    {
        UIEvent.MasterSoundEvent -= SetMasterVolume;
        UIEvent.MusicSoundEvent -= SetMusicVolume;
        UIEvent.EffectsSoundEvent -= SetEffectVolume;
    }

    private void SetMasterVolume(float volume)
    {
        m_MasterMixer.SetFloat("Master", volume);
    }

    private void SetMusicVolume(float volume)
    {
        m_MasterMixer.SetFloat("Music", volume);
    }

    private void SetEffectVolume(float volume)
    {
        m_MasterMixer.SetFloat("Effects", volume);
    }
}
