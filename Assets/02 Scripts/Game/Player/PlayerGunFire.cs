using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunFire : MonoBehaviour
{
    [Header("Gun settings")]
    [Tooltip("Time between gun fire")]
    public int m_GunDamage = 0;                   // ÃÑ µ¥¹ÌÁö
    public float m_GunFireInterval = 0.0f;        // ÃÑ ¹ß»ç °£°Ý
    public float m_GunRange = 0.0f;               // ÃÑ ¹ß»ç °Å¸®

    [SerializeField]
    [Tooltip("Time between gun firing effects\nDefault value = 0.2f")]
    private float m_GunEffectTimeInterval = 0.0f; // ÃÑ ¹ß»ç ÀÌÆåÆ® °£°Ý

    private float m_Timer = 0.0f;                 
    private float m_HitDistance = 0.0f;

    private Ray m_GunRay;
    private RaycastHit m_GunRayHit;

    private Light m_GunLight;                     // ÃÑ±¸ ºÒºû
    private LineRenderer m_GunLine;               // ÃÑ¾Ë ±¥Àû
    private ParticleSystem m_GunParticle;         // ÃÑ±¸ È­¿°
    private AudioSource m_GunAudio;               // ÃÑ ¹ß»ç ¼Ò¸®

    private void Awake()
    {
        m_GunLight = GetComponent<Light>();
        m_GunLine = GetComponent<LineRenderer>();
        m_GunAudio = GetComponent<AudioSource>();
        m_GunParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        m_GunLight.enabled = false;
        m_GunLine.enabled = false;

        m_GunRay = new Ray();
    }

    private void Update()
    {
        if (GameManager.Instance.m_GameState != GameState.Play)
        {
            return;
        }

        m_Timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && m_Timer >= m_GunFireInterval)
        {
            FireControl(true);
        }

        if(m_Timer >= m_GunFireInterval * m_GunEffectTimeInterval)
        {
            FireControl(false);
        }
    }

    /// <summary>
    /// ÃÑ ¹ß»ç ÀÌÆåÆ®, »ç¿îµå, ÆÄÆ¼Å¬ ÄÁÆ®·Ñ
    /// </summary>
    /// <param name="isFire">ÃÑ ¹ß»ç ¿©ºÎ</param>
    private void FireControl(bool isFire)
    {
        m_GunLight.enabled = isFire;
        m_GunLine.enabled = isFire;

        if (!isFire)
        {
            return;
        }

        m_GunRay.origin = transform.position;
        m_GunRay.direction = m_GunLight.transform.forward;

        if (!Physics.Raycast(m_GunRay.origin, m_GunRay.direction, out m_GunRayHit, m_GunRange))
        {
            return;
        }

        m_HitDistance = Vector3.Distance(transform.position, m_GunRayHit.point);
        m_GunLine.SetPosition(1, new Vector3(0.0f, 0.0f, m_HitDistance));

        m_GunAudio.Play();
        m_GunParticle.Play();

        m_Timer = 0;

        if (!m_GunRayHit.collider.gameObject.GetComponent<EnemyState>())
        {
            return;
        }

        m_GunRayHit.rigidbody.gameObject.GetComponent<EnemyState>().EnemyTakeDamage(m_GunDamage, m_GunRayHit.point);
    }
}
