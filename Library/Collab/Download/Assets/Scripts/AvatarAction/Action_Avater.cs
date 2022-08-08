using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Action_Avatar : MonoBehaviour
{
    public GameObject player;
    public float extraRotationSpeed = 5f;

    private string event_Player_now;
    private EventTrigger_Player script_player;
    private NavMeshAgent nav;
    private Transform destination;

    private Transform avatar_model;
    private Animator avatar_animation;
    public Transform_Avatar avatar_transform;

    private GameObject used_object;


    // 명령된 함수들을 저장하여 우선순위의 함수부터 실행하기 위한 리스트입니다.
    
    private List<KeyValuePair<string, GameObject>> PairList = new List<KeyValuePair<string, GameObject>>();


    private void Start()
    {
        InitNavMeshAgent();
        script_player = player.GetComponent<EventTrigger_Player>();
        Move2Player();
        avatar_model = this.GetComponent<Transform>();
        avatar_animation = this.GetComponentInChildren<Animator>();
        avatar_transform = this.GetComponent<Transform_Avatar>();
    }

    private void InitNavMeshAgent()
    {
        nav = this.GetComponent<NavMeshAgent>();
        nav.speed = 6;
        nav.SetDestination(this.transform.position);
        nav.acceleration = 60;
        nav.updateRotation = false;
    }
    private void FixedUpdate()
    {
        //Debug.Log(destination);
        if(destination != null)
        {
            Debug.Log(destination);
            Vector3 move_dir = destination.position - this.transform.position;
            Debug.DrawLine(avatar_model.position + new Vector3(0, 3, 0), destination.position + new Vector3(0, 1, 0), Color.red);
            Debug.DrawLine(avatar_model.position + new Vector3(0, 3, 0), (avatar_model.position + avatar_model.rotation.eulerAngles + new Vector3(0, 1, 0)), Color.green);
            if (nav.enabled)
            {
                nav.SetDestination(destination.position);
                avatar_animation.SetFloat("avatar_speed", nav.velocity.magnitude);
                Quaternion rot_select = Camera.main.transform.rotation;
                avatar_transform.SetStandAnimation(rot_select);
                avatar_transform.SetWalkAnimation(Quaternion.LookRotation(move_dir));
            }
        }
    }

    private float GetDistance_xz(Vector3 pos1, Vector3 pos2)
    {
        Vector3 pos1_xz = new Vector3(pos1.x, 0, pos1.z);
        Vector3 pos2_xz = new Vector3(pos2.x, 0, pos2.z);
        
        return Vector3.Distance(pos1_xz, pos2_xz);
    }

    public void AddList(KeyValuePair<string, GameObject> event_Player)
    {
        PairList.Add(event_Player);
    }
    public void SortList()
    {

    }
    public void Act_Avatar(KeyValuePair<string, GameObject> event_player)
    {
        //Debug.Log(event_player + " " + action_avatar);

        //Debug.Log(event_Player_now + " " + action_avatar + " on " + event_player.Value);
        
        
        if (used_object != null)
        {
            if (used_object != event_player.Value && event_Player_now == "IsObject")
            {
                OnExitObject(used_object);
                Move2Object(event_player.Value);
                return;
            }
        }
        event_Player_now = event_player.Key;
        switch (event_Player_now)
        {
            case "Free":
                OnExitObject(event_player.Value);
                break;
                //switch (action_avatar)
                //{
                //    case "Move2Player":
                //        break;
                //    case "Move2Object":
                //        Move2Player(event_player.Value);
                //        break;
                //    case "OnObject":
                //        OnExitObject(event_player.Value);
                //        break;
                //}
                //break;
            case "IsObject":
                Move2Object(event_player.Value);
                break;
                //if(!event_player.Value.GetComponent<Action_Object>().IsUsing(this.gameObject))
                //{
                //    event_player.Value.GetComponent<Action_Object>().Using_Object(this.gameObject);
                //    used_object = event_player.Value;
                //    switch (action_avatar)
                //    {
                //        case "Move2Player":
                //            Move2Object(event_player.Value);
                //            break;
                //        case "Move2Object":
                //            Debug.Log("HI!");
                //            OnObject(event_player.Value);
                //            break;
                //        case "OnObject":
                //            break;
                //    }
                //    break;
                //}

        }
    }

    private KeyValuePair<string, GameObject> startpoint = new KeyValuePair<string, GameObject>();
    public void Move2Object(GameObject obj)
    {
        startpoint = obj.GetComponent<Action_Object>().GetStartPoint(this.transform.position);
        StartCoroutine(IsAvaterMove2Object(startpoint, obj));
    }

    public GameObject cur_obj;
    IEnumerator IsAvaterMove2Object(KeyValuePair<string, GameObject> startpoint, GameObject obj)
    {
        do
        {
            yield return null;
        } while (!mutex_exit);

        used_object = obj;
        destination = startpoint.Value.transform;
        script_player.Set_action_Avatar("Move2Object");

        Vector3 target = startpoint.Value.transform.position;
        while (GetDistance_xz(this.transform.position,target) >= 0.5f && event_Player_now == "IsObject")
        {
            //Debug.Log(event_Player_now);
            yield return new WaitForFixedUpdate();
        }
        OnObject(obj);
    }

    public void OnObject(GameObject obj)
    {
        //Debug.Log(obj.tag);
        switch (obj.tag)
        {
            case "Chair":
                StartCoroutine(OnObjectAnimation(1f, obj.transform.rotation, "Chair_On"));
                break;

            case "Bed":
                //Vector3 obj_xz = new Vector3(obj.transform.position.x, 0, obj.transform.position.z);
                //Vector3 this_xz = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                //Quaternion target = Quaternion.LookRotation(this_xz -obj_xz);
                //
                //if (Vector3.Cross(obj.transform.forward, (obj_xz - this_xz)).y < 0) 
                //{
                //    StartCoroutine(OnObjectAnimation(1f, target, "Bed_Left"));
                //}
                //else
                //{
                //    StartCoroutine(OnObjectAnimation(1f, target, "Bed_Right"));
                //}
                //
                //StartCoroutine(OnObjectAnimation(1f, target, "Bed_On"));
                break;
            case "Desk":
                break;
        }

        
    }

    IEnumerator OnObjectAnimation(float delay,Quaternion target_rotation, string trigger )
    {
        float time_t = 0;
        GameObject temp_obj = used_object;
        do
        {
            if (event_Player_now != "IsObject" || temp_obj != used_object)
            {
                yield break;
            }
            time_t += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        } while (time_t < delay);

        
        this.GetComponent<NavMeshAgent>().enabled = false;
        yield return StartCoroutine(avatar_transform.Rotate_On_Object(target_rotation));

        avatar_animation.SetTrigger(trigger);
        do
        {
            yield return null;
        } while (!avatar_animation.GetCurrentAnimatorStateInfo(0).IsName(trigger));
        script_player.Set_action_Avatar("OnObject");
        
    }

    public void OnExitObject(GameObject obj)
    {
        if(obj != null)
        {
            obj.GetComponent<Action_Object>().Using_Object(null);

            mutex_exit = false;
            switch (obj.tag)
            {
                case "Chair":
                    StartCoroutine(OnExitObjectAnimation(obj, "Chair_Exit"));
                    break;
                case "Bed":
                    //StartCoroutine(OnExitObjectAnimation(obj, "Bed_Exit"));
                    Move2Player();
                    break;
                case "Desk":
                    Move2Player();
                    break;
            }
        }

    }

    private bool mutex_exit= false;
    IEnumerator OnExitObjectAnimation(GameObject obj, string trigger)
    {
        this.GetComponent<NavMeshAgent>().enabled = false;
        
        avatar_animation.SetTrigger(trigger);
        script_player.Set_action_Avatar("OnObject");

        do
        {
            yield return null;
        } while (!avatar_animation.GetCurrentAnimatorStateInfo(0).IsName("Idle"));

        this.GetComponent<NavMeshAgent>().enabled = true;
        Move2Player();
    }

    public void Move2Player()
    {
        destination = player.transform;
        script_player.Set_action_Avatar("Move2Player");

        mutex_exit = true;
    }

}
