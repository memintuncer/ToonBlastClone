using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : AffectedByExplosionCube
{
    
    private AudioSource BaloonSound;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public override void  CheckExplosionCondition(Transform particle_parent)
    {
        base.CheckExplosionCondition(particle_parent);
        if(RequiredExplosionCount==0)
        {
            BaloonSound = GetComponent<AudioSource>();
            BaloonSound.Play();
            CheckForGameGoal();
            this.GetParentTile().RemoveCubeFromTile();
            
            DestroyCube(particle_parent);
            
           
        }
    }

    public void CheckForGameGoal()
    {
        List<GameGoal> game_goals = GameManager.GameGoals;
        for (int i = 0; i < game_goals.Count; i++)
        {
            GameGoal game_goal = game_goals[i];
            if (game_goal.GetGoalType().Equals(cubeType))
            {
                game_goal.DecreaseRequiredCount();
            }
        }


    }


}
