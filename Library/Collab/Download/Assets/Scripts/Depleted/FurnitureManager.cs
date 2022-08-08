using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{

    private Transform[] furniture;


    private void Start()
    {
        furniture = GetChildrens(GameObject.FindObjectOfType<MapManager>().obstacle_parent);

        //foreach (Transform child in furniture)
        //{
        //    child.gameObject.SetActive(false);
        //    var trigger = child.gameObject.AddComponent<SampleTriggerCheck>();
        //    trigger.script_AI = GameObject.Find("AI_chase").GetComponent<AI_chase>();
        //    child.gameObject.SetActive(true);
        //}
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
}
