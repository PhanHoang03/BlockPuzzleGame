using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeCtrl : MonoBehaviour, IEndDragHandler
{
    [SerializeField] protected int maxPage = 3;
    protected int currentPage;
    protected Vector3 targetPos;
    [SerializeField] protected Vector3 pageStep = new Vector3(-1100f, 0f, 0f);
    [SerializeField] protected RectTransform characterPagesRect;
    protected Vector3 pos;
    [SerializeField] protected float tweenTime = 0.3f;
    [SerializeField] protected Ease tweenType = Ease.OutCubic;  // Updated to DOTween's Ease type
    private float dragThreshold;

    void Awake()
    {
        this.SetUp();
    }

    protected virtual void SetUp()
    {
        this.pos = characterPagesRect.localPosition;
        this.dragThreshold = Screen.width / 15;
        this.Reset();
    }

    public virtual void Reset()
    {
        Debug.Log(this.pos);
        this.currentPage = 1;
        this.targetPos = this.pos;
        this.characterPagesRect.localPosition = this.pos;
    }

    public virtual void Next()
    {
        if (this.currentPage < this.maxPage) 
        {
            this.currentPage++;
            this.targetPos += this.pageStep;
            this.MovePage();
        }
        else this.MovePage();
    }

    public virtual void Previous()
    {
        if (this.currentPage > 1) 
        {
            this.currentPage--;
            this.targetPos -= this.pageStep;
            this.MovePage();
        }
    }

    protected virtual void MovePage()
    {
        // Use DOTween's method to move the RectTransform
        this.characterPagesRect.DOLocalMove(this.targetPos, this.tweenTime).SetEase(this.tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(transform.name);
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > this.dragThreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x) this.Previous();
            else this.Next();
        }
        else
        {
            this.MovePage();
        }
    }
}
