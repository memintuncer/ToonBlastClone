using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : SingletonComponent<GameManager>
{

    public enum GameStates{

        Idle,
        GameIsRunning,
        CheckingGameGoal,
        LevelFinished,
        Success,
        Fail
    }

    [System.Serializable]
    public class GameGoalCreator
    {
        [SerializeField] int GoalCount;
        [SerializeField] GameObject GameGoalObject;
        [SerializeField] Cube.CubeType GoalCubeType;
        [SerializeField] ColorCube.ColorType GoalColor;
        
        public GameObject GetGoalObject()
        {
            return GameGoalObject;
        }

        public Cube.CubeType GetCubeType()
        {
            return GoalCubeType;
        }

        public ColorCube.ColorType GetGoalColor()
        {
            return GoalColor;
        }

        public int GetGoalCount()
        {
            return GoalCount;
        }
    }
    

    public  GameGoalCreator[] GameGoalCreators;
    public static List<GameGoal> GameGoals = new List<GameGoal>();
    [SerializeField] Transform GameGoalsParent;
   
    [SerializeField] private int MoveCount;

    public static GameStates GameState;
    private bool IsFinish=false;
    private bool CheckingGameGoal;
    public static bool CanClick;
    public int TotalGoalCount = 0;
    [SerializeField] TextMeshProUGUI MoveCountText;

    public List<Cube> AllExplodedCubes = new List<Cube>();
    [SerializeField] GameObject SuccessUI;
    [SerializeField] GameObject FailUI;

    private void OnEnable()
    {
        EventManager.StartListening(GameConstants.GameEvents.DECREASE_MOVE_COUNT, DecraseMoveCount);
        EventManager.StartListening(GameConstants.GameEvents.GOAL_IS_ACHIEVED, DecraseTotalGoalCount);
        EventManager.StartListening(GameConstants.LEVEL_EVENTS.LEVEL_FAILED, LevelFailed);
        EventManager.StartListening(GameConstants.LEVEL_EVENTS.LEVEL_SUCCESSED, LevelSuccessed);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameConstants.GameEvents.DECREASE_MOVE_COUNT, DecraseMoveCount);
        EventManager.StopListening(GameConstants.GameEvents.GOAL_IS_ACHIEVED, DecraseTotalGoalCount);
        EventManager.StartListening(GameConstants.LEVEL_EVENTS.LEVEL_FAILED, LevelFailed);
        EventManager.StartListening(GameConstants.LEVEL_EVENTS.LEVEL_SUCCESSED, LevelSuccessed);
    }


    void LevelFailed(EventParam param)
    {
        FailUI.SetActive(true);

        EventManager.TriggerEvent(GameConstants.LEVEL_EVENTS.LEVEL_FINISHED, param);
    }

    void LevelSuccessed(EventParam param)
    {
        SuccessUI.SetActive(true);
        EventManager.TriggerEvent(GameConstants.LEVEL_EVENTS.LEVEL_FINISHED, param);
    }

    void DecraseTotalGoalCount(EventParam param)
    {
        TotalGoalCount--;
        CheckGoalCountState();
    }

    void CheckGoalCountState()
    {
        if(TotalGoalCount == 0)
        {
            
            EventManager.TriggerEvent(GameConstants.LEVEL_EVENTS.LEVEL_SUCCESSED, new EventParam());
        }
    }

    void DecraseMoveCount(EventParam param)
    {
        MoveCount--;
        MoveCountText.text = MoveCount.ToString();
        CheckMoveState();
        

    }


  
    void CheckMoveState()
    {
        if (MoveCount == 0)
        {
            if (TotalGoalCount > 0)
            {
                EventManager.TriggerEvent(GameConstants.LEVEL_EVENTS.LEVEL_FAILED, new EventParam());
            }
        }
    }

    void Start()
    {
        SetGameGoals();
        GridManager.Instance.GetSpecialCubes();
        GridManager.Instance.CreateGameGrid();

    }

 
    void Update()
    {
        
    }

    void SetGameGoals()
    {
        Vector2 pos_vector = Vector2.zero;
        int tota_goal_count = GameGoalCreators.Length;
        for (int i = 0; i < tota_goal_count; i++)
        {
            GameGoalCreator game_goal_creator = GameGoalCreators[i];
            GameObject temp = game_goal_creator.GetGoalObject();
            GameObject game_goal_object = Instantiate(temp, Vector2.zero, Quaternion.identity);
            GameGoal game_goal = game_goal_object.GetComponent<GameGoal>();
            game_goal.SetGoalColor(game_goal_creator.GetGoalColor());
            game_goal.SetGoalType(game_goal_creator.GetCubeType());
            game_goal.SetRequiredCount(game_goal_creator.GetGoalCount());
            game_goal_object.transform.parent = GameGoalsParent.transform;
            game_goal_object.transform.localPosition = pos_vector + new Vector2(i*0.75f, 0);
            game_goal_object.transform.localScale /= GameGoalCreators.Length;
            GameGoals.Add(game_goal);
        }
        MoveCountText.text = MoveCount.ToString();
        TotalGoalCount = tota_goal_count;
    }


   

    


   
  

}
