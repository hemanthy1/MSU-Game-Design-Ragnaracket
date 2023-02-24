using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeflectCounter : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Text").GetComponent<Text>();
    }

    public void UpdateCounter(int newVal)
    {
        text.text = "" + newVal;
    }

    public void ResetCounter()
    {
        text.text = "0";
    }
}
