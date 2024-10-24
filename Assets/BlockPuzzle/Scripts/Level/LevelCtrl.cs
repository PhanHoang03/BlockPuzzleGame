using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCtrl : MonoBehaviour
{
    [SerializeField] protected GridManager gridManager;
    [SerializeField] protected ShapeManager shapeManager;
    [SerializeField] protected ShapeSwipeCtrl shapeSwipeCtrl;
    [SerializeField] protected List<LevelSO> levels;
    public int levelID;

    private void Start()
    {
        //this.SetUp();
    }

    private  void SetUp()
    {
        Transform source = transform.Find("ViewHolder");

        source = source.Find("ScrollView");

        source = source.Find("View");

        foreach (Transform page in source) 
        {
            foreach (Transform level in page)
            {
                level.GetComponent<Image>().sprite = Resources.Load<Sprite>("win_lose/win_lose/popup");
                level.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(255f, 255f, 255f, 0f);
            }
        }
    }

    public void ChooseLevel (int id)
    {
        this.levelID = id;
        this.shapeManager.SetUp();
        this.gridManager.LoadLevel(levels[id]);
        this.shapeManager.MakeShape(levels[id]);
        this.shapeSwipeCtrl.MaxPage(levels[id].shapeList.Count);
        this.shapeSwipeCtrl.Reset();
        transform.Find("ViewHolder").gameObject.SetActive(false);
    }
}
