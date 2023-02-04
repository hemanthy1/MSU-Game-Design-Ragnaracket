using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public bool visualSetting=false;
    private MeshRenderer visual;

    // Start is called before the first frame update
    void Start()
    {
        visual = GetComponent<MeshRenderer>();
    }

    // Becomes visible and moves to provided location
    public void Show(Vector3 newPos)
    {
        if (visual == null)
        {
            visual = GetComponent<MeshRenderer>();
        }
        transform.localPosition = newPos;
        visual.enabled = visualSetting;
    }

    public void Hide()
    {
        visual.enabled = false;
    }
}
