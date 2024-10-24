using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShapeSwipeCtrl : SwipeCtrl
{
    [SerializeField] List<GameObject> pages;
    public void MaxPage(int numShape)
    {
        this.maxPage = (numShape + 2) / 3;
        Debug.Log(numShape);
        Debug.Log(this.maxPage);
    }
}
