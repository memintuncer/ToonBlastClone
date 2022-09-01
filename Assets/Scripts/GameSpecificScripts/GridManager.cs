using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager :  SingletonComponent<GridManager>
{
    private static List<List<TileGrid>> TileMatrix = new List<List<TileGrid>>();
    [SerializeField] public int HorizontalMatrixSize, VerticalMatrixSize;
    [SerializeField]  GameObject[] AllCubes;
  
    [SerializeField]   GameObject TilePrefab;
    [SerializeField]   Transform GridMatrix;
    private static int MatrixWidth, MatrixHeight; 


    public static int  GetMatrixWidth()
    {
        return MatrixWidth;
    }

    public static int GetMatrixHeight()
    {
        return MatrixHeight;
    }
    public int GetHorizontalMatrixSize()
    {
        return HorizontalMatrixSize;
    }

    public int GetVerticalMatrixSize()
    {
        return VerticalMatrixSize;
    }

    void Start()
    {
        MatrixHeight = VerticalMatrixSize;
        MatrixWidth = HorizontalMatrixSize;
        //CreateGameGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGameGrid()
    {
        for(int i = 0; i < VerticalMatrixSize; i++)
        {
            List<TileGrid> temp = new List<TileGrid>();
            for(int j = 0; j < HorizontalMatrixSize; j++)
            {
                int random_int = Random.Range(0, AllCubes.Length-1);
                GameObject tile_object = Instantiate(TilePrefab, Vector2.zero, Quaternion.identity);
                GameObject cube_object = Instantiate(AllCubes[random_int], Vector2.zero, Quaternion.identity);
                Cube cube_object_script = cube_object.GetComponent<Cube>();
                
                TileGrid tile_grid = tile_object.GetComponent<TileGrid>();
                tile_grid.AddCubeToTile(cube_object_script);
                cube_object.transform.parent = tile_grid.transform;
                tile_grid.transform.parent = GridMatrix;
                tile_grid.transform.localPosition = Vector2.zero + new Vector2(j,i)/2;
                cube_object.transform.localPosition = Vector2.zero;
                cube_object_script.SetParentTile(tile_grid);
                tile_object.transform.localScale /= 2;
                Point matrix_point = new Point(i, j); // i=x, j=y
                tile_grid.SetMatrixPoint(matrix_point);
                temp.Add(tile_grid);
                tile_grid.gameObject.name = tile_grid.gameObject.name + (i).ToString() + "X"  + (j).ToString();

                

            }
            TileMatrix.Add(temp);
            

        }
    }

    void SendGridMatrixCreatedMessage()
    {

    }

    void UpdateGrid()
    {

    }

    public static List<List<TileGrid>> GetTileMatrix()
    {
        return TileMatrix;
    }
      public static void SetTileMatrix(List<List<TileGrid>> x)
    {
        TileMatrix = x;
    }
    
}
