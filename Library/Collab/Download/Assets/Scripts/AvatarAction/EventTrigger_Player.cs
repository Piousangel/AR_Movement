using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EventTrigger_Player : MonoBehaviour
{
    private string[] events_Player = { "Free", "IsObject"  };
    private KeyValuePair<string, GameObject> event_Player;

    private string[] actions = { "Move2Player", "Move2ObjPoint","OnObject",  };
    private string action_Avatar;


    public GameObject avatar;
    private string cur_act;
    private GameObject cur_obj;

    private void Awake()
    {
        event_Player = new KeyValuePair<string, GameObject>("Free", null);
        action_Avatar = "Move2Player";
        cur_act = "";
        cur_obj = this.gameObject;
        InitAvatar();
    }

    float index = 0;
    private void FixedUpdate()
    {
        
        if(event_Player.Value != null)
        {
            if(cur_act != event_Player.Key || cur_obj != event_Player.Value)
            {
                cur_act = event_Player.Key;
                cur_obj = event_Player.Value;
                event_Player.Value.GetComponent<Action_Object>().Act_Object(event_Player, action_Avatar);
                avatar.GetComponent<Action_Avatar>().Act_Avatar(event_Player);
            }    
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Action_Object>() != null)
            event_Player = new KeyValuePair<string, GameObject>("IsObject", other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Action_Object>() != null) 
            event_Player = new KeyValuePair<string, GameObject>("Free", other.gameObject);
    }

    public void Set_action_Avatar(string act)
    {
        action_Avatar = act;
        if (event_Player.Key == "Free" && action_Avatar == "Move2Player")
        {
            event_Player = new KeyValuePair<string, GameObject>("Free", null);
        }
    }

    private void InitAvatar()
    {
        avatar.AddComponent<NavMeshAgent>();
        avatar.AddComponent<Action_Avatar>();
        avatar.GetComponent<Action_Avatar>().player = this.gameObject;
    }
}
