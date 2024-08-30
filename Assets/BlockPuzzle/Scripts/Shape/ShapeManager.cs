using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Analytics;

public class ShapeManager : MonoBehaviour
{
    [SerializeField] protected GridManager gridManager;
    [SerializeField] protected List<ShapeSO> shapeTypes;
    [SerializeField] protected List<Shape> shapeList;
    [SerializeField] protected GameOverCtrl gameOverCtrl;
    [SerializeField] protected GameClearCtrl gameClearCtrl;
    [SerializeField] protected LevelSO levelSO;
    private int numShape;
    private int totalShape;

    void Start()
    {
        //this.MakeShape();
    }

    public void SetUp()
    {
        this.numShape = -1;
        this.levelSO = null;
        foreach (Shape shape in this.shapeList)
        {
            shape.gameObject.SetActive(false);
        }
    }

    public void CheckAvailableShape()
    {
        foreach (Shape shape in this.shapeList)
        {
            if (shape.gameObject.activeSelf) return;
        }
        
        this.MakeShape(this.levelSO);
    }

    public void CheckGameOver()
    {
        bool check = true;
        foreach (Shape shape in this.shapeList)
        {
            if (!shape.gameObject.activeSelf) continue;
            check = false;
            if (this.gridManager.CheckFit(shape.CurShapeSO)) return;
        }

        if (check && this.numShape < this.totalShape) {
            if (!this.gridManager.CheckActivateSquare()) return;
        }

        this.gameOverCtrl.Show();
    }

    public void CheckWin()
    {
        foreach (Shape shape in this.shapeList)
        {
            if (shape.gameObject.activeSelf) return;
        }

        if (this.numShape < this.totalShape - 1) return;

        if (this.gridManager.CheckActivateSquare()) return;

        this.gameClearCtrl.Show();
    }

    public void MakeShape()
    {
        foreach (Shape shape in this.shapeList)
        {
            shape.gameObject.SetActive(true);
            int shapeIndex = Random.Range(0, shapeTypes.Count - 1);
            shape.CreateShape(shapeTypes[shapeIndex]);
        }
    }

    public void MakeShape (LevelSO level)
    {
        if (this.levelSO == null) 
        {
            this.levelSO = level;
            this.totalShape = level.shapeList.Count;
        }

        foreach (Shape shape in this.shapeList)
        {
            if (this.numShape + 1 >= level.shapeList.Count) return;
            this.numShape++;
            shape.gameObject.SetActive(true);
            shape.CreateShape(level.shapeList[this.numShape]);
        }
    }
}
