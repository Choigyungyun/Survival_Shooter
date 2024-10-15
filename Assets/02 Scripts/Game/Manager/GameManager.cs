using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 게임 상태 열거
/// </summary>
public enum GameState
{
    Nothing = 0,
    Ready,
    Play,
    Pause,
    Return,
    RoundEnd,
    GameOver
}

public class GameManager : GenericSingleton<GameManager>
{
    public Action<float> StartTimeCount;
    public Action<float> RoundTimeCount;
    public Action<int> ScoreCount;
    public Action<int> RoundCount;
    public Action OnGameRoundEnd;
    public Action OnGameOver;

    [HideInInspector] public GameState m_GameState;

    [HideInInspector] public int m_GameScore = 0;                                                 // 게임 스코어
    [HideInInspector] public int m_GameRound = 1;                                                 // 게임 라운드

    [SerializeField] private GameObject m_SpawnManager;

    // 매니저
    private PlayerSpawnManager m_PlayerSpawnManager;                                              // 플레이어 스폰 관리
    private EnemySpawnManager m_EnemySpawnManager;                                                // 적 스폰 관리

    private IEnumerator m_RoundTimeEnumerator;

    private const float c_MaxRoundTime = 60.9f;
    private const float c_StartTime = 3.9f;

    private float m_RoundTime;

    private void Start()
    {
        m_PlayerSpawnManager = m_SpawnManager.GetComponent<PlayerSpawnManager>();
        m_EnemySpawnManager = m_SpawnManager.GetComponent<EnemySpawnManager>();

        GameReset();
    }

    private void GameReset()
    {
        m_GameScore = 0;
        m_GameRound = 1;

        OnGameState(GameState.Ready);
    }

    public void OnGameState(GameState state)
    {
        m_GameState = state;
        Debug.Log(m_GameState);
        switch (state)
        {
            case GameState.Ready:
                m_PlayerSpawnManager.ResetPlayer();
                StartCoroutine(GameReady());
                break;
            case GameState.Play:
                StartCoroutine(m_RoundTimeEnumerator);
                break;
            case GameState.Pause:
                StopCoroutine(m_RoundTimeEnumerator);
                break;
            case GameState.Return:
                StartCoroutine(GameReturn());
                break;
            case GameState.RoundEnd:
                OnGameRoundEnd?.Invoke();
                RoundCount?.Invoke(m_GameRound);
                break;
            case GameState.GameOver:
                StopCoroutine(m_RoundTimeEnumerator);
                break;
        }
    }

    public void AddScore(int score)
    {
        m_GameScore += score;

        ScoreCount?.Invoke(m_GameScore);
    }

    private IEnumerator RoundTime(float maxTime)
    {
        while (maxTime > 0.0f)
        {
            maxTime -= Time.deltaTime;
            m_RoundTime = maxTime;

            if (maxTime > 0.0f)
            {
                RoundTimeCount?.Invoke(maxTime);
            }
            else
            {
                m_GameRound += 1;

                OnGameState(GameState.RoundEnd);
            }
            yield return null;
        }
    }

    private IEnumerator CountDown(float maxTime)
    {
        while (maxTime > 0.0f)
        {
            maxTime -= Time.deltaTime;

            StartTimeCount?.Invoke(maxTime);
            yield return null;
        }
    }

    private IEnumerator GameReady()
    {
        ScoreCount?.Invoke(m_GameScore);
        RoundCount?.Invoke(m_GameRound);
        RoundTimeCount?.Invoke(c_MaxRoundTime);

        m_RoundTime = c_MaxRoundTime;
        m_RoundTimeEnumerator = RoundTime(m_RoundTime);

        yield return new WaitForSeconds(3.0f);

        StartCoroutine(CountDown(c_StartTime));
        yield return new WaitForSeconds(c_StartTime);

        OnGameState(GameState.Play);
    }

    private IEnumerator GameReturn()
    {
        StartCoroutine(CountDown(c_StartTime));
        yield return new WaitForSeconds(c_StartTime);

        OnGameState(GameState.Play);
    }
}
