using System;
using UnityEngine;
using UnityEngine.UI;

using GameSettingProperty;

public class PlayerState : MonoBehaviour
{
    [NonSerialized] public bool m_IsDead = false;         // 플레이어 죽음 상태
    [NonSerialized] public int m_CurrentHp = 0;           // 플레이어 유동적인 체력

    [NonSerialized] public Slider m_PlayerHpSlider;       // 플레이어 체력을 표시하는 슬라이드바 UI

    [Header("Player state settings")]
    [SerializeField] private int m_PlayerHp = 0;          // 플레이어 기본 체력 값

    [Header("Player Audioes")]
    [SerializeField] private AudioClip m_PlayerDeadAudio; // 플레이어 죽은 소리 클립

    private AudioSource m_PlayerAudio;                    // 플레이어 오디오 소스 (기본 소리 값 : 플레이어 다치는 소리 클립)
    private Animator m_PlayerAnimator;                    // 플레이어 애니메이션 컨트롤 애니메이터
    private PlayerMove m_PlayerMove;                      // 플레이어 움직임 컴포넌트
    private PlayerGaze m_PlayerGaze;                      // 플레이어 시선 컴포넌트
    private PlayerGunFire m_PlayerGunFire;                // 플레이어 총 발사 컴포넌트

    private void Start()
    {
        PlayerReset();
    }

    private void PlayerReset()
    {
        m_PlayerMove = GetComponent<PlayerMove>();
        m_PlayerGaze = GetComponent<PlayerGaze>();
        m_PlayerAudio = GetComponentInChildren<AudioSource>();
        m_PlayerAnimator = GetComponentInChildren<Animator>();
        m_PlayerGunFire = GetComponentInChildren<PlayerGunFire>();

        GetComponent<CapsuleCollider>().enabled = true;

        m_PlayerMove.enabled = true;
        m_PlayerGaze.enabled = true;
        m_PlayerGunFire.enabled = true;

        m_IsDead = false;

        m_CurrentHp = m_PlayerHp;
        m_PlayerHpSlider.value = m_CurrentHp;
    }

    /// <summary>
    /// 플레이어가 데미지를 받으면 호출
    /// </summary>
    /// <param name="damage">적 데미지</param>
    public void PlayerTakeDamage(int damage)
    {
        if (m_CurrentHp > 0)
        {
            m_CurrentHp -= damage;
            m_PlayerHpSlider.value = m_CurrentHp;
            m_PlayerAudio.Play();
        }
        else
        {
            PlayerDead();
        }
    }

    /// <summary>
    /// 플레이어가 죽으면 호출
    /// </summary>
    public void PlayerDead()
    {
        if (m_IsDead)
        {
            return;
        }

        m_IsDead = true;

        m_PlayerMove.enabled = false;
        m_PlayerGaze.enabled = false;
        m_PlayerGunFire.enabled = false;

        GetComponent<CapsuleCollider>().enabled = false;

        m_CurrentHp = 0;

        m_PlayerAnimator.SetTrigger("gameOver");
        m_PlayerAudio.PlayOneShot(m_PlayerDeadAudio);
    }

    /// <summary>
    /// 플레이어가 죽는 애니메이션이 시작할때 호출
    /// </summary>
    /// <remarks>
    /// 플레이어 "Death" 애니메이션에 등록되어 있습니다.
    /// </remarks>
    private void StartDeath()
    {
        GameManager.Instance.GameState = GameState.GameOver;
    }

    /// <summary>
    /// 플레이어가 죽는 애니메이션이 끝날때 호출
    /// </summary>
    /// <remarks>
    /// 플레이어 "Death" 애니메이션에 등록되어 있습니다.
    /// </remarks>
    private void RestartLevel()
    {

    }
}
