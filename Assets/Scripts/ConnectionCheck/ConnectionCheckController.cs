using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConnectionCheckController : MonoBehaviour
{
    [SerializeField] private CheckingInternetAccessState checkingInternetAccessState;
    [SerializeField] private CheckingServiceAvailabilityState checkingServiceAvailabilityState;
    [SerializeField] private CheckingConnectionDoneState checkingConnectionDoneState;

    [SerializeField] private Button reconnectButton;
    [SerializeField] private TextMeshProUGUI statusTMP;
    [SerializeField] private UnityEvent onConnected;


    private ConnectionState connectionState;

    public ConnectionState ConnectionState => connectionState;
    public UnityEvent OnConnected => onConnected;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        reconnectButton.onClick.AddListener(() =>
        {
            reconnectButton.interactable = false;
            ChangeState(ConnectionCheckState.CheckingInternetAccess);
        });
        SetReconnectButton(false, false);
        ChangeState(ConnectionCheckState.CheckingInternetAccess);
    }

    public void ChangeState(ConnectionCheckState state)
    {
        switch (state)
        {
            case ConnectionCheckState.CheckingInternetAccess:
                Debug.Log("ConnectionCheckController: Changing state to CheckingInternetAccessState");
                checkingInternetAccessState.RunState();
                break;
            case ConnectionCheckState.CheckingServiceState:
                Debug.Log("ConnectionCheckController: Changing state to CheckingServiceAvailabilityState");
                checkingServiceAvailabilityState.RunState();
                break;
            case ConnectionCheckState.CheckingDone:
                Debug.Log("ConnectionCheckController: Changing state to CheckingConnectionDoneState");
                checkingConnectionDoneState.RunState();
                break;
            default:
                Debug.LogWarning("Unknown state!");
                break;
        }
    }

    internal void SetReconnectButton(bool visible, bool interactible)
    {
        reconnectButton.gameObject.SetActive(visible);
        reconnectButton.interactable = interactible;
    }

    internal void SetStatusText(string statusText)
    {
        statusTMP.text = statusText;
    }

    internal void SetConnectionState(ConnectionState connectionState)
    {
        this.connectionState = connectionState;
    }
}
