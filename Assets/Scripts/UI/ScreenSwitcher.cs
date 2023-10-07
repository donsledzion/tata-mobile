using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitcher : MonoBehaviour
{
    [SerializeField] private CanvasGroup checkConnectionCanvasGroup;
    [SerializeField] private CanvasGroup loginCanvasGroup;
    [SerializeField] private CanvasGroup welcomeCanvasGroup;
    [SerializeField] private CanvasGroup linksCanvasGroup;
    //[SerializeField] private CanvasGroup feedsCanvasGroup;
    [SerializeField] private CanvasGroup mainPanelCanvasGroup;
    [SerializeField] private float fadeSpeed;

    private CanvasGroup activePanel;
    private CanvasGroup targetPanel;
    private bool fadingOut = false;
    private bool fadingIn = false;

    public bool Switching => fadingOut || fadingIn;

    private void Start()
    {
        activePanel = checkConnectionCanvasGroup;
    }

    private void SwitchToPanel(CanvasGroup cg)
    {
        if (fadingOut) return;
        targetPanel = cg;
        if (fadingIn) return;
        StartCoroutine(SwitchToTargetPanelCor());
    }

    private IEnumerator SwitchToTargetPanelCor()
    {
        StartCoroutine(DisableActivePanelCor());
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => !fadingOut);
        StartCoroutine(EnableTargetPanelCor());

    }

    private IEnumerator DisableActivePanelCor()
    {
        fadingOut = true;
        while (activePanel.alpha > 0f)
        {
            activePanel.alpha = Mathf.Clamp(activePanel.alpha - fadeSpeed * Time.deltaTime,0f,1f);
            yield return null;
        }
        fadingOut = false;
        activePanel.gameObject.SetActive(false);
    }

    private IEnumerator EnableTargetPanelCor()
    {
        fadingIn = true;
        targetPanel.alpha = 0f;
        targetPanel.gameObject.SetActive(true);
        while (targetPanel.alpha < 1f)
        {
            targetPanel.alpha = Mathf.Clamp(targetPanel.alpha + fadeSpeed * Time.deltaTime, 0f, 1f);
            yield return null;
        }
        fadingIn = false;
        activePanel = targetPanel;
        targetPanel = null;
    }

    [ContextMenu("SwitchToLoginPanel")]
    public void SwitchToLoginPanel()
    {
        SwitchToPanel(loginCanvasGroup);

    }
    [ContextMenu("SwitchToWelcomePanel")]
    public void SwitchToWelcomePanel()
    {
        SwitchToPanel(welcomeCanvasGroup);
    }

    [ContextMenu("SwitchToLinksPanel")]
    public void SwitchToLinksPanel()
    {
        SwitchToPanel(linksCanvasGroup);
    }

    /*[ContextMenu("SwitchToFeedsPanel")]
    public void SwitchToFeedsPanel()
    {
        SwitchToPanel(feedsCanvasGroup);
    }*/

    [ContextMenu("SwitchToCheckConnectionPanel")]
    public void SwitchToCheckConnectionPanel()
    {
        SwitchToPanel(checkConnectionCanvasGroup);
    }

    [ContextMenu("SwitchToMainPanel")]
    public void SwitchToMainPanel()
    {
        SwitchToPanel(mainPanelCanvasGroup);
    }
}
