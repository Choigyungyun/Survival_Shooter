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
    
    [SerializeField] private GameObject m_SpawnManagerObject;                                     // 스폰 매니저 오브젝트
    [SerializeField] private GameObject m_GameUiManagerObject;                                    // 게임 UI 매니저 오브젝트

    // 매니저
    private PlayerSpawnManager m_PlayerSpawnManager;                                              // 플레이어 스폰 관리
    private EnemySpawnManager m_EnemySpawnManager;                                                // 적 스폰 관리


    private int m_GameScore = 0;                                                                  // 게임 스코어
    private int m_GameRound = 1;                                                                  // 게임 라운드

    private const float c_RoundTime = 60.9f;
    private const float c_StartTime = 3.9f;

    private void Awake()
    {
        m_PlayerSpawnManager = m_SpawnManagerObject.GetComponent<PlayerSpawnManager>();
        m_EnemySpawnManager = m_SpawnManagerObject.GetComponent<EnemySpawnManager>();
    }

    private void Start()
    {

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

        switch (state)
        {
            case GameState.Ready:
                ScoreCount?.Invoke(m_GameScore);
                RoundCount?.Invoke(m_GameRound);
                break;
            case GameState.Play:
                break;
            case GameState.Pause:
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
}
