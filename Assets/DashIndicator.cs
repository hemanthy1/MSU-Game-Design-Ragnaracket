using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashIndicator : MonoBehaviour
{
    private float max = 1f;
    private Slider slider;
    private Image img;

    private Color32 activeColor = new Color32(200, 200, 200, 255);
    private Color32 inactiveColor = new Color32(200, 200, 200, 50);

    void Start()
    {
        slider = transform.Find("Recharge").GetComponent<Slider>();
        img = GetComponent<Image>();
    }

    public void SetMax(float val)
    {
        max = val;
    }

    public void SetValue(float val)
    {
        slider.value = val / max;
        if (val >= max)
            img.color = activeColor;
    }

    public void Use()
    {
        slider.value = 0;
        img.color = inactiveColor;
    }
}
