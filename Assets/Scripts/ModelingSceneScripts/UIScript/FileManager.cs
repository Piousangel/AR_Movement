using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using System.Linq;

public class FileManager : MonoBehaviour
{
    private string game_dir_win = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
   

    private MaterialController BodyMaterialController;
    private MaterialController ArmMaterialController;
    private MaterialController HeadMaterialController;
    private MaterialController GroundMaterialController;

    private string animation_dir = "/Animation";
    private string modeling_dir = "/Modeling";

    private bool loopSelected = false;
    private bool _check = false;

    IconController iconController;
    CharacterChangeController characterChangeController;


    public string path_temp = "";
    public Transform Container;
    public Transform prefab_dir;
    public Transform prefab_bms;
    public List<Transform> contents;

    Animator animator;

    Dictionary<string, bool> settingBool;


    void Start()
    {
        //init_file_viewer();
        animator = GameObject.Find("Robot Kyle").GetComponent<Animator>();

        BodyMaterialController = GameObject.FindGameObjectWithTag("MoarBody").GetComponent<MaterialController>();
        ArmMaterialController = GameObject.FindGameObjectWithTag("MoarArm").GetComponent<MaterialController>();
        HeadMaterialController = GameObject.FindGameObjectWithTag("MoarHead").GetComponent<MaterialController>();

        GroundMaterialController = GameObject.Find("Plane").GetComponent<MaterialController>();

        iconController = GameObject.FindGameObjectWithTag("Icon").GetComponent<IconController>();
        characterChangeController = GameObject.Find("CharacterPrefab").GetComponent<CharacterChangeController>();


        settingBool = new Dictionary<string, bool>();
        setting();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setting()    //애니메이터에 넣어줄 boolname, bool/
    {
        settingBool.Add("isJump", false);
        settingBool.Add("isSitting", false);
        settingBool.Add("isSad", false);
        settingBool.Add("isRun", false);
    }


    public void animation_file_viewer()
    {
        clear_content();
        file_viewer(game_dir_win + "/Desktop/AR Navmesh/Assets/UISetting" + animation_dir);
    }

    public void modeling_file_viewer()
    {
        clear_content();
        file_viewer(game_dir_win + "/Desktop/AR Navmesh/Assets/UISetting" + modeling_dir);
    }

    
    public void file_viewer(string path)
    {
        path_temp = path;

        DirectoryInfo di = new DirectoryInfo(path);
        foreach (DirectoryInfo Dir in di.GetDirectories())
        {
            add_content("directory", Dir.Name);
        }
        foreach (FileInfo fi in di.GetFiles())             //파일 분류 코드
        {
            if (fi.Extension == ".fbx")
            {
                char start_target = '@';
                //string target = ".fbx";
                int start_target_num = fi.Name.IndexOf(start_target)+1;
                string cutname = fi.Name.Substring(start_target_num);
                
                //Debug.Log(start_target_num);
                add_content("fbx", cutname); //,fi.Name.Length - target_num
                //splay_animator(fi.Name);
            }

            if (fi.Extension == ".mat")
            { 
                add_content("mat", fi.Name);
            }

            if (fi.Name.Equals("Robot Kyle")){
                add_content("player", fi.Name);
            }

            if(fi.Extension == ".prefab"){
                add_content("prefab", fi.Name);
            }
        }
    }

    public void add_content(string type, string name)
    {

        Transform added_prefab;
        if (type == "directory") added_prefab = prefab_dir;
        else
        {
            string t_type = type;
            string t_name = name;

            //Debug.Log("t_type : " + t_type);
            //Debug.Log("t_name : " + t_name);

            iconController.Change_RawImages(t_type, t_name);         //각 파일에 맞게 파일 이미지 설정
            added_prefab = prefab_bms;   
        }

        Transform created_obj = Instantiate(added_prefab, new Vector3(0, 0, 0), Quaternion.identity);
        created_obj.SetParent(Container);
        created_obj.transform.localScale = new Vector3(1f, 1f, 1f);

        created_obj.GetComponentInChildren<Text>().text = name;

        contents.Add(created_obj);
    }

    public void change_character(string name)
    {
        char target = '.';
        int num = name.IndexOf(target);

        string direct = name.Substring(0, num);

        Debug.Log(direct);

        characterChangeController.characterOnOff(direct);
    }

    public void change_Ground_Materials(string name)
    {
        char target = '.';
        int num = name.IndexOf(target);
        
        string direct = name.Substring(0,num);

        //Debug.Log("direct" + direct);

        GroundMaterialController.Change_Materials(direct);
    }

    public void change_Body_Materials(string name)
    {
        char target = '.';
        int num = name.IndexOf(target);

        string direct = name.Substring(0, num);

        BodyMaterialController.Change_Body(direct);
        ArmMaterialController.Change_Body(direct);
    }


    public void change_Head_Materials(string name)
    {
        char target = '.';
        int num = name.IndexOf(target);
        string direct = name.Substring(0, num);
        HeadMaterialController.Change_Head(direct);
    }


    /**********************************애니메이션*************************************************/
    /******************************************************************************************/
    public void play_animator(string name)
    {
        if (name.Contains("Jumping"))
        {
            
            StartCoroutine(MotionReset());
            animator.SetBool("isJump", _check);    
        }

        else if (name.Contains("Sitting"))
        {
            StartCoroutine(MotionReset());
            animator.SetBool("isSitting", _check);
        }

        else if (name.Contains("Sad"))
        {
            StartCoroutine(MotionReset());
            animator.SetBool("isSad", _check);
        }

        else if (name.Contains("Run"))
        {
            StartCoroutine(MotionReset());
            animator.SetBool("isRun", _check);
        }
    }

    IEnumerator MotionReset()
    {

        foreach (var pair in settingBool)
        {
            animator.SetBool(pair.Key, pair.Value);
        }

        _check = true;
        yield return new WaitForSeconds(2f);

        animator.SetBool("isJump", false);
        animator.SetBool("isSitting", false);
        animator.SetBool("isSad", false);
        animator.SetBool("isRun", false);

     
    }

    //public void Loop_off()
    //{
    //    loopSelected = false;
    //}

    //public void Loop_on()
    //{
    //    loopSelected = true;
    //}
    /**********************************애니메이션끝*************************************************/
    /******************************************************************************************/




    public void clear_content()      //파일의 리소스들 중복처리 방]
    {
        foreach (Transform tr in contents)
        {
            Destroy(tr.gameObject);
        }
        contents.Clear();
    }

}