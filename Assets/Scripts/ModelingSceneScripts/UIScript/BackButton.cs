using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class BackButton : MonoBehaviour
{
    public GameObject ScrollView;
    public GameObject SettingUI;

    GameObject Moar;
    GameObject[] CharPrefabs;

    FileManager script;
    

    private void Start()
    {
        script = GameObject.Find("Script").GetComponentInChildren<FileManager>();
        Moar = GameObject.Find("Moar");
        CharPrefabs = GameObject.Find("CharacterPrefab").GetComponent<CharacterChangeController>().charPrefab;
    }

    public void onClick_Back()
    {
        script.clear_content();

        //Debug.Log("script.path_temp" + script.path_temp);
        //tring path = File.ReadAllText(Path.Co)
        //script.file_viewer(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(script.path_temp)));

        if (script.path_temp.Split(Path.AltDirectorySeparatorChar).Last() == "Modeling" || script.path_temp.Split(Path.AltDirectorySeparatorChar).Last() == "Animation")  //주소의 마지막 이름으로 백버튼 활성화 비활성화 할꺼...
        {
            Debug.Log("Can not Back!!");
            ScrollView.SetActive(false);
            SettingUI.SetActive(true);
        }

        if(script.path_temp.Split(Path.AltDirectorySeparatorChar).Last() == "Character")
        {
            if(Moar.activeSelf == false)
            {
                for(int i=0; i < CharPrefabs.Length; i++)
                {
                    CharPrefabs[i].SetActive(false);
                }
                Moar.SetActive(true);
            }
        }

        script.file_viewer(System.IO.Path.GetDirectoryName(script.path_temp));

    }

}
