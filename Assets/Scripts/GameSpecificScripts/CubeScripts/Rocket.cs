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

    public override void DestroyGrids()
    {
        base.DestroyGrids();
    }

  
}
