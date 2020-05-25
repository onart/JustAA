using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualSave : MonoBehaviour
{
    public Toggle sw;

    void Start()
    {
        if (PlayerPrefs.GetInt(name, 1) == 0) {
            gameObject.SetActive(false);
            sw.isOn = false;
        }
    }

    public void ToggleVisual(bool state)
    {
        int sav = 0;
        if (state) sav = 1;
        PlayerPrefs.SetInt(name, sav);
        PlayerPrefs.Save();
        gameObject.SetActive(state);
    }
}
