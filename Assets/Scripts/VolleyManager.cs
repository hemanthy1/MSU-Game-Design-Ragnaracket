using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolleyManager : MonoBehaviour
{
    public static VolleyManager instance;

    public Text volleyScore;

    int volley = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        volleyScore.text = volley.ToString();
    }

    // Update is called once per frame
    public void IncreaseVolley()
    {
        volley += 1;
        volleyScore.text = volley.ToString();
    }

    public void ResetVolley()
    {
        volley = 0;
        volleyScore.text = volley.ToString();
    }
}
