using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LogoutController : MonoBehaviour
{
    [SerializeField] private Button logoutButton;
    [SerializeField] private UnityEvent onLoggedOut;

    public void OnLogoutButtonClicked()
    {
        StartCoroutine(LogoutCor());
    }

    private IEnumerator LogoutCor()
    {
        logoutButton.interactable = false;
        UnityWebRequest request = UnityWebRequest.Post(Consts.BaseAPI_URL + Consts.URI.Logout, new WWWForm());
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("RememberedApiToken"));
        //request.SetRequestHeader("Accept", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            logoutButton.interactable = true;
        }
        else
        {
            logoutButton.interactable = true;
            PlayerPrefs.DeleteKey("RememberedApiToken");
            Debug.Log("User has been logged out");
            yield return new WaitForEndOfFrame();
            onLoggedOut?.Invoke();
        }
        Debug.Log(request.downloadHandler.text);
    }
}
