using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
    public Texture[] textures;

    RawImage rawImage;


    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    public void Change_RawImages(string type, string name)
    {
        

        if(type == "fbx")
        {
            rawImage.texture = textures[0];
        }
        else if(type == "mat")
        {
            rawImage.texture = textures[3];
            if (name.Contains("Body"))
            {
                if (name.Contains("Abokado"))
                {
                    rawImage.texture = textures[4];
                }
                else if (name.Contains("Takoyaki"))
                {
                    rawImage.texture = textures[5];
                }
                else if (name.Contains("Tiger"))
                {
                    rawImage.texture = textures[6];
                }
                else if (name.Contains("Hood"))
                {
                    rawImage.texture = textures[7];
                }
            }

            else if (name.Contains("Head"))
            {
                if (name.Contains("Abokado"))
                {
                    rawImage.texture = textures[8];
                }
                else if (name.Contains("Takoyaki"))
                {
                    rawImage.texture = textures[9];
                }
                else if (name.Contains("Tiger"))
                {
                    rawImage.texture = textures[10];
                }
                else if (name.Contains("Hood"))
                {
                    rawImage.texture = textures[11];
                }
            }
            

        }

        else if(type == "prefab")
        {
            rawImage.texture = textures[12];
        }



    }

   
}
