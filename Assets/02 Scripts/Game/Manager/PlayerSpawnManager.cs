using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameSettingProperty;

public class PlayerSpawnManager : MonoBehaviour
{
    [Header("Player Prefabs")]
    [SerializeField] private GameObject playerObject;

    [Header("Set Player UI")]
    [SerializeField] private Slider hpSlider;

    private GameObject currentPlayer;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (currentPlayer != null)
        {
            return;
        }
        currentPlayer = Instantiate(playerObject);

        Camera.main.GetComponent<CameraMove>().targetTransform = currentPlayer.transform;
        currentPlayer.GetComponent<PlayerState>().m_PlayerHpSlider = hpSlider;
    }

    public void ResetPlayer()
    {
        currentPlayer.transform.position = Vector3.zero;
    }
}
