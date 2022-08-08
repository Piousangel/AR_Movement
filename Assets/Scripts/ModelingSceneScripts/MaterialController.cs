using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{

    public Material[] groundmaterials;
    public Material[] ppmaterial;

    SkinnedMeshRenderer BodymeshRenderer;
    SkinnedMeshRenderer ArmmeshRenderer;
    SkinnedMeshRenderer HeadmeshRenderer;
    MeshRenderer BackgroundMaterial;

    void Start()
    {
        BodymeshRenderer = GameObject.FindGameObjectWithTag("MoarBody").transform.GetComponent<SkinnedMeshRenderer>();
        ArmmeshRenderer = GameObject.FindGameObjectWithTag("MoarArm").transform.GetComponent<SkinnedMeshRenderer>();
        HeadmeshRenderer = GameObject.FindGameObjectWithTag("MoarHead").transform.GetComponent<SkinnedMeshRenderer>();


        BackgroundMaterial = GameObject.Find("Plane").GetComponent<MeshRenderer>();

        //for(int i=0; i < gameObjects.Length; i++)
        //{
        //    gameObjects[i].AddComponent<MaterialController>();
        //}
        

    }

    public void Change_Materials(string names)
    {
        Debug.Log("Chnage_Materials Clicked!!!");


        for (int i = 0; i < groundmaterials.Length; i++)
        {
            Debug.Log(groundmaterials[i].name);

            if (groundmaterials[i].name == names)
            {
                BackgroundMaterial.material = groundmaterials[i];
                Debug.Log("Same!!!!!");
            }
        }

    }

    public void Change_Body(string names)
    {
        Debug.Log(names);

        for(int i=0; i < ppmaterial.Length; i++)
        {
            if(ppmaterial[i].name == names)
            {
                BodymeshRenderer.material = ppmaterial[i];
                ArmmeshRenderer.material = ppmaterial[i];
            }
        }
    }

    public void Change_Head(string names)
    {
        for (int i = 0; i < ppmaterial.Length; i++)
        {
            if (ppmaterial[i].name == names)
            {
                Debug.Log(names);
                HeadmeshRenderer.material = ppmaterial[i];
            }
        }
    }
}
