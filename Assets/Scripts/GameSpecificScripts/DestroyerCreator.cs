
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerCreator : MonoBehaviour
{
    [SerializeField] GameObject[] RocketCubes;

    

    private void OnEnable()
    {
        EventManager.StartListening(GameConstants.GameEvents.CHECK_FOR_DESTROYER_CREATION, CheckForDestroyerCreation);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameConstants.GameEvents.CHECK_FOR_DESTROYER_CREATION, CheckForDestroyerCreation);
    }
    void Start()
    {
        
    }

    void CheckForDestroyerCreation(EventParam param)
    {
        //int deleted_color_cube_count = param.GetColorCubesToBeDeleted().Count;
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
        if(deleted_color_cube_count>=3)
        {
            CreateRocket(selected_tile,param,empty_tiles_count, empty_tiles_indexes);
            //CreateRocket(param.GetSelectedCube().GetParentTile(),param,empty_tiles_count, empty_tiles_indexes);
        }


        EventManager.TriggerEvent(GameConstants.GameEvents.START_FILLING_EMPTY_TILES, param);
        empty_tiles_count.Clear();
        empty_tiles_indexes.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void CreateRocket(TileGrid selected_tile,EventParam param, Dictionary<int, int> empty_tiles_count, Dictionary<int, List<int>> empty_tiles_indexes)
    {

        int random_rocket_index = Random.Range(0, RocketCubes.Length-1);
        GameObject new_rocket_object = Instantiate(RocketCubes[random_rocket_index], Vector2.zero,Quaternion.identity);
        Rocket rocket_cube = new_rocket_object.GetComponent<Rocket>();


        //TileGrid target_tile = NextTile(selected_tile);
        //rocket_cube.SetParentTile(target_tile);
        //target_tile.AddCubeToTile(rocket_cube);
        //new_rocket_object.transform.parent = target_tile.transform;
        //new_rocket_object.transform.localPosition = Vector2.zero;
        //new_rocket_object.transform.localScale /= 2;

        rocket_cube.SetParentTile(selected_tile);
        selected_tile.AddCubeToTile(rocket_cube);
        new_rocket_object.transform.parent = selected_tile.transform;
        new_rocket_object.transform.localPosition = Vector2.zero;
        new_rocket_object.transform.localScale /= 2;
        param.GetColorCubesToBeDeleted().Remove(param.GetColorCubesToBeDeleted()[0]);
        Point point = selected_tile.GetMatrixPoint();
        empty_tiles_count[point.GetMatrixIndexY()]--;
        List<int> empty_tiles = empty_tiles_indexes[point.GetMatrixIndexY()];

        empty_tiles=UpdateEmptyTileIndexes(empty_tiles,point);
        empty_tiles_indexes[point.GetMatrixIndexY()] = empty_tiles;
        param.SetEmptyTiles(empty_tiles_indexes);
        foreach (int i in empty_tiles_indexes[point.GetMatrixIndexY()])
        {
            Debug.Log("Deneme" + i);
        }

        EventManager.TriggerEvent(GameConstants.GameEvents.START_FILLING_EMPTY_TILES, param);


    }


    List<int> UpdateEmptyTileIndexes(List<int> empty_tiles,Point point)
    {
        foreach (int i in empty_tiles)
        {
            Debug.Log("Deneme" + i);
        }
        Debug.Log("-------Den------");
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

        if(point_x+1 == GridManager.GetMatrixHeight())
        {
            empty_tiles[empty_tiles.Count - 1] = point_x - 1;
        }

        foreach (int i in empty_tiles)
        {
            Debug.Log("Deneme" + i);
        }
        //empty_tiles.Sort((p1, p2) => p1.CompareTo(p2));
        return empty_tiles;
    }

    TileGrid NextTile(TileGrid selected_tile)
    {
        Point point = selected_tile.GetMatrixPoint();

        TileGrid target_tile = selected_tile;
        int x = point.GetMatrixIndexX();
        int y = point.GetMatrixIndexY();

        if (x != 0)
        {
            for (int i = x - 1; i >= 0; i++)
            {
                if (i == 0)
                {
                    target_tile = GridManager.GetTileMatrix()[i][y];
                }

                else
                {
                    Debug.Log(x + "X" + y);
                    TileGrid tile = GridManager.GetTileMatrix()[i][y];

                    if (!tile.CheckTileIsEmpty())
                    {
                        target_tile = GridManager.GetTileMatrix()[i + 1][y];
                        break;
                    }
                }
            }
        }

        
        //Debug.Log(x + "X" + y);
        //TileGrid next_tile = null;
        //if (x + 1 < GridManager.GetMatrixHeight())
        //{
        //    next_tile = GridManager.GetTileMatrix()[x + 1][y];
        //}
        //Debug.Log((x + 1).ToString() + "X" + y);
        ////Debug.Log(next_tile.name);
        //return next_tile;
        return target_tile;
    }
    
}
