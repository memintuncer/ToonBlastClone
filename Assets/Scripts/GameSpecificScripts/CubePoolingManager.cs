using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePoolingManager : MonoBehaviour
{
    [SerializeField] int PoolingCubesCount;
    [SerializeField] GameObject[] AllCubes;
    [SerializeField] Transform PoolTransform;

    private List<GameObject> PooledCubes = new List<GameObject>();


    public List<GameObject> GetPooledCubes() 
    {
        return PooledCubes;
    }


    public void SendCubesToPool(List<GameObject> cubes_list_to_pool)
    {
        for(int i = 0; i < cubes_list_to_pool.Count; i++)
        {
            PooledCubes.Add(cubes_list_to_pool[i]);
            cubes_list_to_pool[i].SetActive(false);
        }
    }

    public void RemoveCubesFromPool(List<GameObject> cubes_list_to_remove_from_pool)
    {
        for(int i =0;i< cubes_list_to_remove_from_pool.Count; i++)
        {
            PooledCubes.Remove(cubes_list_to_remove_from_pool[i]);
        }
    }

    public void CreateCubesPool()
    {
        int random_index = 0;
        for(int i =0; i<PoolingCubesCount;i++)
        {
            random_index = Random.Range(0, AllCubes.Length);
            GameObject pooling_cube = Instantiate(AllCubes[random_index], Vector2.zero, Quaternion.identity);
            pooling_cube.transform.parent = PoolTransform;
            pooling_cube.SetActive(false);
        }
    }
}
