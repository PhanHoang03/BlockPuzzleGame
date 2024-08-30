using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearCtrl : MonoBehaviour
{
    [SerializeField] protected GridManager gridManager;
    [SerializeField] GameObject view;

    void Start()
    {
        this.SetUp();
    }

    public void Show() 
    {
        this.view.SetActive(true);
    }

    protected virtual void SetUp()
    {
        this.view.SetActive(false);
    }

    public void TryAgain()
    {
        this.gridManager.ResetGrid();
        //this.scoreCtrl.ResetValue();
        this.view.SetActive(false);
    }
}
