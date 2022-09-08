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


    private void OnEnable()
    {
        EventManager.StartListening(GameConstants.LEVEL_EVENTS.LEVEL_FINISHED, SetCanClick);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameConstants.LEVEL_EVENTS.LEVEL_FINISHED, SetCanClick);
    }


    void SetCanClick(EventParam param)
    {
        CanPlay = false;
    }


    private void OnMouseDown()
    {
        base.OnMouseDown();
        if (CanPlay)
        {
            SendColorCubeSelectedMessage();
        } 
        
       
        
    }


    void SendColorCubeSelectedMessage()
    {
        EventManager.TriggerEvent(GameConstants.GameEvents.DECREASE_MOVE_COUNT, new EventParam());
        EventParam color_cube_param = new EventParam();
        color_cube_param.SetSelectedCube(this);
        
        EventManager.TriggerEvent(GameConstants.GameEvents.COLOR_CUBE_SELECTED, color_cube_param);
        
    }

   
    public void CheckForGameGoal()
    {
        List<GameGoal> game_goals = GameManager.GameGoals;
        for(int i = 0; i < game_goals.Count; i++)
        {
            GameGoal game_goal = game_goals[i];
            if (game_goal.GetGoalType().Equals(cubeType) && game_goal.GetColorType().Equals(Color))
            {
                game_goal.DecreaseRequiredCount();
            }
        }
    }
    
   
}
