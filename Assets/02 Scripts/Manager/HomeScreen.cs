using UnityEngine.UIElements;
using SurvivalShooter.UI;

public class HomeScreen : UIScreen
{
    private Button m_PlayButton;
    private Button m_SettingButton;
    private Button m_Quit;

    private void SetVisualElements()
    {
        m_PlayButton = m_RootElenet.Q<Button>("play_button");
        m_SettingButton = m_SettingButton.Q<Button>("setting_button");
        m_Quit = m_Quit.Q<Button>("quit_button");
    }
}
