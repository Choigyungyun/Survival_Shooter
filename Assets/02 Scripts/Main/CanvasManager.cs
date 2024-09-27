using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private const float c_CurrentFadeTime = 1.0f;

    protected GameObject m_PreviousPanel;
    protected Image m_CurrentFadeImage;

    protected void PanelControl(GameObject panel, bool isFade)
    {
        if (m_PreviousPanel == panel)
        {
            Debug.Log("같은 패널 화면 입니다.");
            return;
        }

        m_PreviousPanel = panel;

        if (isFade)
        {
            StartCoroutine(PanelChange(panel, m_PreviousPanel, c_CurrentFadeTime));
        }
        else
        {
            m_PreviousPanel.SetActive(false);
            panel.SetActive(true);
        }
    }

    /// <summary>
    /// 패널의 이미지를 Fade In, Fade Out
    /// </summary>
    /// <param name="panelImage">Fade In/Out 할 UnityEngine.UI의 Image</param>
    /// <param name="startAlpha">Image의 페이드 초기 알파 값(min = 0.0f, max = 1.0f)</param>
    /// <param name="endAlpha">Image의 페이드 마지막 알파 값(min = 0.0f, max = 1.0f)</param>
    /// <param name="waitTime">초기 설정된 알파 값의 유지 시간, 유지 시간이 끝나면 페이드 시작</param>
    /// <returns></returns>
    protected IEnumerator PanelFadeControl(Image panelImage, float startAlpha, float endAlpha, float waitTime)
    {
        Color color = panelImage.color;
        color.a = startAlpha;
        panelImage.color = color;

        yield return new WaitForSeconds(waitTime);

        float timer = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            timer += Time.deltaTime;
            percent = timer / c_CurrentFadeTime;

            color.a = Mathf.Lerp(startAlpha, endAlpha, percent);

            panelImage.color = color;

            yield return null;
        }
    }

    /// <summary>
    /// 패널 전환
    /// </summary>
    /// <param name="nextPanel">전환할 다음 패널</param>
    /// <param name="previousePanel">전환되기 이전 패널(현재 패널)</param>
    /// <param name="waitFadeTime">페이드 시작하기 까지 유지 시간</param>
    /// <returns></returns>
    protected IEnumerator PanelChange(GameObject nextPanel, GameObject previousePanel, float waitFadeTime)
    {
        StartCoroutine(PanelFadeControl(m_CurrentFadeImage, 0.0f, 1.0f, 0.0f));

        yield return new WaitForSeconds(c_CurrentFadeTime);

        previousePanel.SetActive(false);
        nextPanel.SetActive(true);

        StartCoroutine(PanelFadeControl(m_CurrentFadeImage, 1.0f, 0.0f, waitFadeTime));
    }
}
