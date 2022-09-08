using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Duck : BottomCubes
{
    // Start is called before the first frame update
    bool IsFall = false;
    private AudioSource DuckSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool CheckFalseClick()
    {
        bool return_value = base.CheckFalseClick();
        return false;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ceiling" && !IsFall)
        {
            DuckSound = GetComponent<AudioSource>();
            DuckSound.Play();
            IsFall = true;
            SendMessage();
        }
    }


    void SendMessage()
    {
        Dictionary<int, List<int>> index_dict = new Dictionary<int, List<int>>();
        Dictionary<int, int> count_dict = new Dictionary<int, int>();
        Point point = GetParentTile().GetMatrixPoint();
        //int point_x = point.GetMatrixIndexX();
        int point_y = point.GetMatrixIndexY();

        List<int> indexes = new List<int>();
        indexes.Add(0);
        index_dict.Add(point_y, indexes);
        count_dict.Add(point_y, 1);

        EventParam param = new EventParam();
        param.SetEmptyTiles(index_dict);
        param.SetEmptyTilesCount(count_dict);
        EventManager.TriggerEvent(GameConstants.GameEvents.START_FILLING_EMPTY_TILES, param);
        GetParentTile().RemoveCubeFromTile();
        //Destroy(gameObject);
        StartCoroutine(RemoveFromBottom());
    }

    IEnumerator RemoveFromBottom()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject particle_effect = Instantiate(ParticleEffectPrefab, Vector2.zero, Quaternion.identity);
        particle_effect.transform.parent = transform;
        particle_effect.transform.localScale /= 3;
        particle_effect.transform.localPosition = Vector2.zero;
        particle_effect.transform.parent = null;
        CheckForGameGoal();
        Destroy(gameObject);
       
        
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
