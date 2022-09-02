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
        
        SendColorCubeSelectedMessage();
        
    }


    void SendColorCubeSelectedMessage()
    {
        EventParam color_cube_param = new EventParam();
        color_cube_param.SetSelectedCube(this);
        
        EventManager.TriggerEvent(GameConstants.GameEvents.COLOR_CUBE_SELECTED, color_cube_param);
    }

   
    
   
}
