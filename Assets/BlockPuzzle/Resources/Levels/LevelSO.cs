using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class LevelSO : ScriptableObject
{
    [System.Serializable]
    public class Row 
    {
        public bool[] _row;
        private int _size = 0;

        public Row(){}

        public Row (int size) 
        {
            this.CreateRow(size);
        }

        public void CreateRow (int size)
        {
            this._size = size;
            this._row = new bool[size];
            this.ClearRow();
        }

        public void ClearRow ()
        {
            for (int i = 0; i < this._size; i++) 
            {
                this._row[i] = false;
            }
        }
    }
    
    public int numRow = 9;
    public int numCol = 9;
    public int numSquare;
    public List<ShapeSO> shapeList;
    public Row[] board;

    public void Clear()
    {
        for (int i = 0; i < this.numRow; i++)
        {
            this.board[i].ClearRow();
        }
    }

    public void CreateBoard()
    {
        this.board = new Row[this.numRow];
        for (int i = 0; i < this.numRow; i++)
        {
            this.board[i] = new Row(this.numCol);
        }
        this.Clear();
    }
}
