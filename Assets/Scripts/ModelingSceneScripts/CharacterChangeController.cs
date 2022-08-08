using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChangeController : MonoBehaviour
{

    public GameObject[] charPrefab;

    GameObject Moar;


    void Start()
    {
        Moar = GameObject.Find("Moar");
    }

    public void characterOnOff(string name)
    {
        for(int i=0; i< charPrefab.Length; i++)
        {
            charPrefab[i].SetActive(false);
            Moar.gameObject.SetActive(false);

            if(charPrefab[i].name == name)
            {
                charPrefab[i].SetActive(true);
                charPrefab[i].transform.rotation = Quaternion.Euler(0, 180f, 0); //180도 회전시켜줌
            }
        }
    }
    
}
