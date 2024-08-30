using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonCtrl : MonoBehaviour
{
    [SerializeField] protected GameObject levelSelect;

    public void Back()
    {
        this.levelSelect.SetActive(true);
    }
}
