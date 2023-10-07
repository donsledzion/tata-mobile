using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TopBarHider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    [SerializeField] RectTransform topBar;
    [SerializeField] float deltaFactor = 1f;

    private float pointerDownPositionY;
    private float pointerPositionY;
    private float deltaY;
    private float previousDelta;
    private bool isPointerDown = false;
    private float maxBarY;
    private float minBarY;
    private float onClickedPositionY;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        deltaY = 0;
        pointerDownPositionY = eventData.position.y;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (isPointerDown)
        {
            pointerPositionY = eventData.position.y;
            deltaY = pointerPositionY- pointerDownPositionY;
            
            topBar.position = new Vector3(
                topBar.position.x,
                Mathf.Clamp(topBar.position.y+deltaY*deltaFactor,minBarY, maxBarY),
                topBar.position.z
                );
            pointerDownPositionY = pointerPositionY;

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        StartCoroutine(CompleteBarSlide());
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        onClickedPositionY = topBar.position.y;
        minBarY = topBar.position.y;
        maxBarY = minBarY + topBar.sizeDelta.y;
    }

    IEnumerator CompleteBarSlide()
    {
        while (topBar.position.y != minBarY || topBar.position.y != maxBarY)
        {
            topBar.position = new Vector3(
                topBar.position.x,
                Mathf.Clamp(topBar.position.y + deltaY * deltaFactor, minBarY, maxBarY),
                topBar.position.z
                );
            yield return null;
        }
        deltaY = 0;
    }
}
