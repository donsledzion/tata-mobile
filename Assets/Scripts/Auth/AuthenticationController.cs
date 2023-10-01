using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class AuthenticationController : MonoBehaviour
{
    [SerializeField] ScreenSwitcher screenSwitcher;
    [SerializeField] LoginController loginController;
    [SerializeField] UnityEvent onAuthenticated;
    public void TryToAuthenticate()
    {
        if (PlayerPrefs.HasKey(Consts.TokenKey))
            CheckToken();
        else
            NeedToLogin();
    }

    private void NeedToLogin()
    {
        screenSwitcher.SwitchToLoginPanel();
    }
    private void CheckToken()
    {
        StartCoroutine(CheckTokenCor());
    }

    internal IEnumerator CheckTokenCor()
    {
        UnityWebRequest request = UnityWebRequest.Get(Consts.BaseAPI_URL + Consts.URI.CheckToken);
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString(Consts.TokenKey));
        request.SetRequestHeader("Accept", "application/json");
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            PlayerPrefs.DeleteKey(Consts.TokenKey);
            NeedToLogin();
        }
        else
        {
            Debug.Log("Response Code: " + request.responseCode);
            onAuthenticated?.Invoke();
        }
        Debug.Log(request.downloadHandler.text);
    }
}
