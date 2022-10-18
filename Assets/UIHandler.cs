using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Walker walker;
    public Slider widthSlider, lengthSlider, heightSlider;
    public Text widthTxt, lengthTxt, heightTxt, failCount;
    public void wlh()
    {
        walker.width = (int)widthSlider.value;
        walker.length = (int)lengthSlider.value;
        walker.height = (int)heightSlider.value;
    }

    void Awake()
    {
        
        widthSlider.value = 3;
        lengthSlider.value = 3;
        heightSlider.value = 3;
    }

    void Update()
    {
        widthTxt.text = widthSlider.value.ToString();
        lengthTxt.text = lengthSlider.value.ToString();
        heightTxt.text = heightSlider.value.ToString();
        failCount.text = "Fail count: " + walker.failCount.ToString();
    }

    public void Restart()
    {
        walker.Restart();
    }
}
