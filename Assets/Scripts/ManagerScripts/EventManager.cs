using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
 
    private Dictionary<string, Action<EventParam>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<EventParam>>();
        }
    }

    public static void StartListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            
            thisEvent += listener;

           
            instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            
            thisEvent += listener;
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<EventParam> listener)
    {
        if (eventManager == null) return;
        Action<EventParam> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
           
            thisEvent -= listener;

           
            instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, EventParam eventParam)
    {
        Action<EventParam> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
            
        }
    }
}


public class EventParam
{
   

    public GameObject paramObj;
    public int paramInt;
    public string paramStr;
    public Type paramType;
    
    public Dictionary<string, object> paramDictionary;



    private  Cube SelectedColorCube;
    private  List<List<TileGrid>> TileMatrix = new List<List<TileGrid>>();
    private  List<ColorCube> ColorCubesToBeDeleted = new List<ColorCube>();
    private  List<AffectedByExplosionCube> AffectedByExplosionCubesToBeDeleted = new List<AffectedByExplosionCube>();
    Dictionary<int, List<int>> EmptyTiles = new Dictionary<int, List<int>>();
    Dictionary<int, int> EmptyTilesCounts = new Dictionary<int, int>();
    public void SetEmptyTiles(Dictionary<int, List<int>> empty_tiles)
    {
        EmptyTiles = empty_tiles;
    }

    public Dictionary<int, List<int>> GetEmptyTiles()
    {
        return EmptyTiles;
    }

    public void SetEmptyTilesCount(Dictionary<int, int> empty_tiles_counts)
    {
        EmptyTilesCounts =empty_tiles_counts;
    }

    public Dictionary<int, int> GetEmptyTilesCount()
    {
        return EmptyTilesCounts;
    }
    public void SetSelectedCube(ColorCube selected_cube)
    {
        SelectedColorCube = selected_cube;
    }

    public Cube GetSelectedCube()
    {
        return SelectedColorCube;
    }


    public void  SetTileMatrix(List<List<TileGrid>> tile_matrix)
    {
        TileMatrix = tile_matrix;
    }

    public List<List<TileGrid>> GetTileMatrix()
    {
        return TileMatrix;
    }

    public List<ColorCube> GetColorCubesToBeDeleted()
    {
        return ColorCubesToBeDeleted;
    }
    public void SetColorCubesToBeDeleted(List<ColorCube> color_cubes)
    {
        ColorCubesToBeDeleted = color_cubes;
    }

    public List<AffectedByExplosionCube> GetAffectedByExplosionCubesToBeDeleted()
    {
        return AffectedByExplosionCubesToBeDeleted;
    }
    public void SetAffectedByExplosionCubesToBeDeleted(List<AffectedByExplosionCube> affectable_cubes)
    {
        AffectedByExplosionCubesToBeDeleted = affectable_cubes;
    }
    public EventParam()
    {

    }

    public EventParam(Dictionary<string, object> paramDictionary)
    {
        this.paramDictionary = paramDictionary;
    }

    public EventParam(GameObject paramObj = null, int paramInt = 0, string paramStr = "", Type paramType = null, Dictionary<string, object> paramDictionary = null)
    {
            
        this.paramObj = paramObj;
        this.paramInt = paramInt;
        this.paramStr = paramStr;
        this.paramType = paramType;
        this.paramDictionary = paramDictionary;
        
    }
}



