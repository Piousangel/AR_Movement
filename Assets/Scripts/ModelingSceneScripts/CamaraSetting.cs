using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSetting : MonoBehaviour
{
    private Transform cam;
    public GameObject _obj;
    public float speed = 2f;
    //추적할 대상
    public Transform target;
    //카메라와의 거리
    public float dist = 4f;

    
    public Quaternion TargetRotation;  // 최종적으로 축적된 Gap이 이 변수에 저장됨.
    public Transform CameraVector;

    public float RotationSpeed;        // 회전 스피드.
    public float ZoomSpeed;            // 줌 스피드.
    public float Distance;             // 카메라와의 거리.

    private Vector3 AxisVec;           // 축의 벡터.
    private Vector3 Gap;               // 회전 축적 값.

  
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();

        target = GameObject.Find("Moar").transform; //동작 확인용        
    }

    void Update()
    {
        //CameraRotation();
    }
   

    public void SettingCamara()
    {
        cam.transform.position = new Vector3(0, 2, -0.8f);
        Debug.Log(cam.transform.rotation);

        cam.transform.rotation = new Quaternion(0.3f, 0f, 0.0f, 3.0f);
        
        /*if (Input.GetMouseButton(0))
        {
            this.transform.RotateAround(_obj.transform.position, Vector3.up, -Input.GetAxis("Mouse X") * speed);
            this.transform.RotateAround(_obj.transform.position, Vector3.right, -Input.GetAxis("Mouse Y") * speed);
        }*/
    }

    public void camTransform()
    {
        cam.transform.position = new Vector3(0.3f, 0.9f, -3);
        cam.transform.rotation = Quaternion.Euler(-8f, 27f, 0);
    }

    public void EmotionCamara()
    {
        Debug.Log("Emotion Camera!!");
        cam.transform.position = new Vector3(0, 1, -10);
        
    }

    public void ShortZoom()
    {
        camTransform();
    }


    public void ExitSettingCamara()
    {
        camTransform();
    }

    public void AnimationCamara()
    {
        camTransform();
    }

    public void ModelingCamara()
    {
        camTransform();
    }

    // 카메라 회전.
    void CameraRotation()
    {

        cam.transform.position = new Vector3(target.position.x , target.position.y + 2, target.position.z - Distance);

        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector3 camAngle = cam.rotation.eulerAngles;

        float x = camAngle.x + mouseDelta.y;

        //Debug.Log(x);


        if (x < 180f)  //위쪽으로 회전하는 경우
        {
            x = Mathf.Clamp(x, -10f, 1f);
        }
        else           //아랫쪽으로 회전하는 경우
        {
            x = Mathf.Clamp(x, 355f, 361f);
        }


        cam.rotation = Quaternion.Euler(x, camAngle.y + (mouseDelta.x * 0.07f), camAngle.z);

    }

}
