using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolleyManager : MonoBehaviour
{
    public static VolleyManager instance;

    public Text volleyText;

    int volleyScore = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        volleyText.text = volleyScore.ToString();
    }

    // Update is called once per frame
    public void AddVolley()
    {
        volleyScore += 1;
        volleyText.text = volleyScore.ToString();
    }

    public void ResetVolley()
    {
        volleyScore = 0;
        volleyText.text = volleyScore.ToString();
    }
}
