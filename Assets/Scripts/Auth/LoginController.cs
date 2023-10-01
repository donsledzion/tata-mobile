using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Events;
using Kidbook.Models;

public class LoginController : MonoBehaviour
{
    [SerializeField] private string autoFillLogin;
    [SerializeField] private string autoFillPassword;



    [SerializeField] private TMP_InputField userEmail;
    [SerializeField] private TMP_InputField userPassword;
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI errorMessages;
    [SerializeField] private GameObject loadingImage;

    [SerializeField] UnityEvent onLoginSucceed;
    [SerializeField] UnityEvent onTokenIsGood;
    [SerializeField] UnityEvent onNeedToLogin;
    private WWWForm loginForm;
    public void OnLoginButtonClicked()
    {
        StopAllCoroutines();
        loginButton.interactable = false;
        Login();
    }



    private void Start()
    {
        userEmail.text = autoFillLogin;
        userPassword.text = autoFillPassword;
    }

    public void Login()
    {
        StartCoroutine(LoginCor());
    }

    private IEnumerator LoginCor()
    {
        loginForm = new();
        loginForm.AddField("email",userEmail.text);
        loginForm.AddField("password", userPassword.text);
        loginForm.AddField("device", SystemInfo.deviceName + "'s" + SystemInfo.deviceModel);

        UnityWebRequest request = UnityWebRequest.Post(Consts.BaseAPI_URL + Consts.URI.Login,loginForm);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log(request.downloadHandler.text);
            loginButton.interactable = true;
        }
        else
        {
            loginButton.interactable = true;

            string plainResponse = request.downloadHandler.text;
            Debug.Log(plainResponse);
            User loggedInUser = User.CreateFromJSON(plainResponse) as User;

            Debug.Log("ObjectResponse->Token: " + loggedInUser.token);
            PlayerPrefs.SetString("RememberedApiToken",loggedInUser.token);
            onLoginSucceed?.Invoke();
        }
    }
}
