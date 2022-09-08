using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Tile for matrix Grid. It holds the cubes for matrix search, destroy and fill

[System.Serializable]
public class TileGrid : MonoBehaviour
{
    [SerializeField] Cube CurrentCube;
    [SerializeField] bool IsEmpty;
    private Vector2 PositionVector;
    private Point MatrixPoint;


    public Cube GetCurrentCube()
    {
        return CurrentCube;
    }
    public void SetMatrixPoint(Point new_point)
    {
        MatrixPoint = new_point;
    }

    public bool CheckTileIsEmpty()
    {
        return IsEmpty;
    }
    public Point GetMatrixPoint()
    {
        return MatrixPoint;
    }


    public Vector2 GetPositionVector()
    {
        return PositionVector;
    }
    void Start()
    {
        PositionVector = transform.position;
    }

    
    public void AddCubeToTile(Cube new_cube)
    {
        IsEmpty = false;
        CurrentCube = new_cube;
        CurrentCube.transform.parent = transform;
    } 
    public void RemoveCubeFromTile()
    {
        IsEmpty = true;
        
    }


   
}
