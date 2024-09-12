using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SurvivalShooter.Event;

public class HomeCanvas : CanvasManager
{
    [Header("Panel Control")]
    [SerializeField] private float m_FadeTime = 0.0f;

    [Header("Panels")]
    [SerializeField] private GameObject m_HomePanel;
    [SerializeField] private GameObject m_SettingPanel;
    [SerializeField] private GameObject m_FadePanel;

    [Header("Home Buttons")]
    [SerializeField] private Button m_StartButton;
    [SerializeField] private Button m_SsettingButton;
    [SerializeField] private Button m_QuitButton;

    [Header("Setting Buttons")]
    [SerializeField] private Button m_SettingBackButton;

    [Header("Setting Sliders")]
    [SerializeField] private Slider m_MainSoundSlider;
    [SerializeField] private Slider m_MusicSoundSlider;
    [SerializeField] private Slider m_EffectsSoundSlider;

    [Header("Audio Mix")]
    [SerializeField] private AudioMixer m_MasterMixer;

    private void Start()
    {
        HomeReeset();

        // Home Button Events
        m_StartButton.onClick.AddListener(GameStart);
        m_SsettingButton.onClick.AddListener(() => PanelControl(m_SettingPanel, true));
        m_QuitButton.onClick.AddListener(QuitGame);

        // Setting Button Events
        m_SettingBackButton.onClick.AddListener(() => PanelControl(m_HomePanel, true));

        // Setting Sound Slider & Audio Mixer
        m_MainSoundSlider.onValueChanged.AddListener(UIEvent.MasterSoundEvent.Invoke);
        m_MusicSoundSlider.onValueChanged.AddListener(UIEvent.MusicSoundEvent.Invoke);
        m_EffectsSoundSlider.onValueChanged.AddListener(UIEvent.EffectsSoundEvent.Invoke);
    }

    private void HomeReeset()
    {
        m_CurrentFadeImage = m_FadePanel.GetComponent<Image>();

        m_CurrentFadeTime = m_FadeTime;
        m_PreviousPanel = m_HomePanel;

        StartCoroutine(PanelFadeControl(m_CurrentFadeImage, 1.0f, 0.0f, 0.0f));
    }

    #region Button Events

    private void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion
}
