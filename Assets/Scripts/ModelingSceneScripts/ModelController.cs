using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModelController : MonoBehaviour
{
    
    public float speed = 2f;
    public float rotatespeed = 3f;
    private Animator animator;
    private Animator MoarAnimator;
    

    private void Start()
    {
        animator = GetComponent<Animator>();

        MoarAnimator = GameObject.Find("Moar").transform.GetComponent<Animator>();
    }

    private void Update()
    {

        if (this.name == "Robot Kyle")
        {
            KyleModelControl();
        }
        if (this.name == "Moar")
        {
            MoarControl();
        }
    }




    void KyleModelControl()
    {

        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");

        if (_horizontal == 0 && _vertical == 0)
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
        }


        else
        {
            animator.SetBool("isWalk", true);

            isRun();
            Vector3 currentPosition = this.transform.position;

            float realspeed = speed * Time.deltaTime;

            Vector3 pos = this.transform.position;

            pos = pos + new Vector3(_horizontal * Time.deltaTime * speed, 0, _vertical * Time.deltaTime * speed);
            this.transform.position = pos;

            Vector3 direction = new Vector3(_horizontal, 0, _vertical);


            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, angle, 0.0f), rotatespeed * Time.deltaTime);
            }

        }
    }

    void MoarControl()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");

        if (_horizontal == 0 && _vertical == 0)
        {
            MoarAnimator.SetBool("isWalk", false);
        }
        else
        {
            MoarAnimator.SetBool("isWalk", true);
            Vector3 currentPosition = this.transform.position;

            float realspeed = speed * Time.deltaTime;

            Vector3 pos = this.transform.position;

            pos = pos + new Vector3(_horizontal * Time.deltaTime * speed, 0, _vertical * Time.deltaTime * speed);
            this.transform.position = pos;

            Vector3 direction = new Vector3(_horizontal, 0, _vertical);


            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, angle, 0.0f), rotatespeed * Time.deltaTime);
            }
        }
        

        
    }

    void isRun()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isRun", true);
            
        }
        if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("isRun", false);
        }
    }

}
