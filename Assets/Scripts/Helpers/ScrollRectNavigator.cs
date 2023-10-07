using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectNavigator : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private ScrollRect scrollRect;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();        
    }

    [ContextMenu("Back To Top")]
    public void BackToTop(bool instant = false)
    {
        if (instant)
            scrollRect.normalizedPosition = Vector3.one;
        else
            StartCoroutine(BackToTopCor());
    }

    public IEnumerator BackToTopCor()
    {
        while (scrollRect.normalizedPosition.y < 1f)
        {
            scrollRect.normalizedPosition = new Vector2(
                scrollRect.normalizedPosition.x,
                scrollRect.normalizedPosition.y + scrollSpeed * Time.deltaTime);
            yield return null;
        }
    }

}
