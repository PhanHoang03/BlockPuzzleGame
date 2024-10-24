using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonCtrl : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    [SerializeField] ShapeManager shapeManager;
    [SerializeField] GameObject levels;
    
    public void BackToMenu()
    {
        this.gridManager.ResetGrid();
        this.shapeManager.SetUp();
        this.levels.transform.Find("ViewHolder").gameObject.SetActive(true);
    }
}
