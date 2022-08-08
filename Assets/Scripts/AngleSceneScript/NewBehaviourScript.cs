using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Transform Target;

    private void Start()
    {
        Target = GameObject.Find("Cube").transform;
    }

    private void Update()
    {
        Debug.Log(Quaternion.Euler(new Vector3(0,1, 0)));
        //this.transform.rotation = Quaternion.Euler(1, 1, 1);

    }


    void camMoving()
    {
        //this.transform.position = new Vector3(desta)
    }
}
