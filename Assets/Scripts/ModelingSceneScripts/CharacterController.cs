using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject[] gameObjects;

    void Start()
    {
        InputAnimator();
    }

    void InputAnimator()
    {
        for(int i=0; i< gameObjects.Length; i++)
        {
            gameObjects[i].AddComponent<ModelController>();

            if(gameObjects[i].name == "Moar")
            {
                gameObjects[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load("MoarAnimatorController") as RuntimeAnimatorController;
            }
            
        }
    }
    
    void Update()
    {
        
    }
}
