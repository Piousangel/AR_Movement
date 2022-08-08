using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Camera maincam;
    public float speed = 9f;


    private void Awake()
    {
        
    }

    private void Update()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");
        Vector3 pos = this.transform.position;
        pos = pos + new Vector3(_horizontal * Time.deltaTime * speed, 0, _vertical * Time.deltaTime * speed);
        this.transform.position = pos;

        Vector3 direction = new Vector3(_horizontal, 0, _vertical);

        if(direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0,5,0), 10 * Time.fixedDeltaTime);
        }

        this.transform.LookAt(pos + direction);
        
        

        
    }
}
