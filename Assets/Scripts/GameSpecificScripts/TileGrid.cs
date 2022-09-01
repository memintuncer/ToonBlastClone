using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 1)
        {
            int x = 1;
        }
    }

    public void AddCubeToTile(Cube new_cube)
    {
        IsEmpty = false;
        CurrentCube = new_cube;
    } 
    public void RemoveCubeFromTile()
    {
        IsEmpty = true;
        CurrentCube = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Cube")
        //{
        //    if (CurrentCube == null)
        //    {
        //        Cube new_cube = collision.GetComponent<Cube>();
        //        AddCubeToTile(new_cube);
        //        new_cube.transform.parent = transform;
        //        new_cube.SetParentTile(this);
        //    }
            
        //    //if (ParentTileGrid == null)
        //    //{
        //    //    ParentTileGrid = new_cube;
        //    //    SetParentTile(new_cube);
        //    //    new_cube.AddCubeToTile(this);
        //    //    transform.parent = new_cube.transform;

        //    //}
        //    //if (ParentTileGrid.gameObject != collision)
        //    //{

        //    //    SetParentTile(new_cube);
        //    //    new_cube.AddCubeToTile(this);
        //    //    transform.parent = new_cube.transform;
        //    //}
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }

}
