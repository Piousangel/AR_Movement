using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Action : MonoBehaviour
{
    private GameObject avatar;

    void Start()
    {
        avatar = GameObject.Find("Member").transform.GetChild(1).gameObject;
        ObjectBehavior ob = this.transform.GetComponent<ObjectBehavior>();
        ob.AddEventListener("OnRangeIn", 3, Enter_Action);
        ob.AddEventListener("OnTriggerExit", 1, Exit_Action);
        
    }


    public void Enter_Action()
    {      
            Debug.Log("avatar on " + this.transform.name);
            Vector3 scale = avatar.transform.lossyScale;
            avatar.transform.SetParent(this.transform);    //chair
            float temp_y = avatar.transform.position.y;
            avatar.transform.localPosition = new Vector3(0, 0, 0);
            avatar.transform.localScale = new Vector3(scale.x / this.transform.lossyScale.x, scale.y / this.transform.lossyScale.y, scale.z / this.transform.lossyScale.z);
            avatar.transform.position = new Vector3(avatar.transform.position.x, temp_y, avatar.transform.position.z);
            avatar.transform.rotation = this.transform.rotation;
            avatar.GetComponentInChildren<NavMeshAgent>().enabled = false;
    }

    void Exit_Action()
    {
        Debug.Log("avatar out " + this.transform.name);
        avatar.transform.SetParent(GameObject.Find("Member").transform);
        avatar.transform.position = this.transform.position + this.transform.forward * 3;
        avatar.GetComponentInChildren<NavMeshAgent>().enabled = true;
    }
    
}
