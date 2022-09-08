using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAlgorithm : MonoBehaviour
{
    [SerializeField] float Fall_Time;

    public GameObject[] Prefabs;
    [SerializeField] Transform ParticleParent;
    [SerializeField] Transform GridMatrix;
    private void OnEnable()
    {
        EventManager.StartListening(GameConstants.GameEvents.START_FILLING_EMPTY_TILES, FillEmptyTiles);
    }
    private void OnDisable()
    {
        EventManager.StopListening(GameConstants.GameEvents.START_FILLING_EMPTY_TILES, FillEmptyTiles);
    }

    //Make cubes to fall and assign to new TileGrids
    void FillEmptyTiles(EventParam param)
    {
        Dictionary<int, List<int>> tile_indexes = param.GetEmptyTiles();
        
        List<List<TileGrid>> TileMatrix = GridManager.GetTileMatrix();
        Dictionary<int, List<int>> empty_tiles = new Dictionary<int, List<int>>();
        Dictionary<int, int> empty_tiles_count = new Dictionary<int, int>();
        empty_tiles = param.GetEmptyTiles();
        empty_tiles_count = param.GetEmptyTilesCount();



        foreach (int tile_key_y in empty_tiles.Keys)
        {
            foreach (int tile_value_x in empty_tiles[tile_key_y])
            {
                if (tile_value_x + 1 < GridManager.GetMatrixHeight())
                {
                    
                    //First Cube to be fall
                    TileGrid falling_tile = TileMatrix[tile_value_x + 1][tile_key_y];
                   
                    if (!falling_tile.CheckTileIsEmpty())
                    {
                        Cube cube = falling_tile.GetCurrentCube();
                        
                       
                        while (!falling_tile.CheckTileIsEmpty())
                        {
                            Cube tile_cube = falling_tile.GetCurrentCube();
                            
                            if (tile_cube.GetCanFall())
                            {
                                AssignNewTileToCube(tile_cube);
                            }

                            else
                            {
                                empty_tiles_count[tile_key_y] -= GetEmptyTilesTillStationaryCube(falling_tile);
                            }
                            falling_tile = NextTile(falling_tile);
                            if (falling_tile == null)
                            {
                                break;
                            }



                        }
                    }
                    



                }


            }

        }
        BringNewCubes(empty_tiles_count);



        ClearMessageParamaters(param);
        StartCoroutine(ClearMessageParamaters(param));
       
    }






    //Makes non-falling objects behave like can fall and updates empty tiles.
    int GetEmptyTilesTillStationaryCube(TileGrid next_tile)
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

    IEnumerator ClearMessageParamaters(EventParam param)
    {
        param.GetEmptyTiles().Clear();

        param.GetEmptyTilesCount().Clear();
        param.GetColorCubesToBeDeleted().Clear();
        param.GetAffectedByExplosionCubesToBeDeleted().Clear();

        param.SetSelectedCube(null);
        yield return new WaitForSeconds(0.25f);
    }
    

    TileGrid NextTile(TileGrid falling_tile)
    {
        Point point = falling_tile.GetMatrixPoint();
        int x = point.GetMatrixIndexX();
        int y = point.GetMatrixIndexY();
       
        TileGrid next_tile = null;
        if (x + 1 < GridManager.GetMatrixHeight())
        {
            next_tile = GridManager.GetTileMatrix()[x + 1][y];
        }
        
        return next_tile;
    }

    void AssignNewTileToCube(Cube cube)
    {
        Point point = cube.GetParentTile().GetMatrixPoint();
        int x = point.GetMatrixIndexX();
        int y = point.GetMatrixIndexY();

        TileGrid next_tile = GridManager.GetTileMatrix()[x - 1][y];
        cube.GetParentTile().RemoveCubeFromTile();
        TileGrid target_tile = SlideToBottom(next_tile);
        cube.SetParentTile(target_tile);
        target_tile.AddCubeToTile(cube);
        


    }


    void BringNewCubes(Dictionary<int, int> empty_tiles_count)
    {
        int random_index = 0;
        foreach (int tile_key_y in empty_tiles_count.Keys)
        {
            int new_cubes_to_created_count = empty_tiles_count[tile_key_y];
            int temp = new_cubes_to_created_count;
            for (int i = 0; i < new_cubes_to_created_count; i++)
            {
                Vector2 fall_start_position = GridManager.GetTileMatrix()[GridManager.GetMatrixHeight() - 1][tile_key_y].GetPositionVector();
                fall_start_position += new Vector2(0, (5 + (float)i) / 2);
                Point target_point = new Point(GridManager.GetMatrixHeight()-temp, tile_key_y);
                random_index = Random.Range(0, Prefabs.Length - 1);
                GameObject new_cube = Instantiate(Prefabs[random_index], fall_start_position, Quaternion.identity);
                Cube cube = new_cube.GetComponent<Cube>();
                cube.transform.parent = GridMatrix;
                cube.transform.localScale = Vector2.one/2;
                SetNewCubesToTiles(cube, GridManager.GetTileMatrix()[GridManager.GetMatrixHeight() - temp][tile_key_y]);
                temp--;
            }
        }
    }


    void SetNewCubesToTiles(Cube cube, TileGrid tile_grid)
    {
        cube.SetParentTile(tile_grid);
        tile_grid.AddCubeToTile(cube);
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
            next_tile = GridManager.GetTileMatrix()[x - 1][y];

            if (!next_tile.CheckTileIsEmpty())
            {
                next_tile = GridManager.GetTileMatrix()[x][y];
                Debug.Log("Finish" + next_tile.name);
                break;

            }



        }

        return next_tile;

    }
    

    

}
