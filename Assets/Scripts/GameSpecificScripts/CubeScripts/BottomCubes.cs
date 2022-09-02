using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BottomCubes : Cube
{
    // Start is called before the first frame update
    public virtual bool CheckFalseClick() {
        return false;
    }
    
    
}
