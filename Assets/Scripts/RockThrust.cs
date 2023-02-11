using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrust : MonoBehaviour
{
    protected float parabolaAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parabolaAnimation += Time.deltaTime;

        parabolaAnimation = parabolaAnimation % 5f;

        transform.position = MathParabola.Parabola(Vector3.zero, Vector3.forward * 10f, 5f, parabolaAnimation / 5f);
    }
}
