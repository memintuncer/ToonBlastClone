using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCubesAlgorithm : MonoBehaviour
{
    Dictionary<int, List<int> > EmptyTilesIndexes = new Dictionary<int, List<int>>();
    Dictionary<int, int> EmptyTilesCounts = new Dictionary<int, int>();
    Cube SelectedCube;
    [SerializeField] private Transform TempParticleEffectsParent;
    private AudioSource SoundEffect;

    private void Awake()
    {
        SoundEffect = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        EventManager.StartListening(GameConstants.GameEvents.COLOR_CUBE_SEARCH_COMPLETED, ColorCubeSearchExplosions);
        EventManager.StartListening(GameConstants.GameEvents.DESTOYER_EXPLOSION, DestoyerExplosion);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameConstants.GameEvents.COLOR_CUBE_SEARCH_COMPLETED, ColorCubeSearchExplosions);
        EventManager.StopListening(GameConstants.GameEvents.DESTOYER_EXPLOSION, DestoyerExplosion);
    }

    void ColorCubeSearchExplosions(EventParam param)
    {
        List<Cube> AllExplodedCubes = new List<Cube>();
        SelectedCube = param.GetSelectedCube();
        List<Cube> color_cubes_to_be_deleted = param.GetColorCubesToBeDeleted();
        List<AffectedByExplosionCube> affected_cubes_to_be_deleted = param.GetAffectedByExplosionCubesToBeDeleted();
        if (color_cubes_to_be_deleted.Count > 1)
        {
            SoundEffect.Play();
            foreach (Cube cube in color_cubes_to_be_deleted)
            {

                TileGrid tile = cube.GetParentTile();
                tile.RemoveCubeFromTile();
                UpdateEmptyTiles(tile);
                ((ColorCube)cube).CheckForGameGoal();
                cube.DestroyCube(TempParticleEffectsParent);
                
            }


            foreach (AffectedByExplosionCube cube in affected_cubes_to_be_deleted)
            {
                TileGrid tile = cube.GetParentTile();
                tile.RemoveCubeFromTile();
                UpdateEmptyTiles(tile);

                
                cube.CheckExplosionCondition(TempParticleEffectsParent);


            }
            param.SetEmptyTiles(EmptyTilesIndexes);
            param.SetEmptyTilesCount(EmptyTilesCounts);

            EventManager.TriggerEvent(GameConstants.GameEvents.CHECK_FOR_DESTROYER_CREATION, param);

        }



       
        color_cubes_to_be_deleted.Clear();
        affected_cubes_to_be_deleted.Clear();
        param.SetColorCubesToBeDeleted(color_cubes_to_be_deleted);
        param.SetAffectedByExplosionCubesToBeDeleted(affected_cubes_to_be_deleted);
        StartCoroutine(DestroyParticleEffects(TempParticleEffectsParent));

    }


    IEnumerator DestroyParticleEffects(Transform particles_parent)
    {
        yield return new WaitForSeconds(2f);
        int count = particles_parent.childCount;

        for (int i = 0; i < count; i++)
        {
            particles_parent.GetChild(i).gameObject.SetActive(false);
        }
    }
   

    void DestoyerExplosion(EventParam param)
    {
        List<Cube> cubes_to_be_deleted = param.GetColorCubesToBeDeleted();
        Debug.Log("Silinecek Sayýsý: " + cubes_to_be_deleted.Count);
        foreach (Cube cube in cubes_to_be_deleted)
        {

            TileGrid tile = cube.GetParentTile();
            tile.RemoveCubeFromTile();
            UpdateEmptyTiles(tile);



            


            if (cube.GetCubeType() == Cube.CubeType.Affectible)
            {
                
                ((AffectedByExplosionCube)cube).DestoyerExplosion = true;
                ((AffectedByExplosionCube)cube).CheckExplosionCondition(TempParticleEffectsParent);
                

                
            }
            if (cube.GetCubeType() == Cube.CubeType.Destroyer)
            {
               
                ((DestoyerCube)cube).DestroyerAnimation();
            }

            if (cube.GetCubeType() != Cube.CubeType.Destroyer)
            {
                
                cube.DestroyCube(TempParticleEffectsParent);
                Destroy(cube.gameObject);
            }

           

        }


        param.SetEmptyTiles(EmptyTilesIndexes);
        param.SetEmptyTilesCount(EmptyTilesCounts);

        
        SendEmptyTilesMessage(param);
        cubes_to_be_deleted.Clear();
    }

    void UpdateEmptyTiles(TileGrid tile)
    {
        Point dict_point = tile.GetMatrixPoint();
        int dict_key = dict_point.GetMatrixIndexY();
        int new_value = dict_point.GetMatrixIndexX();
        if (!EmptyTilesIndexes.ContainsKey(dict_key))
        {
            List<int> value_list = new List<int>();
            value_list.Add(new_value);
            EmptyTilesIndexes.Add(dict_key, value_list);
            EmptyTilesCounts.Add(dict_key, 0);

        }

        if (EmptyTilesIndexes.ContainsKey(dict_key))
        {
            EmptyTilesCounts[dict_key] += 1;
            List<int> value_list = EmptyTilesIndexes[dict_key];
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
       
        EventManager.TriggerEvent(GameConstants.GameEvents.START_FILLING_EMPTY_TILES, param);
    }

    
}
