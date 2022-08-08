using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class File_Viewer_Click : MonoBehaviour
{
    FileManager script;
    
    //BackButton backButton;

    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Script").GetComponentInChildren<FileManager>();

        //backButton = GameObject.FindGameObjectWithTag("Script").GetComponentInChildren<BackButton>();



    }

    private void Update()
    {
        
    }

    public void onClick_DIR()
    {
        //backButton.newBtn();
        script.clear_content();
        Debug.Log(script.path_temp + "/" + this.transform.parent.GetComponentInChildren<Text>().text);
        script.file_viewer(script.path_temp + "/" + this.transform.parent.GetComponentInChildren<Text>().text);
        
    }


    /*         애니메이터 관련 부분          */
    public void onClick_Animator()      
    {
        if (this.transform.parent.GetComponentInChildren<Text>().text.Contains(".fbx"))
        {
            Debug.Log(this.transform.parent.GetComponentInChildren<Text>().text);
            script.play_animator(this.transform.parent.GetComponentInChildren<Text>().text);
        }
    }

    /*         표정 관련 부분          */
    public void onClick_Emotion()
    {

        //Debug.Log("onClick_Emotion()!!!!!!");
        //if (script.path_temp.Split(Path.AltDirectorySeparatorChar).Last() == "Emotion")
        //{
        //    camaraSetting.EmotionCamara();

        //}
    }

    /*          Modeling 관련 부분          */
    public void onClick_Character()
    {
        if (this.transform.parent.GetComponentInChildren<Text>().text.Contains("Moar"))
        {
            script.change_character(this.transform.parent.GetComponentInChildren<Text>().text);
        }
    }

    public void onClick_Dress()
    {
        if (script.path_temp.Split(Path.AltDirectorySeparatorChar).Last() == "Body") //사실 이게 잘 먹는지는 모르겠습니다...
        {
            script.change_Body_Materials(this.transform.parent.GetComponentInChildren<Text>().text);
        }

        else if(script.path_temp.Split(Path.AltDirectorySeparatorChar).Last() == "Head")
        {
            script.change_Head_Materials(this.transform.parent.GetComponentInChildren<Text>().text);
        }    
    }

    public void onClick_BackGround()
    {
        if (this.transform.parent.GetComponentInChildren<Text>().text.Contains("Ground"))
        {
            Debug.Log(this.transform.parent.GetComponentInChildren<Text>().text);
            script.change_Ground_Materials(this.transform.parent.GetComponentInChildren<Text>().text);
        }

        
        
    }

    
}

