using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeCtrl : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public RectTransform scrollViewContent; // Assign the content RectTransform of your ScrollView
    public float swipeDuration = 0.5f; // Duration of the swipe animation
    public float thresholdSwipeDistance = 100f; // Minimum swipe distance to trigger a page change

    private Vector2 startDragPosition;

    void Start()
    {
        // Ensure DOTween is initialized
        DOTween.Init();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startDragPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 endDragPosition = eventData.position;
        Vector2 dragDelta = endDragPosition - startDragPosition;

        if (Mathf.Abs(dragDelta.x) >= thresholdSwipeDistance)
        {
            // Determine the direction of the swipe
            if (dragDelta.x > 0)
            {
                // Swipe to the right
                ScrollToPage(-1);
            }
            else
            {
                // Swipe to the left
                ScrollToPage(1);
            }
        }
    }

    void ScrollToPage(int direction)
    {
        // Get the width of the viewport to determine the scrolling distance
        float viewportWidth = ((RectTransform)scrollViewContent.parent).rect.width / 3;

        // Calculate the target position
        Vector2 targetPosition = scrollViewContent.anchoredPosition;
        targetPosition.x += viewportWidth * direction;

        // Clamp the position to ensure it doesn't go out of bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, -viewportWidth * (scrollViewContent.childCount - 1), 0);

        // Smoothly move the content to the target position
        scrollViewContent.DOAnchorPos(targetPosition, swipeDuration).SetEase(Ease.OutCubic);
    }
}
