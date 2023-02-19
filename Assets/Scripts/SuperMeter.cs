using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperMeter : MonoBehaviour
{
    private GameObject indicator;
    private Slider slider;
    private bool full = false;

    // Start is called before the first frame update
    void Start()
    {
        indicator = transform.Find("FullIndicator").gameObject;
        ToggleIndicator(false);
        slider = GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        if (!full && slider.value == slider.maxValue)
            ToggleIndicator(true);
        else if (full && slider.value < slider.maxValue)
            ToggleIndicator(false);
    }

    public void ToggleIndicator(bool val)
    {
        indicator.SetActive(val);
        full = val;
    }
}
