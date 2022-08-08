using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Camera maincam;
    public float speed = 9f;

    private float turnSmoothVelocity;
    private float turnSmoothTime;

    private void Awake()
    {
        turnSmoothTime = 0.1f;
    }

    private void Update()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");    
        float _vertical = Input.GetAxisRaw("Vertical");
        float cameraAngle = Camera.main.transform.eulerAngles.y;

        Vector3 direction = new Vector3(_horizontal, 0, _vertical);
        float moveAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraAngle;
        float angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, moveAngle, ref turnSmoothVelocity, turnSmoothTime);
        this.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDir_xz = Quaternion.Euler(0f, moveAngle, 0f) * Vector3.forward;
        
        Vector3 moveVec = moveDir_xz.normalized * speed * Time.deltaTime;
        if (HasAxisValue(_vertical) | HasAxisValue(_horizontal))
            this.transform.position = moveVec + this.transform.position;


       //Vector3 pos = this.transform.position;
       //float temp_speed = speed;
       //if (_horizontal != 0 && _vertical != 0)
       //    temp_speed = speed / 1.3f;
       //pos = pos + new Vector3(this.transform.rotation.eulerAngles.normalized.x * _horizontal * Time.deltaTime * temp_speed, 0, this.transform.rotation.eulerAngles.normalized.z * _vertical * Time.deltaTime * temp_speed);
       //this.transform.position = pos;
       //
       //
       //if (direction != Vector3.zero)
       //{
       //    float angle = Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg;
       //
       //    //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 5, 0), 10 * Time.fixedDeltaTime);
       //}
       //
       //this.transform.LookAt(pos + direction);
        
        
    }
    private bool HasAxisValue(float num)
    {
        bool value = false;
        if (num != 0) value = true;
        return value;
    }
}
