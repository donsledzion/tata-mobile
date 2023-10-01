using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CheckConnection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusTMP;
    [SerializeField] private GameObject checkingConnectionIcon;
    [SerializeField] private GameObject connectionSucceedIcon;
    [SerializeField] private GameObject noInternetIcon;
    [SerializeField] private GameObject noServiceIcon;
    [SerializeField] private Button reconnectButton;
    [SerializeField] private UnityEvent onConnectionSucceed;

    void Start()
    {
        CheckConnectionProcedure();
    }

    public void CheckConnectionProcedure()
    {
        gameObject.SetActive(true);
        StartCoroutine(CheckConnectionCor());
    }

    public void OnReconnectButton()
    {
        reconnectButton.interactable = false;
        StopAllCoroutines();
        StartCoroutine (CheckConnectionCor());
    }

    private IEnumerator CheckConnectionCor()
    {
        reconnectButton.onClick.AddListener(OnReconnectButton);
        reconnectButton.gameObject.SetActive(false);
        statusTMP.text = "Sprawdzanie po³¹czenia";
        ShowImage(checkingConnectionIcon);
        yield return new WaitForEndOfFrame();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            ShowImage(noInternetIcon);
            statusTMP.text = "Brak po³¹czenia z internetem";
            reconnectButton.gameObject.SetActive(true);
            reconnectButton.interactable = true;
        }
        else
        {
            ShowImage(connectionSucceedIcon);
            statusTMP.text = "Internet dostêpny";
            yield return CheckServiceAvailability();
        }

    }

    private IEnumerator CheckServiceAvailability()
    {
        UnityWebRequest URL = UnityWebRequest.Get(Consts.BaseAPI_URL + Consts.URI.Status);

        yield return URL.SendWebRequest();

        if (URL.result != UnityWebRequest.Result.Success)
        {
            statusTMP.text = "Serwer chwilowo niedostêpny";
            ShowImage(noServiceIcon);
            reconnectButton.gameObject.SetActive(true);
            reconnectButton.interactable = true;

        }
        else
        {
            reconnectButton.gameObject.SetActive(false);
            statusTMP.text = "Po³¹czono z serwerem aplikacji";
            ShowImage(connectionSucceedIcon);
            yield return new WaitForSeconds(1);
            onConnectionSucceed?.Invoke();

        }
    }

    private void HideImages()
    {
        checkingConnectionIcon.gameObject.SetActive(false);
        connectionSucceedIcon.gameObject.SetActive(false);
        noInternetIcon.gameObject.SetActive(false);
        noServiceIcon.gameObject.SetActive(false);
    }

    private void ShowImage(GameObject imageGameObject)
    {
        HideImages();
        imageGameObject.SetActive(true);
    }
}
