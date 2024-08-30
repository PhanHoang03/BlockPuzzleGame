using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(ShapeSO), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class ShapeEditor : Editor
{
    private ShapeSO shapeSO => target as ShapeSO;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        this.ClearBoard();
        EditorGUILayout.Space();

        this.CreateNewShape();
        EditorGUILayout.Space();

        if (this.shapeSO.board != null) 
        {
            this.DrawShapeTable();
        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(this.shapeSO);
            this.CheckNumSquare();
        }
    }

    private void CheckNumSquare ()
    {
        this.shapeSO.numSquare = 0;
        for (int row = 0; row < this.shapeSO.numRow; row++) 
        {
            for (int col = 0; col < this.shapeSO.numCol; col++)
            {
                if (this.shapeSO.board[row]._row[col] == false) continue; 
                this.shapeSO.numSquare++;
            }
        }
    }

    private void ClearBoard()
    {
        if (GUILayout.Button("Clear Board"))
        {
            this.shapeSO.Clear();
        }
    }

    private void CreateNewShape()
    {
        int rowTemp = this.shapeSO.numRow;
        int colTemp = this.shapeSO.numCol;

        this.shapeSO.numRow = EditorGUILayout.IntField("Rows", this.shapeSO.numRow);
        this.shapeSO.numCol = EditorGUILayout.IntField("Columns", this.shapeSO.numCol);

        if (this.shapeSO.numCol == colTemp && this.shapeSO.numRow == rowTemp) return;
        if (this.shapeSO.numCol == 0 || this.shapeSO.numRow == 0) return;

        this.shapeSO.CreateBoard();
    }

    private void DrawShapeTable()
    {
        var tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10, 10, 10, 10);
        tableStyle.margin.left = 32;

        var headerColStyle = new GUIStyle();
        headerColStyle.fixedWidth = 65;
        headerColStyle.alignment = TextAnchor.MiddleCenter;

        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;
        rowStyle.alignment = TextAnchor.MiddleCenter;

        var dataFieldStyle = new GUIStyle(EditorStyles.miniButtonMid);
        dataFieldStyle.normal.background = Texture2D.grayTexture;
        dataFieldStyle.onNormal.background = Texture2D.whiteTexture;

        for (int row = 0; row < this.shapeSO.numRow; row++)
        {
            EditorGUILayout.BeginHorizontal(headerColStyle);
            for (int col = 0; col < this.shapeSO.numCol; col++)
            {
                EditorGUILayout.BeginHorizontal(rowStyle);
                bool data = EditorGUILayout.Toggle(this.shapeSO.board[row]._row[col], dataFieldStyle);
                this.shapeSO.board[row]._row[col] = data;
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    public override bool HasPreviewGUI()
    {
        return true;
    }

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        ShapeSO shapeSO = (ShapeSO)target;

        if (shapeSO != null && shapeSO.board != null)
        {
            DrawShapePreview(r, shapeSO);
        }
    }

    private void DrawShapePreview(Rect r, ShapeSO shapeSO)
    {
        int numRow = shapeSO.numRow;
        int numCol = shapeSO.numCol;
        float cellSize = Mathf.Min(r.width / numCol, r.height / numRow);

        for (int row = 0; row < numRow; row++)
        {
            for (int col = 0; col < numCol; col++)
            {
                if (shapeSO.board[row]._row[col])
                {
                    Rect cellRect = new Rect(r.x + col * cellSize, r.y + row * cellSize, cellSize, cellSize);
                    EditorGUI.DrawRect(cellRect, Color.gray); // Draw the cell as a gray square
                }
            }
        }
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        ShapeSO shapeSO = (ShapeSO)target;

        if (shapeSO == null || shapeSO.board == null)
        {
            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }

        Texture2D previewTexture = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.clear;
        }

        previewTexture.SetPixels(pixels);

        int numRow = shapeSO.numRow;
        int numCol = shapeSO.numCol;
        float cellWidth = width / (float)numCol;
        float cellHeight = height / (float)numRow;

        for (int row = 0; row < numRow; row++)
        {
            for (int col = 0; col < numCol; col++)
            {
                if (shapeSO.board[row]._row[col])
                {
                    int startX = Mathf.FloorToInt(col * cellWidth);
                    int startY = Mathf.FloorToInt(row * cellHeight);
                    int endX = Mathf.CeilToInt((col + 1) * cellWidth);
                    int endY = Mathf.CeilToInt((row + 1) * cellHeight);

                    for (int y = startY; y < endY; y++)
                    {
                        for (int x = startX; x < endX; x++)
                        {
                            previewTexture.SetPixel(x, height - y - 1, Color.gray); // Set the color to gray
                        }
                    }
                }
            }
        }

        previewTexture.Apply();
        return previewTexture;
    }
}
