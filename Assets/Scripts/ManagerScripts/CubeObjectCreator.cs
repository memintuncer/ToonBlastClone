using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObjectCreator : MonoBehaviour
{
    [SerializeField] GameObject[] CubeObjects;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }



    public GameObject CreateCubeObject()
    {
        int random_cube_index = Random.Range(0, CubeObjects.Length);
        GameObject new_cube_object = Instantiate(CubeObjects[random_cube_index], Vector2.zero, Quaternion.identity);
        return new_cube_object;
    }
}
