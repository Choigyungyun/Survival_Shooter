using System;
using UnityEngine;
using UnityEngine.UI;

using GameSettingProperty;

public class PlayerState : MonoBehaviour
{
    [NonSerialized] public bool m_IsDead = false;         // �÷��̾� ���� ����
    [NonSerialized] public int m_CurrentHp = 0;           // �÷��̾� �������� ü��

    [NonSerialized] public Slider m_PlayerHpSlider;       // �÷��̾� ü���� ǥ���ϴ� �����̵�� UI

    [Header("Player state settings")]
    [SerializeField] private int m_PlayerHp = 0;          // �÷��̾� �⺻ ü�� ��

    [Header("Player Audioes")]
    [SerializeField] private AudioClip m_PlayerDeadAudio; // �÷��̾� ���� �Ҹ� Ŭ��

    private AudioSource m_PlayerAudio;                    // �÷��̾� ����� �ҽ� (�⺻ �Ҹ� �� : �÷��̾� ��ġ�� �Ҹ� Ŭ��)
    private Animator m_PlayerAnimator;                    // �÷��̾� �ִϸ��̼� ��Ʈ�� �ִϸ�����
    private PlayerMove m_PlayerMove;                      // �÷��̾� ������ ������Ʈ
    private PlayerGaze m_PlayerGaze;                      // �÷��̾� �ü� ������Ʈ
    private PlayerGunFire m_PlayerGunFire;                // �÷��̾� �� �߻� ������Ʈ

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
    /// �÷��̾ �������� ������ ȣ��
    /// </summary>
    /// <param name="damage">�� ������</param>
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
    /// �÷��̾ ������ ȣ��
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
    /// �÷��̾ �״� �ִϸ��̼��� �����Ҷ� ȣ��
    /// </summary>
    /// <remarks>
    /// �÷��̾� "Death" �ִϸ��̼ǿ� ��ϵǾ� �ֽ��ϴ�.
    /// </remarks>
    private void StartDeath()
    {
        GameManager.Instance.OnGameState(GameState.GameOver);
    }

    /// <summary>
    /// �÷��̾ �״� �ִϸ��̼��� ������ ȣ��
    /// </summary>
    /// <remarks>
    /// �÷��̾� "Death" �ִϸ��̼ǿ� ��ϵǾ� �ֽ��ϴ�.
    /// </remarks>
    private void RestartLevel()
    {
        GameManager.Instance.OnGameOver?.Invoke();
    }
}
