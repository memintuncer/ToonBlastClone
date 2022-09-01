using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAlgorithm : MonoBehaviour
{
    [SerializeField] float Fall_Time;
    [SerializeField] GameObject[] NewCubes;
    private void OnEnable()
    {
        EventManager.StartListening(GameConstants.GameEvents.START_FILLING_EMPTY_TILES, FillEmptyTiles);
    }
    private void OnDisable()
    {
        EventManager.StopListening(GameConstants.GameEvents.START_FILLING_EMPTY_TILES, FillEmptyTiles);
    }
    
    void FillEmptyTiles(EventParam param)
    {
        //Dictionary<int, int> dict = param.GetEmptyTiles();
        List<List<TileGrid>> TileMatrix = GridManager.GetTileMatrix();
        //Dictionary<int, List<int>> empty_tiles = new Dictionary<int, List<int>>();
        Dictionary<int, int> empty_tiles_count = new Dictionary<int, int>();
        
        empty_tiles_count = param.GetEmptyTilesCount();

        

        foreach(int tile_key_y in empty_tiles_count.Keys)
        {
            int new_cubes_to_created_count = empty_tiles_count[tile_key_y];
            

            for(int i = 0; i < new_cubes_to_created_count; i++)
            {
                Vector2 fall_start_position = GridManager.GetTileMatrix()[GridManager.GetMatrixHeight() - 1][tile_key_y].GetPositionVector();
                fall_start_position += new Vector2(0,(10 +(float) i) / 2); 

                GameObject new_cube = Instantiate(NewCubes[Random.Range(0, NewCubes.Length)], fall_start_position, Quaternion.identity);
                new_cube.transform.localScale /= 2;
                new_cube.name += "CreatedCube";
            }

        

        }

        ClearMessageParamaters(param);


    }
   

    int GetEmptyTilesTillStationaryCube(TileGrid next_tile )
    {
        
        int empty_tiles_count = 0;
        Point point = next_tile.GetMatrixPoint();
        int initial_x = point.GetMatrixIndexX();
        int initial_y = point.GetMatrixIndexY();

        while (next_tile.CheckTileIsEmpty())
        {
            empty_tiles_count++;
            int x = next_tile.GetMatrixPoint().GetMatrixIndexX();
            int y = next_tile.GetMatrixPoint().GetMatrixIndexY();
            if (x == 0)
            {
                next_tile = GridManager.GetTileMatrix()[0][y];
                break;
            }
            next_tile = GridManager.GetTileMatrix()[x - 1][y];

            if (!next_tile.CheckTileIsEmpty())
            {
                next_tile = GridManager.GetTileMatrix()[x][y];
                Debug.Log("Finish" + next_tile.name);
                break;

            }



        }
        next_tile = GridManager.GetTileMatrix()[initial_x][initial_y];
        return empty_tiles_count;
        
    }
    void ClearMessageParamaters(EventParam param)
    {
        param.GetEmptyTiles().Clear();
        
        param.GetEmptyTilesCount().Clear();
        param.GetColorCubesToBeDeleted().Clear();
        param.GetAffectedByExplosionCubesToBeDeleted().Clear();
       
        param.SetSelectedCube(null);
        
    }

    TileGrid NextTile(TileGrid falling_tile)
    {
        Point point = falling_tile.GetMatrixPoint();
        int x = point.GetMatrixIndexX();
        int y = point.GetMatrixIndexY();
        Debug.Log(x + "X" + y);
        TileGrid next_tile = null;
        if (x + 1 < GridManager.GetMatrixHeight())
        {
            next_tile = GridManager.GetTileMatrix()[x + 1][y];
        }
        Debug.Log((x+1).ToString() + "X" + y);
        //Debug.Log(next_tile.name);
        return next_tile;
    }

    void AssignNewTileToCube(Cube cube)
    {
        Point point = cube.GetParentTile().GetMatrixPoint();
        int x = point.GetMatrixIndexX();
        int y = point.GetMatrixIndexY();
        
        TileGrid next_tile = GridManager.GetTileMatrix()[x-1][y];
        cube.GetParentTile().RemoveCubeFromTile();
        TileGrid target_tile = SlideToBottom(next_tile);

        //cube.FallToTargetTile(Fall_Time, target_tile);
        //cube.SetParentTile(target_tile);
        StartCoroutine(FallCubesToTargetsCRT(cube,target_tile));
        
        //target_tile.AddCubeToTile(cube);
        //cube.transform.parent = target_tile.transform;
        //cube.transform.localPosition = Vector2.zero;
        

    }


    IEnumerator FallCubesToTargetsCRT(Cube cube, TileGrid target_tile)
    {
        cube.FallToTargetTile(Fall_Time, target_tile);
        yield return new WaitForSeconds(Fall_Time);
    }


    TileGrid SlideToBottom(TileGrid next_tile)
    {
        while (next_tile.CheckTileIsEmpty())
        {
            int x = next_tile.GetMatrixPoint().GetMatrixIndexX();
            int y = next_tile.GetMatrixPoint().GetMatrixIndexY();
            if (x == 0)
            {
                next_tile = GridManager.GetTileMatrix()[0][y];
                break;
            }
            next_tile = GridManager.GetTileMatrix()[x-1][y];
            
            if (!next_tile.CheckTileIsEmpty())
            {
                next_tile = GridManager.GetTileMatrix()[x][y];
                Debug.Log("Finish" + next_tile.name);
                break;

            }

            

        }

        return next_tile;

    }
    void MoveCubesToEmptyTiles()
    {

    }

    // Start is called before the first frame update
   
}
