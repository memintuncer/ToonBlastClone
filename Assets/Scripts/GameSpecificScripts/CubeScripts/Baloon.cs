using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : AffectedByExplosionCube
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void  CheckExplosionCondition()
    {
        base.CheckExplosionCondition();
        if(RequiredExplosionCount==0)
        {
            this.GetParentTile().RemoveCubeFromTile();
            Destroy(gameObject);
           
        }
    }

    public  void Boo()
    {
        Debug.Log("ASDASDASDASD");
    }
}
