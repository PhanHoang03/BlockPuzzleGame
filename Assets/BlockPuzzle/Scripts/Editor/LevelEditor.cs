using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.VisualScripting;

[CustomEditor(typeof(LevelSO), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class LevelEditor : Editor
{
    private LevelSO levelSO => target as LevelSO;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        this.ClearBoard();
        EditorGUILayout.Space();

        this.CreateNewShape();
        EditorGUILayout.Space();

        this.DisplayShapeList();
        EditorGUILayout.Space();

        if (this.levelSO.board != null) 
        {
            this.DrawShapeTable();
        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(this.levelSO);
        }
    }

    private void ClearBoard()
    {
        if (GUILayout.Button("Clear Board"))
        {
            this.levelSO.Clear();
        }
    }

    private void CreateNewShape()
    {
        int rowTemp = this.levelSO.numRow;
        int colTemp = this.levelSO.numCol;

        this.levelSO.numRow = EditorGUILayout.IntField("Rows", this.levelSO.numRow);
        this.levelSO.numCol = EditorGUILayout.IntField("Columns", this.levelSO.numCol);

        if (this.levelSO.numCol == colTemp && this.levelSO.numRow == rowTemp) return;
        if (this.levelSO.numCol == 0 || this.levelSO.numRow == 0) return;
        this.levelSO.CreateBoard();
    }

    private void DrawShapeTable()
    {
        var tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10, 10, 10, 10);
        tableStyle.margin.left = 32;

        var headerColStyle = new GUIStyle
        {
            fixedWidth = 65,
            alignment = TextAnchor.MiddleCenter
        };

        var rowStyle = new GUIStyle
        {
            fixedHeight = 25,
            alignment = TextAnchor.MiddleCenter
        };

        var dataFieldStyle = new GUIStyle(EditorStyles.miniButtonMid);
        dataFieldStyle.normal.background = Texture2D.grayTexture;
        dataFieldStyle.onNormal.background = Texture2D.whiteTexture;

        for (int row = 0; row < this.levelSO.numRow; row++)
        {
            EditorGUILayout.BeginHorizontal(headerColStyle);
            for (int col = 0; col < this.levelSO.numCol; col++)
            {
                EditorGUILayout.BeginHorizontal(rowStyle);
                bool data = EditorGUILayout.Toggle(this.levelSO.board[row]._row[col], dataFieldStyle);
                this.levelSO.board[row]._row[col] = data;
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void DisplayShapeList()
    {
        EditorGUILayout.LabelField("Shapes", EditorStyles.boldLabel);

        if (this.levelSO.shapeList == null)
        {
            this.levelSO.shapeList = new List<ShapeSO>();
        }

        // Display each ShapeSO in the list
        for (int i = 0; i < this.levelSO.shapeList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            this.levelSO.shapeList[i] = (ShapeSO)EditorGUILayout.ObjectField($"Shape {i + 1}", this.levelSO.shapeList[i], typeof(ShapeSO), false);
            if (GUILayout.Button("Remove"))
            {
                this.levelSO.shapeList.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }

        // Button to add a new ShapeSO to the list
        if (GUILayout.Button("Add Shape"))
        {
            this.levelSO.shapeList.Add(null);
        }
    }
}
