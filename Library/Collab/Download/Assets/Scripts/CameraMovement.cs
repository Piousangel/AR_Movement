using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public float rotSpeed = 200;
    

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        Vector3 dir = new Vector3(-v, h, 0);
        Vector3 angle = transform.eulerAngles;
        angle += dir * rotSpeed * Time.deltaTime;


        transform.eulerAngles = angle;

        transform.position = player.position + new Vector3(0,3.5f,0);
    }
}
