using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class TriggerCheck : MonoBehaviour
{
    public AI_chase script_AI;

    [Tooltip("?????? ???? ?? ???????? ?????? ?????? ??????????.")]
    public float wait_delay_in = 2;
    [Tooltip("?????? ???? ?? ???????? ?????? ?????? ??????????.")]
    public float wait_delay_out = 2;
    [Tooltip("?????????? ???? ?????????? string???? ??????????.")]
    public string player_interaction;

    private float time_in = 0;           //enter???? ???
    private float time_out = 0;          //exit???? ???
    private GameObject avatar; 
    private bool onExit = false;

    private void Start()
    {
        avatar = GameObject.Find("Member").transform.GetChild(1).gameObject;
        script_AI = GameObject.FindObjectOfType<AI_chase>();
        player_state = "None";
        player_interaction = "";

        //time_T.Add("time_in", 0);
        //time_T.Add("time_out", 0);
    }

    private string player_state;

    private void FixedUpdate()
    {
        StartCoroutine(PlayerAction(player_state, player_interaction));
        Debug.Log(player_state);
    }

    /*private void UseOverlapsShphere()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 10f, 1 << 8);

        if (cols.Length > 0)
        {

            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == "Player")
                {
                    player_state = "Stay";
                }
            }
        }
        else
        {
            player_state = "Exit";
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player_state = "Enter";
        }
    }          

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player_state = "Stay";
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player_state = "Exit";
        }
    }


    IEnumerator PlayerAction(string state, string action)     //string action? 
    {
        switch (state)
        {
            case "Enter":
                //time_in = 0;
                //time_out = 0;
                //Debug.Log(time_in);
                //action = "Player Enter " + this.transform.name;
                //Debug.Log(action);
                break;

            case "Stay":
              
                time_in += Time.deltaTime;
                if (time_in > wait_delay_in)
                {
                    action = "avatar on " + this.transform.name;
                    Debug.Log(action);
                    Vector3 scale = avatar.transform.lossyScale;
                    avatar.transform.SetParent(this.transform);    //chair
                    float temp_y = avatar.transform.position.y;
                    avatar.transform.localPosition = new Vector3(0, 0, 0);
                    avatar.transform.localScale = new Vector3(scale.x / this.transform.lossyScale.x, scale.y / this.transform.lossyScale.y, scale.z / this.transform.lossyScale.z);
                    avatar.transform.position = new Vector3(avatar.transform.position.x, temp_y, avatar.transform.position.z);
                    avatar.transform.rotation = this.transform.rotation;
                    avatar.GetComponentInChildren<NavMeshAgent>().enabled = false;
                    onExit = true;
                    time_out = 0;     
                }
                break;

            case "Exit":

                time_in = 0;
                time_out += Time.deltaTime;
                if (time_out > wait_delay_out && onExit == true )   //&& time_in > wait_delay_in
                {
                    action = "avatar out " + this.transform.name;
                    Debug.Log(action);
                    avatar.transform.SetParent(GameObject.Find("Member").transform);
                    avatar.transform.position = this.transform.position + this.transform.forward * 3;
                    avatar.GetComponentInChildren<NavMeshAgent>().enabled = true;
                    player_state = "None";
                    time_out = 0;
                    onExit = false;
                   
                }
                break;


        }
        yield return new WaitForFixedUpdate();
    }

    

}
