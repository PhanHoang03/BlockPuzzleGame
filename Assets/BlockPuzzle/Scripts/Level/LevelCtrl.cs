using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCtrl : MonoBehaviour
{
    [SerializeField] protected GridManager gridManager;
    [SerializeField] protected ShapeManager shapeManager;
    [SerializeField] protected List<LevelSO> levels;

    public void ChooseLevel (int id)
    {
        this.shapeManager.SetUp();
        this.gridManager.LoadLevel(levels[id]);
        this.shapeManager.MakeShape(levels[id]);
        transform.Find("ViewHolder").gameObject.SetActive(false);
    }
}
