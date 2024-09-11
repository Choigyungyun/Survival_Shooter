using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeCanvasEvent : CanvasManager
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
    [SerializeField] private Slider m_EffectSoundSlider;

    private void Start()
    {
        Reeset();

        // Home Button Events
        m_StartButton.onClick.AddListener(GameStart);
        m_SsettingButton.onClick.AddListener(() => PanelControl(m_SettingPanel, true));
        m_QuitButton.onClick.AddListener(QuitGame);

        // Setting Button Events
        m_SettingBackButton.onClick.AddListener(() => PanelControl(m_HomePanel, true));
    }

    private void Reeset()
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
