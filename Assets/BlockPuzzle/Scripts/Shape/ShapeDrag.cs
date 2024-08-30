using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapeDrag : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] protected GridManager gridManager;
    [SerializeField] protected ShapeManager shapeManager;
    [SerializeField] protected ShapeSO shapeSO;
    [SerializeField] protected Vector3 scaleUp;
    [SerializeField] protected Vector2 originalPosition;
    [SerializeField] protected Canvas _canvas;
    private Vector3 orginalScale;
    private RectTransform _rect;
    protected bool isDraggable;
    private Vector2 dragOffset;

    void Awake()
    {
        this.SetUp();
    }

    protected virtual void SetUp()
    {
        this._rect = this.GetComponent<RectTransform>();
        this.orginalScale = this._rect.localScale;
        this.originalPosition = this._rect.anchoredPosition;
        this.isDraggable = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out this.dragOffset
        );

        this.dragOffset = (Vector2)_rect.localPosition - this.dragOffset;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out localPoint
        );

        _rect.localPosition = localPoint + dragOffset;
        this.shapeSO = transform.GetComponent<Shape>().CurShapeSO;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.gridManager.CheckOverlap()) return;
        if (this.gridManager.CheckOutside(this.shapeSO.numSquare)) return;
        this.gridManager.PlaceSquare();
        this.DisableShape();
        this.shapeManager.CheckAvailableShape();
        this.shapeManager.CheckGameOver();
        this.shapeManager.CheckWin();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this._rect.localScale = this.scaleUp;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this._rect.localScale = this.orginalScale;
        this._rect.anchoredPosition = this.originalPosition;
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        
    }

    protected virtual void DisableShape()
    {
        Debug.Log(transform.name);
        transform.gameObject.SetActive(false);
    }
}
