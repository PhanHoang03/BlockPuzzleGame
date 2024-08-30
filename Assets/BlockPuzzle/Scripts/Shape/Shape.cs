using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] protected float squareOffset = 0.0f;
    private Vector2 _offset = new Vector2(0f, 0f);
    [SerializeField] protected GameObject squareImage;
    [SerializeField] protected ShapeSO curShapeSO;
    public ShapeSO CurShapeSO => curShapeSO;
    private List<GameObject> curShape = new List<GameObject>();
    
    public void CreateShape (ShapeSO shapeSO)
    {
        this.curShapeSO = shapeSO;

        var squareRect = squareImage.GetComponent<RectTransform>();

        this._offset.x = squareRect.rect.width * squareRect.transform.localScale.x + squareOffset; 
        this._offset.y = squareRect.rect.height * squareRect.transform.localScale.y + squareOffset;

        int index = 0;
        int _size = curShape.Count;
        for (int row = 0; row < this.curShapeSO.numRow; row++) 
        {
            for (int col = 0; col < this.curShapeSO.numCol; col++)
            {
                if (this.curShapeSO.board[row]._row[col] == false) continue; 

                if (_size == 0) this.curShape.Add(Instantiate(squareImage, transform) as GameObject);
                else _size--;

                var posX = this._offset.x * col;
                var posY = this._offset.y * row;

                this.curShape[index].gameObject.SetActive(true);
                this.curShape[index].GetComponent<RectTransform>().localPosition = new Vector3(posX, -posY, 0f);
                index++;
            }
        }

        for (int i = index; i < this.curShape.Count; i++)
        {
            this.curShape[i].gameObject.SetActive(false);
        }
    }
}
