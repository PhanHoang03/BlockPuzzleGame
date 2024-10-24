using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCtrl : MonoBehaviour
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

    public void TryAgain()
    {
        this.view.SetActive(false);
        this.levelCtrl.ChooseLevel(this.levelCtrl.levelID);
    }
}
