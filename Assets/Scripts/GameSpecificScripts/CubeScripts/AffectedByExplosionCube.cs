using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectedByExplosionCube : Cube
{
    
    [SerializeField] protected int RequiredExplosionCount;
    [SerializeField] protected Cube.CubeType RequiredExplosionType;
    protected Cube.CubeType NeighbourExplosionType;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Boo() { }
   
    public virtual void CheckExplosionCondition()
    {
        if (NeighbourExplosionType.Equals(RequiredExplosionType) && RequiredExplosionCount>0)
        {
            RequiredExplosionCount--;
        }
    }

    public void SendNeighbourType(CubeType neighbour_cube_type)
    {
        NeighbourExplosionType = neighbour_cube_type;
    }
}
