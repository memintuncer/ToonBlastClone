using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixScaler : MonoBehaviour
{
    [SerializeField] Transform Border;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ScaleMatrix(int widht,int height)
    {
        Vector2 scale_vector = new Vector2((float)((float)widht / 2), (float)((float)height / 2));
        Vector2 position_vector = new Vector2(((float)widht / 5 + (widht/5)*0.1f), (float)(height / 5));
        Border.transform.localScale = scale_vector;
        Border.transform.localPosition = position_vector;
        if(height >10 || widht > 10)
        {
            if (widht > height)
            {
                transform.localScale = new Vector2((float)((float)widht / 25), (float)((float)height / 25));
            }
            else
            {
                transform.localScale = new Vector2((float)((float)height / 25), (float)((float)height / 25));
            }
            
        }
        //transform.localScale = 
        
    }
}
