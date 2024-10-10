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

    public GameState m_GameState;

    [HideInInspector] public int m_GameScore = 0;                                                 // 게임 스코어
    [HideInInspector] public int m_GameRound = 1;                                                 // 게임 라운드

    [SerializeField] private GameObject m_SpawnManagerObject;                                     // 스폰 매니저 오브젝트

    // 매니저
    private PlayerSpawnManager m_PlayerSpawnManager;                                              // 플레이어 스폰 관리
    private EnemySpawnManager m_EnemySpawnManager;                                                // 적 스폰 관리

    private IEnumerator m_RoundTimeEnumerator;

    private const float c_MaxRoundTime = 60.9f;
    private const float c_StartTime = 3.9f;

    private float m_RoundTime;

    private void Awake()
    {
        m_PlayerSpawnManager = m_SpawnManagerObject.GetComponent<PlayerSpawnManager>();
        m_EnemySpawnManager = m_SpawnManagerObject.GetComponent<EnemySpawnManager>();
    }

    private void Start()
    {
        GameReset();
    }

    private void GameReset()
    {
        m_GameScore = 0;
        m_GameRound = 1;

        m_RoundTime = c_MaxRoundTime;

        m_RoundTimeEnumerator = RoundTime(m_RoundTime);

        OnGameState(GameState.Ready);
    }

    public void OnGameState(GameState state)
    {
        m_GameState = state;
        Debug.Log(m_GameState);
        switch (state)
        {
            case GameState.Ready:
                StartCoroutine(GameReady());
                break;
            case GameState.Play:
                StartCoroutine(m_RoundTimeEnumerator);
                break;
            case GameState.Pause:
                StopCoroutine(m_RoundTimeEnumerator);
                break;
            case GameState.Return:
                break;
            case GameState.RoundEnd:
                break;
            case GameState.GameOver:
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

                RoundCount?.Invoke(m_GameRound);
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
        yield return new WaitForSeconds(3.0f);

        StartCoroutine(CountDown(c_StartTime));
        yield return new WaitForSeconds(c_StartTime);

        OnGameState(GameState.Play);
    }
}
