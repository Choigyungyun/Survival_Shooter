using SurvivalShooter;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : CanvasManager
{
    [SerializeField] private GameObject m_GamePanel;
    [SerializeField] private GameObject m_PausePanel;
    [SerializeField] private GameObject m_SettingPanel;
    [SerializeField] private GameObject m_FadePanel;

    [Header("Scoreboard panel texts")]
    [SerializeField] private Text m_ScoreText;
    [SerializeField] private Text m_RoundTimeText;

    [Header("Fade panel texts")]
    [SerializeField] private Text m_StateText;
    [SerializeField] private Button m_FadeHomeButton;

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
        GameManager.Instance.RoundTimeCount += SetRoundTime;
        GameManager.Instance.StartTimeCount += SetStartTime;
        GameManager.Instance.OnGameRoundEnd += SetGameRoundEnd;
        GameManager.Instance.OnGameOver += SetGameOver;
    }

    private void OnDisable()
    {
        GameManager.Instance.RoundCount -= SetGameRound;
        GameManager.Instance.ScoreCount -= SetGameScore;
        GameManager.Instance.RoundTimeCount -= SetRoundTime;
        GameManager.Instance.StartTimeCount -= SetStartTime;
        GameManager.Instance.OnGameRoundEnd -= SetGameRoundEnd;
        GameManager.Instance.OnGameOver -= SetGameOver;
    }

    private void Start()
    {
        GameCanavsReset();

        // 버튼 이벤트
        m_PauseButton.onClick.AddListener(Pause);
        m_ReturnButton.onClick.AddListener(ReturnGame);
        m_SettingButton.onClick.AddListener(() => PanelControl(m_SettingPanel, false));
        m_HomeButton.onClick.AddListener(Home);
        m_SettingBackButton.onClick.AddListener(() => PanelControl(m_PausePanel, false));
        m_FadeHomeButton.onClick.AddListener(Home);

        // 사운드 슬라이드
        m_MainSoundSlider.onValueChanged.AddListener(SoundManager.Instance.SetMasterVolume);
        m_MusicSoundSlider.onValueChanged.AddListener(SoundManager.Instance.SetMusicVolume);
        m_EffectsSoundSlider.onValueChanged.AddListener(SoundManager.Instance.SetEffectVolume);
    }

    private void GameCanavsReset()
    {
        m_PausePanel.SetActive(false);
        m_SettingPanel.SetActive(false);
        m_FadeHomeButton.gameObject.SetActive(false);

        m_FadePanel.SetActive(true);
        m_StateText.gameObject.SetActive(true);


        m_CurrentFadeImage = m_FadePanel.GetComponent<Image>();
        m_PreviousPanel = m_GamePanel;

        SoundManager.Instance.InitialSoundSetting();

        m_MainSoundSlider.value = SoundManager.Instance.m_MasterVolume;
        m_MusicSoundSlider.value = SoundManager.Instance.m_MusicVolume;
        m_EffectsSoundSlider.value = SoundManager.Instance.m_EffectsVolume;

        StartCoroutine(PanelFadeControl(m_CurrentFadeImage, 1.0f, 0.0f, 1.0f));
    }

    #region 게임 이벤트 함수
    private void SetGameRound(int round)
    {
        m_StateText.text = $"Day {round}";
    }

    private void SetGameScore(int score)
    {
        m_ScoreText.text = $"Score : {score}";
    }

    private void SetRoundTime(float time)
    {
        m_RoundTimeText.text = $"Time : {(int)time}";
    }

    private void SetStartTime(float time)
    {
        if(time > 0.0f)
        {
            m_StateText.text = $"{(int)time}";
        }
        else
        {
            m_FadePanel.SetActive(false);
            m_StateText.gameObject.SetActive(false);
        }
    }

    private void SetGameRoundEnd()
    {
        StartCoroutine(RoundEnd());
    }

    private void SetGameOver()
    {
        StartCoroutine(GameOver());
    }
    #endregion


    #region 버튼 이벤트
    private void Pause()
    {
        PanelControl(m_PausePanel, false);
        GameManager.Instance.OnGameState(GameState.Pause);
    }

    private void ReturnGame()
    {
        PanelControl(m_GamePanel, false);

        m_FadePanel.SetActive(true);
        m_StateText.gameObject.SetActive(true);

        GameManager.Instance.OnGameState(GameState.Return);
    }

    private void Home()
    {
        SceneManager.LoadScene("MainScene");
    }
    #endregion

    private IEnumerator RoundEnd()
    {
        m_FadePanel.SetActive(true);
        StartCoroutine(PanelFadeControl(m_CurrentFadeImage, 0.0f, 1.0f, 0.0f));
        yield return new WaitForSeconds(1.0f);

        m_StateText.gameObject.SetActive(true);
        StartCoroutine(PanelFadeControl(m_CurrentFadeImage, 1.0f, 0.0f, 1.0f));
        GameManager.Instance.OnGameState(GameState.Ready);
    }

    private IEnumerator GameOver()
    {
        m_FadePanel.SetActive(true);
        StartCoroutine(PanelFadeControl(m_CurrentFadeImage, 0.0f, 1.0f, 1.0f));
        yield return new WaitForSeconds(1.0f);

        m_StateText.gameObject.SetActive(true);
        m_FadeHomeButton.gameObject.SetActive(true);
        m_StateText.text = "Game Over";
    }
}
