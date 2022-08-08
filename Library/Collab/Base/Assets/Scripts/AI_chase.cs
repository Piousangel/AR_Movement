using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_chase : MonoBehaviour
{
    public GameObject target;
    public Transform npc_trans;
    float running_time = 0;
    float temp_time = 0;


    [Tooltip("???????? ?????? ??????????. ?????? 12 ??????.")]
    public float max_speed = 12f;

    [Tooltip("???????? ???????? ??????????. ?????? 100 ??????.")]
    public float acceleration = 100f;
    [Tooltip("???????? ???????????? ??????????. ?????? 10 ??????.")]
    public float extraRotationSpeed = 10f;
    private NavMeshAgent nav;
    void Start()
    {
        nav = npc_trans.GetComponent<NavMeshAgent>();
        nav.updateRotation = false;
        nav.acceleration = acceleration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(target.transform.position, npc_trans.position) > 0.1f && nav.enabled)
        {
            nav.speed = max_speed;
            nav.SetDestination(target.transform.position);

            Vector3 lookrotation = target.transform.position - npc_trans.transform.position;
            
            npc_trans.rotation = Quaternion.Slerp(npc_trans.transform.rotation, Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);
            //Debug.Log("target: " + target.transform.rotation.eulerAngles + "Quarternion: " + Quaternion.LookRotation(lookrotation).eulerAngles + "npc: " + npc_trans.rotation.eulerAngles);
            //npc_trans.rotation = target.transform.rotation;
        }
    }
}
