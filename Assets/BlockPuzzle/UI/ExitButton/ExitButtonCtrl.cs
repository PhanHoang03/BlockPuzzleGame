using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonCtrl : MonoBehaviour
{
    [SerializeField] protected GameObject levelSelect;
    [SerializeField] protected GameObject currentWindow;

    public void Back()
    {
        this.currentWindow.SetActive(false);
        this.levelSelect.SetActive(true);
    }
}
