using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class GameGoal :MonoBehaviour
{
    private Cube.CubeType GoalType;
    private ColorCube.ColorType GoalColor;
    int RequiredCount;
    private bool GoalIsAchieved = false;
    [SerializeField] TextMeshProUGUI CountText;
    public Cube.CubeType GetGoalType()
    {
        return GoalType;
    }

    public void SetGoalType(Cube.CubeType goal_type)
    {
        GoalType = goal_type;
    }

    public ColorCube.ColorType GetColorType()
    {
        return GoalColor;
    }


    public void SetGoalColor(ColorCube.ColorType goal_color)
    {
        GoalColor = goal_color;
    }
   
    public void SetRequiredCount(int count)
    {
        RequiredCount = count;
    }

    public void DecreaseRequiredCount()
    {
        
        if (RequiredCount > 0)
        {
            RequiredCount--;
            CountText.text = RequiredCount.ToString();
        }
        if(RequiredCount == 0 && !GoalIsAchieved)
        {
            GoalAchieved();
        }
    }

    private void Start()
    {
        CountText.text = RequiredCount.ToString();
    }

    void GoalAchieved()
    {
        GoalIsAchieved = true;
        EventManager.TriggerEvent(GameConstants.GameEvents.GOAL_IS_ACHIEVED, new EventParam());
    }
}
