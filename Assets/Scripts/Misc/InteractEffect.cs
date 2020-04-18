using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractEffect : MonoBehaviour
{
    TextMeshPro txt;
    void Start()
    {
        txt = GetComponent<TextMeshPro>();
        txt.SetText(SysManager.keymap["상호작용"].ToString());
        txt.sortingLayerID = SortingLayer.NameToID("FX");
    }
}
