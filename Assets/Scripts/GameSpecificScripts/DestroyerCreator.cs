
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
        int deleted_color_cube_count = param.GetColorCubesToBeDeleted().Count;
        TileGrid selected_tile = param.GetColorCubesToBeDeleted()[deleted_color_cube_count - 1].GetParentTile();
        if(deleted_color_cube_count>=6)
        {
            CreateRocket(selected_tile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void CreateRocket(TileGrid selected_tile)
    {

        int random_rocket_index = Random.Range(0, RocketCubes.Length);
        GameObject new_rocket_object = Instantiate(RocketCubes[random_rocket_index], Vector2.zero,Quaternion.identity);
        Rocket rocket_cube = new_rocket_object.GetComponent<Rocket>();
        rocket_cube.SetParentTile(selected_tile);
        selected_tile.AddCubeToTile(rocket_cube);
        new_rocket_object.transform.parent = selected_tile.transform;
        new_rocket_object.transform.localPosition = Vector2.zero;
        new_rocket_object.transform.localScale /= 2;

    }
}
