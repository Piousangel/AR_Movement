using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Object : MonoBehaviour
{

    [SerializeField]
    [Tooltip("List for name of position that avatar will start/end action in each object")]
    public List<string> name_points = new List<string>();
    [SerializeField] 
    [Tooltip("List for Vector3 value for position that avatar will start/ed action in each object")]
    public List<Vector3> vector_points = new List<Vector3>();


    private Dictionary<string, GameObject> named_points = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    //public List<Vector3> start_points;
    //private List<GameObject> start_lossy_points;
    public GameObject prefab_point;
    private void Start()
    {
        MadePoints();
    }

    private void MadePoints()
    {
        //start_lossy_points = new List<GameObject>();
        int index = 0;
        foreach (var pos in vector_points)
        {
            var obj = Instantiate(prefab_point, Vector3.zero, Quaternion.identity);
            obj.name = "StartPoint";
            obj.transform.SetParent(this.transform, true);
            obj.transform.localPosition = new Vector3(pos.x / this.transform.lossyScale.x, pos.y / this.transform.lossyScale.y, pos.z / this.transform.lossyScale.z );
            
            //start_lossy_points.Add(obj);
            named_points.Add(name_points[index], obj);
            index++;
        }
    }
    public KeyValuePair<string, GameObject> GetStartPoint(Vector3 position_Avatar)
    {
        float min_distance = 1000;
        //GameObject minVec = start_lossy_points[0];
        //foreach(var pos_obj in start_lossy_points)
        KeyValuePair<string, GameObject> minPair = new KeyValuePair<string, GameObject>();
        foreach(var dic in named_points)
        {
            GameObject pos_obj = dic.Value;
            if(min_distance > Vector3.Distance(pos_obj.transform.position, position_Avatar))
            {
                minPair = dic;
                min_distance = Vector3.Distance(pos_obj.transform.position, position_Avatar);
            }
        }
        return minPair;
    }

    public KeyValuePair<string, GameObject> GetEndPoint(Vector3 position_Player)
    {
        float min_distance = 1000;
        //GameObject minVec = start_lossy_points[0];
        //foreach (var pos_obj in start_lossy_points)
        KeyValuePair<string, GameObject> minPair = new KeyValuePair<string, GameObject>();
        foreach (var dic in named_points)
        {
            GameObject pos_obj = dic.Value;
            if (min_distance > Vector3.Distance(pos_obj.transform.position, position_Player))
            {
                minPair = dic;
                min_distance = Vector3.Distance(pos_obj.transform.position, position_Player);
            }
        }
        //return minVec;
        return minPair;
    }

    public void Act_Object(KeyValuePair<string, GameObject> event_player, string action_avatar)
    {
        switch (event_player.Key) 
        {
            case "Free":
                switch (action_avatar)
                {
                    case "Move2Player":
                        break;
                    case "Move2Object":
                        break;
                    case "OnObject":
                        break;
                }
                break;
            case "IsObject":
                switch (action_avatar)
                {
                    case "Move2Player":
                        break;
                    case "Move2Object":
                        break;
                    case "OnObject":
                        break;
                }
                break;
        }
    }

    [SerializeField]
    [Tooltip("GameObject that using this object")]
    private GameObject using_Avatar;
    public bool IsUsing(GameObject avatar)
    {
        if(using_Avatar != null)
        {
            if (using_Avatar != avatar)
                return true;
        }
        return false;
    }

    public void Using_Object(GameObject avatar)
    {
        using_Avatar = avatar;
    }

    
}
