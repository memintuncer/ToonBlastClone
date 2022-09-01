using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCubesAlgorithm : MonoBehaviour
{
    Dictionary<int, List<int> > EmptyTiles = new Dictionary<int, List<int>>();
    Dictionary<int, int> EmptyTilesCounts = new Dictionary<int, int>();
    private void OnEnable()
    {
        EventManager.StartListening(GameConstants.GameEvents.COLOR_CUBE_SEARCH_COMPLETED, ColorCubeSearchExplosions);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameConstants.GameEvents.COLOR_CUBE_SEARCH_COMPLETED, ColorCubeSearchExplosions);
    }

    void ColorCubeSearchExplosions(EventParam param)
    {
        List<ColorCube> color_cubes_to_be_deleted = param.GetColorCubesToBeDeleted();
        List<AffectedByExplosionCube> affected_cubes_to_be_deleted = param.GetAffectedByExplosionCubesToBeDeleted();
        if (color_cubes_to_be_deleted.Count > 1)
        {
            foreach (ColorCube c_b in color_cubes_to_be_deleted)
            {

                TileGrid tile = c_b.GetParentTile();
                tile.RemoveCubeFromTile();
                UpdateEmptyTiles(tile);
                Destroy(c_b.gameObject);
                
               
            }

            
            //foreach (AffectedByExplosionCube c_b in affected_cubes_to_be_deleted)
            //{
            //    TileGrid tile = c_b.GetParentTile();
            //    tile.RemoveCubeFromTile();
            //    UpdateEmptyTiles(tile);

              
            //    c_b.CheckExplosionCondition();
                

            //}

            SendEmptyTilesMessage(param);


        }

        EventManager.TriggerEvent(GameConstants.GameEvents.CHECK_FOR_DESTROYER_CREATION, param);

        if (color_cubes_to_be_deleted.Count > 1)
        {
            //SendEmptyTilesMessage(param,EmptyTiles);
        }
        color_cubes_to_be_deleted.Clear();
        affected_cubes_to_be_deleted.Clear();
        param.SetColorCubesToBeDeleted(color_cubes_to_be_deleted);
        param.SetAffectedByExplosionCubesToBeDeleted(affected_cubes_to_be_deleted);
    }

    

    

    void UpdateEmptyTiles(TileGrid tile)
    {
        Point dict_point = tile.GetMatrixPoint();
        int dict_key = dict_point.GetMatrixIndexY();
        int new_value = dict_point.GetMatrixIndexX();
        if (!EmptyTiles.ContainsKey(dict_key))
        {
            List<int> value_list = new List<int>();
            value_list.Add(new_value);
            EmptyTiles.Add(dict_key, value_list);
            EmptyTilesCounts.Add(dict_key, 0);

        }

        if (EmptyTiles.ContainsKey(dict_key))
        {
            EmptyTilesCounts[dict_key] += 1;
            List<int> value_list = EmptyTiles[dict_key];
            int value_list_count = value_list.Count;
            if(new_value != value_list[value_list_count - 1]){
                if (Mathf.Abs((new_value - value_list[value_list_count - 1])) == 1)
                {
                    
                    if (new_value > value_list[value_list_count - 1])
                    {
                        value_list[value_list_count - 1] = new_value;
                    }

                }

                if(Mathf.Abs((new_value - value_list[value_list_count - 1])) != 1 && Mathf.Abs((new_value - value_list[value_list_count - 1])) !=0)
                {
                    
                    if (new_value > value_list[value_list_count - 1])
                    {
                        value_list.Add(new_value);
                    }

                   
                }
            }
           
        }
    }


    void SendEmptyTilesMessage(EventParam param)
    {
        param.SetEmptyTiles(EmptyTiles);
        param.SetEmptyTilesCount(EmptyTilesCounts);
        EventManager.TriggerEvent(GameConstants.GameEvents.START_FILLING_EMPTY_TILES, param);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
