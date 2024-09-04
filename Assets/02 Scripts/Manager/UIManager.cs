using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using SurvivalShooter.UI;

public class UIManager : MonoBehaviour
{
    private UIScreen m_HomeScreen;
    private UIScreen m_SettingScreen;
    private UIScreen m_LevelSelectionScreen;
    private UIScreen m_GameScreen;
    private UIScreen m_PauseScreen;
    private UIScreen m_GameOverScreen;
    private UIScreen m_EndGameScreen;
    private UIScreen m_CurrentScreen;

    private Stack<UIScreen> m_HistoryStack = new Stack<UIScreen>();
}
