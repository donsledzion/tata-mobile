using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ExitButton : MonoBehaviour
{
    [SerializeField] UnityEvent onExitClicked;
    private int click = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            click++;
            StartCoroutine(ClickTime());

            if (click > 1)
            {
                Application.Quit();
            }
            else
            {
                onExitClicked?.Invoke();
            }
        }
    }
    IEnumerator ClickTime()
    {
        yield return new WaitForSeconds(0.5f);
        click = 0;
    }
}
