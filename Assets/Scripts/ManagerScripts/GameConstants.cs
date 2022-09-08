using UnityEngine;

public class GameConstants
{
    
    public struct LEVEL_EVENTS
    {
       
        public static string OBJECTIVE_FAILED = "OBJECTIVE_FAILED";
        public static string LEVEL_FINISHED = "LEVEL_FINISHED";
        public static string LEVEL_FAILED = "LEVEL_FAILED";
        public static string LEVEL_SUCCESSED = "LEVEL_SUCCESSED";
        public static string LEVEL_STARTED = "LEVEL_STARTED";
        
       
    }

   
    public struct GameEvents
    {
       
        public static string GAME_STARTED = "GAME_STARTED";
        public static string COLOR_CUBE_SELECTED = "COLOR_CUBE_SELECTED";
        public static string COLOR_CUBE_SEARCH_COMPLETED = "COLOR_CUBE_SEARCH_COMPLETED";
        public static string CHECK_FOR_DESTROYER_CREATION = "CHECK_FOR_DESTROYER_CREATION";
        public static string START_FILLING_EMPTY_TILES = "START_FILLING_EMPTY_TILES";
        public static string DESTOYER_EXPLOSION = "DESTOYER_EXPLOSION";
        public static string EXPLOSION_IS_RIGHT = "EXPLOSION_IS_RIGHT";
        public static string DECREASE_MOVE_COUNT = "DECREASE_MOVE_COUNT";
        public static string CHECK_FOR_GAME_GOALS = "CHECK_FOR_GAME_GOALS";
        public static string GOAL_IS_ACHIEVED = "GOAL_IS_ACHIEVED";
        


    }

   
    

}