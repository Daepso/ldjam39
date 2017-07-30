using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControler : MonoBehaviour
{
    Color on = Color.gray;
    Color off = Color.white;
    Toggle toggle;

    // Use this for initialization
    void Awake () {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        ColorBlock cb = toggle.colors;
        cb.normalColor = isOn ? on : off;
        cb.highlightedColor = isOn ? on : off;
        toggle.colors = cb;
    }
}
