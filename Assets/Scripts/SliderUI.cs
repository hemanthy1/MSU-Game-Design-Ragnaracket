using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    private Slider slider = null;
    private float actualVal;
    public float lerpSpeed = 0.5f;
    public bool startEmpty = false;

    private bool emptying = false;
    static float t = 0f;
    private float emptyTime = 5f;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (emptying)
        {
            slider.value = Mathf.Lerp(slider.maxValue, 0, t);
            t += Time.deltaTime / emptyTime;
            if (t >= 1f)
            {
                emptying = false;
            }
        }
        else if (slider.value != actualVal)
        {
            slider.value = Mathf.Lerp(slider.value, actualVal, lerpSpeed);
        }
    }

    public void SetMax(float newVal)
    {
        if (slider == null)
            slider = GetComponent<Slider>();
        slider.maxValue = newVal;
        if (!startEmpty)
        {
            slider.value = newVal;
            actualVal = newVal;
        }
        else
        {
            slider.value = 0;
            actualVal = 0;
        }
    }

    public void UpdateValue(float newVal)
    {
        if (!emptying)
            actualVal = newVal;
    }

    public void EmptyOverTime(float time)
    {
        emptying = true;
        emptyTime = time;
        actualVal = 0;
    }
}
