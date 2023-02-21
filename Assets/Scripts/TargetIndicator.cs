using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    public bool visualSetting = false;
    private Image visual;
    private bool targetActive = false;

    // Start is called before the first frame update
    void Start()
    {
        visual = transform.Find("Canvas").Find("Image").GetComponent<Image>();
    }

    // Becomes visible and moves to provided location
    public void Show(Vector3 newPos)
    {
        if (visual == null)
        {
            visual = transform.Find("Canvas").Find("Image").GetComponent<Image>();
        }
        transform.position = newPos;
        visual.enabled = visualSetting;
        targetActive = true;
    }

    public void Hide()
    {
        visual.enabled = false;
        targetActive = false;
    }

    public bool IsActive()
    {
        return targetActive;
    }
}
