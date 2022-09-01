using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [SerializeField] int HorizontalGridCount, VerticalGridCount;
    public CubeObjectCreator CubeObjectCreator;
    [SerializeField] GameObject GridPrefab;
    [SerializeField] private Transform GridMatrix;
    void Start()
    {
        CreateGameGridMatrix();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CreateGameGridMatrix()
    {
       
    }


    public void GetGridMatrix()
    {
       
    }
}
