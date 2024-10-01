using SurvivalShooter;
using System.Collections;
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
    [SerializeField] private Button m_SettingBackButton;

    [Header("Setting sliders")]
    [SerializeField] private Slider m_MainSoundSlider;
    [SerializeField] private Slider m_MusicSoundSlider;
    [SerializeField] private Slider m_EffectsSoundSlider;

    private void OnEnable()
    {
        GameManager.Instance.RoundCount += SetGameRound;
        GameManager.Instance.ScoreCount += SetGameScore;
    }

    private void Start()
    {
        GameCanavsReset();

        // 버튼 이벤트
        m_PauseButton.onClick.AddListener(Pause);
        m_ReturnButton.onClick.AddListener(ReturnGame);
        m_SettingButton.onClick.AddListener(() => PanelControl(m_SettingPanel, true));
        m_HomeButton.onClick.AddListener(Home);
        m_SettingBackButton.onClick.AddListener(() => PanelControl(m_PausePanel, true));

        // 사운드 슬라이드
        m_MainSoundSlider.onValueChanged.AddListener(SoundManager.Instance.SetMasterVolume);
        m_MusicSoundSlider.onValueChanged.AddListener(SoundManager.Instance.SetMusicVolume);
        m_EffectsSoundSlider.onValueChanged.AddListener(SoundManager.Instance.SetEffectVolume);
    }

    private void GameCanavsReset()
    {
        m_PausePanel.SetActive(false);
        m_SettingPanel.SetActive(false);
        m_FadePanel.SetActive(true);

        m_StateText.gameObject.SetActive(true);

        m_CurrentFadeImage = m_FadePanel.GetComponent<Image>();
        m_PreviousPanel = null;

        StartCoroutine(PanelFadeControl(m_CurrentFadeImage, 1.0f, 0.0f, 1.0f));
    }

    private void SetGameRound(int round)
    {

    }

    private void SetGameScore(int score)
    {

    }

    private IEnumerator GameReady()
    {
        yield return new WaitForSeconds(3.0f);
    }


    #region 버튼 이벤트
    private void Pause()
    {
        PanelControl(m_PausePanel, false);
        GameManager.Instance.OnGameState(GameState.Pause);
    }

    private void ReturnGame()
    {
        m_PreviousPanel = null;
        PanelControl(m_PausePanel, false);
    }

    private void Home()
    {
        SceneManager.LoadScene("MainScene");
    }
    #endregion
}
