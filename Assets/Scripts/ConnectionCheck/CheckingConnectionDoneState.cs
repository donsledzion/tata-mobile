using UnityEngine;

public class CheckingConnectionDoneState : ConnectionCheckBaseState
{
    [SerializeField] private string messageConnected = "Po³¹czono pomyœlnie";
    [SerializeField] private GameObject iconConnected;
    [SerializeField] private string messageNoService = "Serwer aplikacji niedostêpny";
    [SerializeField] private GameObject iconNoService;
    [SerializeField] private string messageNoInternet = "Brak dostêpu do internetu";
    [SerializeField] private GameObject iconNoInternet;
    public override void RunState()
    {
        base.RunState();
        ConnectionState connectionState = connectionCheckController.ConnectionState;

        Debug.Log("Connection sate: " + connectionState.ToString());
        switch (connectionState)
        {
            case ConnectionState.Connected:
                ShowIcon(iconConnected);
                connectionCheckController.SetStatusText(messageConnected);
                connectionCheckController.SetReconnectButton(false, false);
                connectionCheckController.OnConnected?.Invoke();
                break;
            case ConnectionState.NoService:
                ShowIcon(iconNoService);
                connectionCheckController.SetStatusText(messageNoService);
                connectionCheckController.SetReconnectButton(true, true);
                break;
            case ConnectionState.NoInternet:
                ShowIcon(iconNoInternet);
                connectionCheckController.SetStatusText(messageNoInternet);
                connectionCheckController.SetReconnectButton(true, true);
                break;
            default:
                break;
        }
    }

    public override void StopState()
    {
        base.StopState();
    }

    private void ShowIcon(GameObject iconGO)
    {
        HideIcons();
        iconGO.SetActive(true);
    }

    private void HideIcons()
    {
        statusIcon.SetActive(false);
        iconConnected.SetActive(false);
        iconNoService.SetActive(false);
        iconNoInternet.SetActive(false);
    }

}
