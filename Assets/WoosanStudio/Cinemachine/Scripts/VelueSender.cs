using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelueSender : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();       
    }

    public void SetValue(Slider slider)
    {
        text.text = slider.value.ToString();
    }
}
