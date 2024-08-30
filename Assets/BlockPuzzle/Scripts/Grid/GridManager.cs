using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] protected int columns = 0;
    [SerializeField] protected int rows = 0;
    [SerializeField] protected GameObject gridSquare;
    [SerializeField] protected Vector2 startPosition = new Vector2(0f, 0f);
    [SerializeField] protected float squareScale = 0.5f;
    [SerializeField] protected float squareOffset = 0.0f;

    //[SerializeField] protected ScoreCtrl scoreCtrl;

    private Vector2 _offset = new Vector2(0f, 0f);
    private List<GameObject> _gridSquares = new List<GameObject>();

    void Start()
    {
        this.CreateGrid();
    }

    public void LoadLevel (LevelSO level)
    {   
        int squareIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                bool state = level.board[row]._row[col];
                int _index = this.GetBigSquareIndex(row, col);
                this._gridSquares[squareIndex].GetComponent<GridSquare>().SetUp();
                if (state == true)
                {
                    this._gridSquares[squareIndex].GetComponent<GridSquare>().ActivateSquare();
                }
                squareIndex++;
            }
        }
    }

    public void ResetGrid()
    {
        int squareIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int _index = this.GetBigSquareIndex(row, col);
                this._gridSquares[squareIndex].GetComponent<GridSquare>().SetUp();
                squareIndex++;
            }
        }
    }

    public bool CheckActivateSquare()
    {
        int squareIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if (!this._gridSquares[squareIndex].GetComponent<GridSquare>().CanBePlaced) return true;
                squareIndex++;
            }
        }

        return false;
    }

    protected virtual void CreateGrid()
    {
        this.SqpawnGridSquares();
        this.SetGridSquaresPosition();
    }

    protected virtual void SqpawnGridSquares()
    {
        int squareIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int _index = this.GetBigSquareIndex(row, col);
                this._gridSquares.Add(Instantiate(gridSquare) as GameObject);
                this._gridSquares[squareIndex].transform.SetParent(this.transform);
                this._gridSquares[squareIndex].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                this._gridSquares[squareIndex].GetComponent<GridSquare>().SetUp();
                this._gridSquares[squareIndex].GetComponent<GridSquare>().SetImage(_index % 2 == 0 ? 0 : 1);
                squareIndex++;
            }
        }
    }

    protected virtual void SetGridSquaresPosition()
    {
        int rowNum = 0;
        int colNum = 0;

        var squareRect = _gridSquares[0].GetComponent<RectTransform>();

        this._offset.x = squareRect.rect.width * squareRect.transform.localScale.x + this.squareOffset; 
        this._offset.y = squareRect.rect.height * squareRect.transform.localScale.y + this.squareOffset; 

        foreach (GameObject square in _gridSquares)
        {
            if (colNum == columns)
            {
                colNum = 0;
                rowNum++;
            }

            var posX = this.startPosition.x + this._offset.x * colNum;
            var posY = this.startPosition.y + this._offset.y * rowNum;

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, -posY);
            square.GetComponent<RectTransform>().localPosition = new Vector3(posX, -posY, 0f);
            colNum++;
        }
    }

    public bool CheckOverlap()
    {
        foreach(GameObject square in _gridSquares)
        {
            GridSquare gSquare = square.GetComponent<GridSquare>();
            if (gSquare.Selected && !gSquare.CanBePlaced) return true;
        }
        return false;
    }

    public bool CheckOutside(int numSquare)
    {
        int numSelected = 0;
        foreach(GameObject square in _gridSquares)
        {
            GridSquare gSquare = square.GetComponent<GridSquare>();
            if (!gSquare.Selected) continue;
            numSelected++;
        }
        return !(numSelected == numSquare);
    }

    public int GetBigSquareIndex(int row, int col)
    {
        int _index = (int)(row / 3) * 3 + (int)(col / 3);
        return _index;
    }

    public int GetSquareIndex(int row, int col)
    {
        int _index = row * columns + col;
        return _index;
    }

    protected virtual void CheckScoring()
    {
        int[] checkRow = new int[15];
        int[] checkCol = new int[15];
        int[] checkSquare = new int[15];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int _index = this.GetSquareIndex(row, col);
                GridSquare gSquare = _gridSquares[_index].GetComponent<GridSquare>();
                if (!gSquare.CanBePlaced) 
                {
                    checkRow[row]++;
                    checkCol[col]++;
                }
            }
        }

        for (int i = 0; i < rows; i++)
        {
            if (checkRow[i] == rows) 
            {
                this.RowClear(i);
                //this.scoreCtrl.AddScore(50);
            }
            if (checkCol[i] == columns) 
            {
                this.ColClear(i);
                //this.scoreCtrl.AddScore(50);
            }
        }
    }

    protected virtual void RowClear(int row)
    {
        for (int col = 0; col < columns; col++)
        {
            this.SquareClear(row, col);
        }
    }

    protected virtual void ColClear(int col)
    {
        for (int row = 0; row < rows; row++)
        {
            this.SquareClear(row, col);
        }
    }

    protected virtual void BigSquareClear(int squareIndex)
    {
        int row = (int)(squareIndex / 3) * 3; 
        int col = (int)(squareIndex % 3) * 3;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                 this.SquareClear(row + i, col + j);
            }
        }
    }

    protected virtual void SquareClear(int row, int col)
    {
        int _index = this.GetSquareIndex(row, col);
        _gridSquares[_index].GetComponent<GridSquare>().DeactivateSquare();
    }

    public void PlaceSquare()
    {
        int squareIndex = -1;
        foreach(GameObject square in _gridSquares)
        {
            squareIndex++;
            GridSquare gSquare = square.GetComponent<GridSquare>();
            if (!gSquare.CanBePlaced) continue;
            if (!gSquare.Selected) continue;
            gSquare.ActivateSquare();
        }

        this.CheckScoring();
        
    }

    public bool CheckFit(ShapeSO shapeSO)
    {
        Debug.Log(shapeSO.name);
        for (int row = 0; row < rows - shapeSO.numRow; row++)
        {
            for (int col = 0; col < columns - shapeSO.numCol; col++)
            {
                if (CheckFitSpace(row, col, shapeSO)) return true;
            }
        }
        return false;
    }

    protected bool CheckFitSpace(int r, int c, ShapeSO shapeSO)
    {
        for (int row = r; row < r + shapeSO.numRow; row++)
        {
            for (int col = c; col < c + shapeSO.numCol; col++)
            {
                if (shapeSO.board[row - r]._row[col - c] == false) continue;
                int _index = GetSquareIndex(row, col);
                GridSquare gSquare = this._gridSquares[_index].GetComponent<GridSquare>();
                if (!gSquare.CanBePlaced) 
                {
                    return false;
                }
            }
        }
        return true;
    }
}
