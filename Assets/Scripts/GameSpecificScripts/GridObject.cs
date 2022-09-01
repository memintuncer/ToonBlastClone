using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private GameObject ConnectedCube;
    private Vector2 PositionVector;
    private float ScaleRatio;
    private List<List<CubeObject>> GameMatrix = new List<List<CubeObject>>();



    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignCubeToGrid(GameObject cube_object)
    {

        

        if(ConnectedCube == null)
        {
            GameObject new_cube_object = cube_object;
            ConnectedCube = new_cube_object;
        }

        else
        {
            ConnectedCube = cube_object;
        }

        cube_object.transform.parent = transform;
    }
}
