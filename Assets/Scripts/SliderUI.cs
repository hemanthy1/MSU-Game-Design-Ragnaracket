using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    private Slider slider;
    private float actualVal;
    public float lerpSpeed = 0.5f;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value != actualVal)
        {
            slider.value = Mathf.Lerp(slider.value, actualVal, lerpSpeed);
        }
    }

    public void SetMax(float newVal)
    {
        slider.maxValue = newVal;
        slider.value = newVal;
        actualVal = newVal;
    }

    public void UpdateValue(float newVal)
    {
        actualVal = newVal;
    }
}
