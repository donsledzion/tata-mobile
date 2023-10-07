using UnityEngine;

public class FramerateFixer : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 120;
    }
}
