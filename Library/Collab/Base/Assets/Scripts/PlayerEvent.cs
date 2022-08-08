using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class PlayerEvent : MonoBehaviour
{
    private Transform player;
    private Transform[] furniture;
    private Dictionary<Transform, float> distance_dict;
    private GameObject min;
    


    private void Start()
    {
        
        player = GameObject.Find("Member").transform.GetChild(0).transform;
        furniture = GetChildrens(GameObject.FindObjectOfType<MapManager>().obstacle_parent);

    }

    private bool IsRangeOut_switch = false;
    private void Update()
    {

        if (IsRangeIn())
        {
            min.GetComponent<ObjectBehavior>().Call_Action("OnRangeIn", IsRangeIn);
            IsRangeOut_switch = true;
        }
        if (IsRangeOut_switch && IsRangeOut())
        {
            IsRangeOut_switch = false;
            Debug.Log(min);
            min.GetComponent<ObjectBehavior>().Call_Action("OnRangeOut", IsRangeOut);
        }

    }

    public Transform[] GetChildrens(Transform parent)
    {
        Transform[] children = new Transform[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            children[i] = parent.GetChild(i);
        }

        return children;
    }

    private bool IsRangeIn()
    {
        
        float min_distance = 1000;

        Vector3 player_pos_xy = new Vector3(player.position.x, 0, player.position.z);

        foreach (Transform child in furniture)
        {
            float distance = Vector3.Distance(player_pos_xy, new Vector3(child.position.x, 0, child.position.z));
            if ( min_distance > distance)
            {
                min_distance = distance;
                min = child.gameObject;
            }
        }

        if (min_distance < 1) return true;

        return false;
    }

    private bool IsRangeOut()
    {
       
        Vector3 player_pos_xy = new Vector3(player.position.x, 0, player.position.z);
        float distance = Vector3.Distance(player_pos_xy, new Vector3(min.transform.position.x, 0, min.transform.position.z));
        if (distance > 1) return true;
          
        return false;
    }

    public bool Get_Action(ObjectBehavior.IsEvent isEvent)
    {
        return isEvent();
    }

    

   


   


}

