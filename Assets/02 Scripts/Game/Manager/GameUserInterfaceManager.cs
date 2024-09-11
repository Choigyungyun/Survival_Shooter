using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUserInterfaceManager : MonoBehaviour
{
    public float m_FadeTime;

    public virtual void GetRound(int round)
    {

    }

    public virtual void GetRoundTime(float time)
    {

    }

    public virtual void GetScore(int score) { }

    public virtual void InterfaceStateControl(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Nothing:
                break;
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
            case GameState.EndGame:
                break;
        }
    }

    #region Fade control Functions

    #endregion
}
