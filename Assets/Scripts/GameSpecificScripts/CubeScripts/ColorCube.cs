using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCube : Cube
{
    public enum ColorType
    {
        None,
        Blue,
        Green,
        Yellow,
        Red,
        Purple
    }

    
    [SerializeField] private ColorType Color;

    private List<ColorCube> SameColorNeighBours = new List<ColorCube>();
    private List<AffectedByExplosionCube> AffectedByExplosionCubes = new List<AffectedByExplosionCube>();
    public ColorType GetColor()
    {
        return Color;
    }

   
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


   
    private void OnMouseDown()
    {
        base.OnMouseDown();
        //SearchForNeighbours();
        SendColorCubeSelectedMessage();
        
    }


    void SendColorCubeSelectedMessage()
    {
        EventParam color_cube_param = new EventParam();
        color_cube_param.SetSelectedCube(this);
        
        EventManager.TriggerEvent(GameConstants.GameEvents.COLOR_CUBE_SELECTED, color_cube_param);
    }

    private void SearchForNeighbours()
    {

        //gridManager.TileMatrix;
    }

    void CheckTraversedCube()
    {

    }

   
}
