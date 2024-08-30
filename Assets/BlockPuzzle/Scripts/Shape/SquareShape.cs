using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SquareShape : MonoBehaviour
{
    [SerializeField] protected Image squareImage;
    [SerializeField] protected Image squareRed;

    void Start()
    {
        this.SetUp();
    }

    protected virtual void SetUp()
    {
        this.squareImage.gameObject.SetActive(false); 
        this.squareRed.gameObject.SetActive(false); 
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.transform.GetComponent<GridSquare>() == null) return;
        if (collision.transform.GetComponent<GridSquare>().CanBePlaced) return;
        this.squareRed.gameObject.SetActive(true);
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.transform.GetComponent<GridSquare>() == null) return;
        if (collision.transform.GetComponent<GridSquare>().CanBePlaced) return;
        this.squareRed.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        this.squareRed.gameObject.SetActive(false);
    }
}
