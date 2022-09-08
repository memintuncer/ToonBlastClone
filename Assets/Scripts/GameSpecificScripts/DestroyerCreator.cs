
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerCreator : MonoBehaviour
{
    [SerializeField] GameObject[] RocketCubes;


    [SerializeField] Transform TileMatrix;
    private void OnEnable()
    {
        EventManager.StartListening(GameConstants.GameEvents.CHECK_FOR_DESTROYER_CREATION, CheckForDestroyerCreation);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameConstants.GameEvents.CHECK_FOR_DESTROYER_CREATION, CheckForDestroyerCreation);
    }
  

    void CheckForDestroyerCreation(EventParam param)
    {
        
        int deleted_color_cube_count =0;
        Dictionary<int, int> empty_tiles_count = param.GetEmptyTilesCount();
        Dictionary<int,List<int>> empty_tiles_indexes = param.GetEmptyTiles();
        TileGrid selected_tile = param.GetColorCubesToBeDeleted()[0].GetParentTile();
        foreach(Cube cube in param.GetColorCubesToBeDeleted())
        {
            if(cube.GetCubeType() == Cube.CubeType.Normal)
            {
                deleted_color_cube_count++;
            }
        }
        if(deleted_color_cube_count>=5)
        {
            CreateRocket(selected_tile,param,empty_tiles_count, empty_tiles_indexes);
            
        }


        EventManager.TriggerEvent(GameConstants.GameEvents.START_FILLING_EMPTY_TILES, param);
        empty_tiles_count.Clear();
        empty_tiles_indexes.Clear();
    }

    
    
    private void CreateRocket(TileGrid selected_tile,EventParam param, Dictionary<int, int> empty_tiles_count, Dictionary<int, List<int>> empty_tiles_indexes)
    {

        int random_rocket_index = Random.Range(0, RocketCubes.Length);
        GameObject new_rocket_object = Instantiate(RocketCubes[random_rocket_index], Vector2.zero,Quaternion.identity);
        Rocket rocket_cube = new_rocket_object.GetComponent<Rocket>();


      

        rocket_cube.SetParentTile(selected_tile);
        selected_tile.AddCubeToTile(rocket_cube);
        
        
        new_rocket_object.transform.parent = TileMatrix;
        new_rocket_object.transform.localScale  = Vector2.one/2;
        new_rocket_object.transform.parent = selected_tile.transform;
        new_rocket_object.transform.localPosition = Vector2.zero;
        
        param.GetColorCubesToBeDeleted().Remove(param.GetColorCubesToBeDeleted()[0]);
        Point point = selected_tile.GetMatrixPoint();
        empty_tiles_count[point.GetMatrixIndexY()]--;
        List<int> empty_tiles = empty_tiles_indexes[point.GetMatrixIndexY()];

        empty_tiles=UpdateEmptyTileIndexes(empty_tiles,point);
        empty_tiles_indexes[point.GetMatrixIndexY()] = empty_tiles;
        param.SetEmptyTiles(empty_tiles_indexes);
        
        EventManager.TriggerEvent(GameConstants.GameEvents.START_FILLING_EMPTY_TILES, param);


    }

    //When a destroyer is created, updates to empty tiles indexes for FillAlgorithm

    List<int> UpdateEmptyTileIndexes(List<int> empty_tiles,Point point)
    {
       
        List<int> new_list = new List<int>();

        int point_x = point.GetMatrixIndexX();
        int point_y = point.GetMatrixIndexY();

        if (point_x != 0 && point_x!=empty_tiles[empty_tiles.Count-1])
        {
            if(GridManager.GetTileMatrix()[point_x - 1][point_y].CheckTileIsEmpty())
            {
                empty_tiles.Add(point_x - 1);
                empty_tiles.Sort((p1, p2) => p1.CompareTo(p2));
            }
            
        }


        if(point_x== empty_tiles[empty_tiles.Count - 1])
        {
            if (point_x == 0)
            {
                empty_tiles.Remove(point_x);
            }
            else
            {
                if (GridManager.GetTileMatrix()[point_x - 1][point_y].CheckTileIsEmpty())
                {
                    empty_tiles[empty_tiles.Count - 1] = point_x - 1;
                }

                if (!GridManager.GetTileMatrix()[point_x - 1][point_y].CheckTileIsEmpty())
                {
                    empty_tiles.Remove(point_x);
                }
            }

        }


        
        return empty_tiles;
    }

    
    
}
