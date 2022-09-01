using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObject : MonoBehaviour
{

    enum CubeColor
    {
        None,
        Yellow,
        Green,
        Purple,
        Red,
        Blue
    }

    
    [SerializeField] private CubeColor Type;
    private float ScaleRatio=1;
    

    public void SetScaleRatio(int new_scale_ratio)
    {
        ScaleRatio = new_scale_ratio;

    }

    public float GetScaleRatio(int new_scale_ratio)
    {
        return ScaleRatio;

    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
