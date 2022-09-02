using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : DestoyerCube
{
    public enum RocketDirection
    {
        Horizontal,
        Vertical
    }

    [SerializeField] private RocketDirection Direction;

    public RocketDirection GetDirection()
    {
        return Direction;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        base.OnMouseDown();
        DestroyGrids();
    }

    public override void DestroyGrids()
    {
        base.DestroyGrids();
        List<Cube> cubes_to_be_destroyed = new List<Cube>();
        Point start_point = GetParentTile().GetMatrixPoint();
        int x = start_point.GetMatrixIndexX();
        int y = start_point.GetMatrixIndexY();

        switch (Direction)
        {
            case RocketDirection.Horizontal:
                DestroyHorizontal(x,cubes_to_be_destroyed);
                break;
            case RocketDirection.Vertical:
                DestroyVertical(y,cubes_to_be_destroyed);
                break;

        }
        EventParam param = new EventParam();
        param.SetColorCubesToBeDeleted(cubes_to_be_destroyed);
        EventManager.TriggerEvent(GameConstants.GameEvents.DESTOYER_EXPLOSION, param);
        gameObject.SetActive(false);
    }

    

    void DestroyHorizontal(int point_x,List<Cube> cubes_to_be_destroyed)
    {
        for(int i = 0; i < GridManager.GetMatrixWidth(); i++)
        {
            cubes_to_be_destroyed.Add((GridManager.GetTileMatrix()[point_x][i]).GetCurrentCube());
        }
    }

    void DestroyVertical(int point_y,List<Cube> cubes_to_be_destroyed)
    {
        for (int i = 0; i < GridManager.GetMatrixHeight(); i++)
        {
            cubes_to_be_destroyed.Add((GridManager.GetTileMatrix()[i][point_y]).GetCurrentCube());
        }
    }
}
