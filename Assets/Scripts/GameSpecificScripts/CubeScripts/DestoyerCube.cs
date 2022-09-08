using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyerCube : Cube
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public virtual void  DestroyGrids()
    {
        EventManager.TriggerEvent(GameConstants.GameEvents.DECREASE_MOVE_COUNT, new EventParam());
    }


    public void DestroyerAnimation()
    {
        CubeAnimator.SetTrigger("Destroy");
        StartCoroutine(DestroySelf());
    }


    IEnumerator DestroySelf()
    {
        SelfCollider.enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    
}
