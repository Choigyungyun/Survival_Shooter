using SurvivalShooter;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : CanvasManager
{
    [SerializeField] private GameObject m_ScoreBoardPanel;
    [SerializeField] private GameObject m_PausePanel;
    [SerializeField] private GameObject m_SettingPanel;
    [SerializeField] private GameObject m_FadePanel;

    [Header("Scoreboard panel texts")]
    [SerializeField] private Text m_ScoreText;
    [SerializeField] private Text m_RoundText;

    [Header("Fade panel texts")]
    [SerializeField] private Text m_StateText;

    [Header("Buttons")]
    [SerializeField] private Button m_PauseButton;
    [SerializeField] private Button m_ReturnButton;
    [SerializeField] private Button m_SettingButton;
    [SerializeField] private Button m_HomeButton;

    [Header("Setting sliders")]
    [SerializeField] private Slider m_MainSoundSlider;
    [SerializeField] private Slider m_MusicSoundSlider;
    [SerializeField] private Slider m_EffectsSoundSlider;

    private void Start()
    {
        m_PauseButton.onClick.AddListener(Pause);
        m_ReturnButton.onClick.AddListener(ReturnGame);
        m_SettingButton.onClick.AddListener(Setting);
        m_HomeButton.onClick.AddListener(Home);

        m_MainSoundSlider.onValueChanged.AddListener(SoundManager.Instance.SetMasterVolume);
        m_MusicSoundSlider.onValueChanged.AddListener(SoundManager.Instance.SetMusicVolume);
        m_EffectsSoundSlider.onValueChanged.AddListener(SoundManager.Instance.SetEffectVolume);
    }

    private void GameCanavsReset()
    {
        m_CurrentFadeImage = m_FadePanel.GetComponent<Image>();
        m_PreviousPanel = null;
    }

    #region 버튼 이벤트
    private void Pause()
    {
        PanelControl(m_PausePanel, false);
    }

    private void ReturnGame()
    {
        m_PreviousPanel = null;
    }

    private void Setting()
    {
        PanelControl(m_SettingPanel, true);
    }

    private void Home()
    {
        SceneManager.LoadScene("MainScene");
    }
    #endregion
}
