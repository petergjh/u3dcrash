using UnityEngine;
using UnityEngine.UI;

public class TestSlider : MonoBehaviour
{
    float SliderValue = 0;
    public Text text;
    void Update()
    {
        SliderValue = transform.GetComponentInChildren<Slider>().value;
        text.text = SliderValue * 100 + "%";
        
    }
}
