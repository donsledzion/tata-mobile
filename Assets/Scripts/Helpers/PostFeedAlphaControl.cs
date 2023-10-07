using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostFeedAlphaControl : MonoBehaviour
{
    [SerializeField] RectTransform myRectTransform;
    [SerializeField] CanvasGroup myCanvasGroup;
    float centerY = Screen.height / 2;

    [ContextMenu("Debug My Position")]
    public void DebugMyPosition()
    {
        float myY = transform.position.y;
        Debug.Log(Mathf.Abs(myY-centerY).ToString("0.0") + " from center");
    }

    private void Update()
    {
        float alpha = Mathf.Clamp(myRectTransform.sizeDelta.y / Mathf.Abs(myRectTransform.position.y - centerY), 0.0f, 1.0f);
        if (alpha < 0.5f)
            alpha = 0f;
        myCanvasGroup.alpha = alpha;
    }
}
