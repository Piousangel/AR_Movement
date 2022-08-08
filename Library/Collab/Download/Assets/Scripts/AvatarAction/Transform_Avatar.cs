using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform_Avatar : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("해당 숫자의 프레임 숫자만큼 회전합니다.")]
    public float extraRotationSpeed = 15f;

    [Tooltip("아바타의 몸체가 회전할 각도입니다.")]
    public float AvatarRotateAngle = 90f;

    public Transform world;
    public Transform model;
    public Transform head;
    public Transform body;
    public Transform leg;

    
    public void SetStandAnimation(Quaternion rotate_head)
    {
        head.rotation = Quaternion.Euler(new Vector3(0, rotate_head.eulerAngles.y, 0));
        //Debug.Log(rotate_head.eulerAngles + " " + Quaternion.Euler(new Vector3(0, rotate_head.eulerAngles.y, 0)));

        switch (LegDir(this.transform.rotation, rotate_head))
        {
            case "Right":
                //Debug.Log("Leg turn to Right side");
                StartCoroutine(Rotate_Transform(this.transform, rotate_head));
                break;
            case "Left":
                //Debug.Log("Leg turn to Left sied");
                StartCoroutine(Rotate_Transform(this.transform, rotate_head));
                break;
            case "Center":
                //Debug.Log("Leg do not rotate");
                break;
        }

    }
    public float SetWalkAnimation(Quaternion comparative)
    {
        float distance = GetRotateDirection(this.transform.rotation, comparative);
        distance = (distance + 360) % 360;

        this.GetComponentInChildren<Animator>().SetFloat("direction", distance);
        return distance;
    }

    //코루틴에서 실행되는 함수이므로 IEnumerator로 선언
    public IEnumerator Rotate_On_Object(Quaternion rotate_head)
    {
        StopAllCoroutines();
        mutex_rotate = false;
        yield return StartCoroutine(Rotate_Transform(this.transform, rotate_head));
    }


    private bool mutex_rotate = false;
    IEnumerator Rotate_Transform(Transform trans, Quaternion rot)
    {
        if (!mutex_rotate)
        {
            this.GetComponentInChildren<Animator>().SetFloat("rotate_stand", 1);
            mutex_rotate = true;
            int index = 0;
            do
            {
                //Debug.Log(index);
                trans.rotation = Rotate2TargetY(trans.rotation, rot, extraRotationSpeed - index);
                index++;
                yield return new WaitForFixedUpdate();
            } while (index != extraRotationSpeed);
            mutex_rotate = false;
            this.GetComponentInChildren<Animator>().SetFloat("rotate_stand", 0);
        }
            
    }
    private Quaternion Rotate2TargetY(Quaternion main, Quaternion target, float speed)
    {
        float rot_distance = Mathf.Abs(GetRotateDirection(main, target));

        float angle = (main.eulerAngles.y + rot_distance/speed) % 360;
        //Debug.Log(GetRotateDirection(main, target) > 0);
        if (GetRotateDirection(main, target) > 0)
        {
            angle = (main.eulerAngles.y + rot_distance/speed) % 360;
        }else
        {
            angle = (main.eulerAngles.y + 360 - rot_distance / speed) % 360;
            //Debug.Log(main.eulerAngles.y + " " + angle);
        }
        Vector3 rot_slashed = new Vector3(0, angle, 0);
        if (main == target) return target;
        return Quaternion.Euler(rot_slashed);
    }

    public string LegDir(Quaternion standard, Quaternion comparative)
    {
        float stn_y = standard.eulerAngles.y;
        float com_y = comparative.eulerAngles.y;

        string dir;
        float distance = GetRotateDirection(standard, comparative);
        if (distance > 0)
        {
            dir = "Right";
        }
        else
        {
            dir = "Left";
        }
        
        if (Mathf.Abs(distance) < AvatarRotateAngle)
        {
            return "Center";
        }
        return dir;
    }


    public float GetRotateDirection(Quaternion standard, Quaternion comparative)
    {
        float stn_y = standard.eulerAngles.y;
        float com_y = comparative.eulerAngles.y;


        float distance;
        if (Mathf.Abs(Mathf.Abs(com_y - stn_y) - 360 ) > Mathf.Abs(com_y - stn_y))
        {
            distance = com_y - stn_y;
        }
        else
        {
            if (com_y - stn_y < 0)
                distance = com_y - stn_y + 360;
            else
                distance = com_y - stn_y - 360;
        }

        //Debug.Log("Main: " + stn_y + " target: " + com_y + " angel: " + distance);
        return distance;
    }
}

