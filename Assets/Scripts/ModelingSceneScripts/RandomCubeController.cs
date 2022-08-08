using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCubeController : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform prefab_dir;
    public List<GameObject> cubeList;


    void Start()
    {
        
    }

    public void randomInit()
    {
        GameObject created_obj = Instantiate(cubePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        cubeList.Add(created_obj);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
