using UnityEngine;
using UnityEngine.UI;

public abstract class ConnectionCheckBaseState : MonoBehaviour
{
    [SerializeField] protected string statusText;
    [SerializeField] protected GameObject statusIcon;
    protected ConnectionCheckController connectionCheckController;

    protected virtual void Start()
    {
        connectionCheckController = GetComponent<ConnectionCheckController>();    
    }

    public virtual void RunState()
    {
        connectionCheckController.SetStatusText(statusText);
        statusIcon.SetActive(true); 
    }

    public virtual void StopState()
    {
        statusIcon.SetActive(false);
        StopAllCoroutines();
    }
}

public enum ConnectionCheckState
{
    CheckingInternetAccess,
    CheckingServiceState,
    CheckingDone
}

public enum ConnectionState
{
    Checking,
    InternetAvailable,
    NoInternet,
    NoService,
    Connected
}