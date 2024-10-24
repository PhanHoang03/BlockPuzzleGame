using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearCtrl : MonoBehaviour
{
    [SerializeField] protected GridManager gridManager;
    [SerializeField] GameObject view;
    [SerializeField] LevelCtrl levelCtrl;

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

    public void Continue()
    {
        this.view.SetActive(false);
        this.levelCtrl.ChooseLevel(this.levelCtrl.levelID + 1);
    }
}
