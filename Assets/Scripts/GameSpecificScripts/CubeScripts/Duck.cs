using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Duck : BottomCubes
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool CheckFalseClick()
    {
        bool return_value = base.CheckFalseClick();
        return false;

    }
}
