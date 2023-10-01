using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitcher : MonoBehaviour
{
    [SerializeField] private CanvasGroup checkConnectionCanvasGroup;
    [SerializeField] private CanvasGroup loginConnectionCanvasGroup;
    [SerializeField] private CanvasGroup welcomeConnectionCanvasGroup;
    [SerializeField] private CanvasGroup linksCanvasGroup;
    [SerializeField] private CanvasGroup feedsCanvasGroup;

    public void SwitchToLoginPanel()
    {
        checkConnectionCanvasGroup.gameObject.SetActive(false);
        loginConnectionCanvasGroup.gameObject.SetActive(true);
        welcomeConnectionCanvasGroup.gameObject.SetActive(false);
        linksCanvasGroup.gameObject.SetActive(false);
        feedsCanvasGroup.gameObject.SetActive(false);

    }

    public void SwitchToWelcomePanel()
    {
        checkConnectionCanvasGroup.gameObject.SetActive(false);
        loginConnectionCanvasGroup.gameObject.SetActive(false);
        welcomeConnectionCanvasGroup.gameObject.SetActive(true);
        linksCanvasGroup.gameObject.SetActive(false);
        feedsCanvasGroup.gameObject.SetActive(false);
    }

    public void SwitchToLinksPanel()
    {
        checkConnectionCanvasGroup.gameObject.SetActive(false);
        loginConnectionCanvasGroup.gameObject.SetActive(false);
        welcomeConnectionCanvasGroup.gameObject.SetActive(false);
        linksCanvasGroup.gameObject.SetActive(true);
        feedsCanvasGroup.gameObject.SetActive(false);
    }

    public void SwitchToFeedsPanel()
    {
        checkConnectionCanvasGroup.gameObject.SetActive(false);
        loginConnectionCanvasGroup.gameObject.SetActive(false);
        welcomeConnectionCanvasGroup.gameObject.SetActive(false);
        linksCanvasGroup.gameObject.SetActive(false);
        feedsCanvasGroup.gameObject.SetActive(true);
    }
}
