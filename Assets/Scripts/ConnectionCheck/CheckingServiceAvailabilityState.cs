using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CheckingServiceAvailabilityState : ConnectionCheckBaseState
{
    public override void RunState()
    {
        base.RunState();
        StartCoroutine(CheckServiceAvailability());
    }

    public override void StopState()
    {
        base.StopState();
    }

    private IEnumerator CheckServiceAvailability()
    {
        UnityWebRequest request = UnityWebRequest.Get(Consts.BaseAPI_URL + Consts.URI.Status);
        request.SetRequestHeader("Accept", "application/json");
        yield return request.SendWebRequest();


        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
            connectionCheckController.SetConnectionState(ConnectionState.NoService);
        }
        else
        {
            connectionCheckController.SetConnectionState(ConnectionState.Connected);
            Debug.Log(request.downloadHandler.text);

        }
        connectionCheckController.ChangeState(ConnectionCheckState.CheckingDone);
        StopState();
    }
}
