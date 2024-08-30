using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    [SerializeField] protected Image normalImage;
    [SerializeField] protected Image hoverImage;
    [SerializeField] protected Image activeImage;
    [SerializeField] protected List<Sprite> _normalImages;
    [SerializeField] protected bool canBePlaced = true;
    public bool CanBePlaced => canBePlaced;
    [SerializeField] protected bool selected = false;
    public bool Selected => selected;

    void Start()
    {
        this.SetUp();
    }

    public void SetUp()
    {
        this.hoverImage.gameObject.SetActive(false);
        this.activeImage.gameObject.SetActive(false);
        this.selected = false;
        this.canBePlaced = true;
    }

    public void ActivateSquare()
    {
        this.hoverImage.gameObject.SetActive(false);
        this.activeImage.gameObject.SetActive(true);
        this.canBePlaced = false;
    }

    public void DeactivateSquare()
    {
        this.hoverImage.gameObject.SetActive(false);
        this.activeImage.gameObject.SetActive(false);
        this.canBePlaced = true;
    }

    public virtual void SetImage(int mode)
    {
        this.normalImage.GetComponent<Image>().sprite = _normalImages[mode];
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        this.hoverImage.gameObject.SetActive(true);
        this.selected = true;
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        this.hoverImage.gameObject.SetActive(true);
        this.selected = true;
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        this.hoverImage.gameObject.SetActive(false);
        this.selected = false;
    }
}
