using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingInternetAccessState : ConnectionCheckBaseState
{
    public override void RunState()
    {
        base.RunState();
        StartCoroutine(CheckConnectionCor());
    }

    public override void StopState()
    {
        base.StopState();
    }

    private IEnumerator CheckConnectionCor()
    {
        yield return new WaitForEndOfFrame();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            connectionCheckController.SetConnectionState(ConnectionState.NoInternet);
            connectionCheckController.ChangeState(ConnectionCheckState.CheckingDone);
        }
        else
        {
            connectionCheckController.SetConnectionState(ConnectionState.InternetAvailable);
            connectionCheckController.ChangeState(ConnectionCheckState.CheckingServiceState);
        }
        StopState();

    }
}
