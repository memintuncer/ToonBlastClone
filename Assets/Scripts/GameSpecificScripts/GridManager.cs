using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager :  SingletonComponent<GridManager>
{
    
    
    
    //Allow to add speacial cubes and editing matrix indexes
    [System.Serializable]
    public class CubeSelector
    {
        
        
        [SerializeField] Vector2 PosIndex;
        [SerializeField] public GameObject SpecialCubeObjectPrefab;
        [SerializeField] Cube.CubeType Type;
        
        public Vector2 GetPosIndex()
        {
            return PosIndex;
        }

        public GameObject GetCube()
        {
            return SpecialCubeObjectPrefab;
        }
        
        
    }
    //If there will be special cubes (Such as duck, baloon), they must added to this array for Matrix Creation
    public CubeSelector[] SpecialCubes;

    

    private static List<List<TileGrid>> TileMatrix = new List<List<TileGrid>>();

    [SerializeField] public int HorizontalMatrixSize, VerticalMatrixSize;

    
    [SerializeField]  GameObject[] AllCubes;
  
    [SerializeField]   GameObject TilePrefab;
    [SerializeField]   Transform GridMatrix;
    private static int MatrixWidth, MatrixHeight;
    private Dictionary<Point, GameObject> SpecialCubesIndexes = new Dictionary<Point, GameObject>();
    [SerializeField] MatrixScaler MatrixScaler;
    public Vector2 ScaleFactor;
    
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
        MatrixScaler.ScaleMatrix(HorizontalMatrixSize, VerticalMatrixSize);
        
    }


    public void GetSpecialCubes()
    {
        foreach(CubeSelector cube_selector in SpecialCubes)
        {
            Vector2 pos_vec = cube_selector.GetPosIndex();
            Point new_point = new Point((int) pos_vec.x,(int)pos_vec.y);
            

            SpecialCubesIndexes.Add(new_point, cube_selector.SpecialCubeObjectPrefab);

            foreach(Point p in SpecialCubesIndexes.Keys)
            {
                Debug.Log(p.GetMatrixIndexX() + p.GetMatrixIndexY());
            }
        }

        
    }

    
    public void CreateGameGrid()
    {
        

        
        for(int i = 0; i < VerticalMatrixSize; i++)
        {
            List<TileGrid> temp = new List<TileGrid>();
            for(int j = 0; j < HorizontalMatrixSize; j++)
            {
                int random_int = Random.Range(0, AllCubes.Length);
                GameObject cube_object = null;
                Point matrix_point = new Point(i, j); // i=x, j=y



               
                foreach (Point p in SpecialCubesIndexes.Keys)
                {
                    if (p.GetMatrixIndexX() == i && p.GetMatrixIndexY() == j)
                    {
                        
                        cube_object = Instantiate(SpecialCubesIndexes[p], Vector2.zero, Quaternion.identity);
                        
                        break;
                    }
                    
                        
                    
                }

                if(cube_object == null)
                {
                    cube_object = Instantiate(AllCubes[random_int], Vector2.zero, Quaternion.identity);
                }
                GameObject tile_object = Instantiate(TilePrefab, Vector2.zero, Quaternion.identity);
                
                Cube cube_object_script = cube_object.GetComponent<Cube>();
                
                TileGrid tile_grid = tile_object.GetComponent<TileGrid>();
                
                


                tile_grid.SetMatrixPoint(matrix_point);
                tile_grid.AddCubeToTile(cube_object_script);
                cube_object.transform.parent = tile_grid.transform;
                tile_grid.transform.parent = GridMatrix;
                tile_grid.transform.localPosition = Vector2.zero + new Vector2(j,i)/2;
                cube_object.transform.localPosition = Vector2.zero;
                cube_object_script.SetParentTile(tile_grid);
                
                tile_object.transform.localScale /= 2;
                ScaleFactor = tile_object.transform.localScale;

                Debug.Log("ScaleFactor" + ScaleFactor);

                temp.Add(tile_grid);
                tile_grid.gameObject.name = tile_grid.gameObject.name + (i).ToString() + "X"  + (j).ToString();

                

            }
            TileMatrix.Add(temp);
            

        }
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
