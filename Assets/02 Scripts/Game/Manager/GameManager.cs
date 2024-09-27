using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 게임 상태 열거
/// </summary>
public enum GameState
{
    Nothing = 0,
    Ready,
    Play,
    Pause,
    RoundEnd,
    GameOver
}

public class GameManager : GenericSingleton<GameManager>
{
    [HideInInspector]
    public GameState GameState
    {
        get
        {
            return m_GameState;
        }
        set
        {
            switch (value)
            {
                case GameState.Ready:
                    break;
                case GameState.Play:
                    break;
                case GameState.Pause:
                    break;
                case GameState.RoundEnd:
                    break;
                case GameState.GameOver:
                    break;
            }
        }
    }
    
    [SerializeField] private GameObject m_SpawnManagerObject;                                     // 스폰 매니저 오브젝트
    [SerializeField] private GameObject m_GameUiManagerObject;                                    // 게임 UI 매니저 오브젝트

    // 매니저
    private PlayerSpawnManager m_PlayerSpawnManager;                                              // 플레이어 스폰 관리
    private EnemySpawnManager m_EnemySpawnManager;                                                // 적 스폰 관리

    private GameState m_GameState;

    private int m_GameScore = 0;                                                                  // 게임 스코어
    private int m_GameRound = 1;                                                                  // 게임 라운드
    private float m_StartTime = 60.9f;

    private void Awake()
    {
        m_PlayerSpawnManager = m_SpawnManagerObject.GetComponent<PlayerSpawnManager>();
        m_EnemySpawnManager = m_SpawnManagerObject.GetComponent<EnemySpawnManager>();
    }

    private void Start()
    {

    }

    public void OnGameState(GameState state)
    {
        m_GameState = state;

        switch (state)
        {
            case GameState.Ready:
                break;
            case GameState.Play:
                break;
            case GameState.Pause:
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
    }

    private IEnumerator RoundTime(float maxTime)
    {
        while (maxTime > 0.0f)
        {
            maxTime -= Time.deltaTime;

            if (maxTime > 0.0f)
            {
            }
            else
            {
                m_GameRound += 1;
            }
            yield return null;
        }
    }
}
