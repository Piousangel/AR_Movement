using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SliderController : MonoBehaviour
{
    private Transform _obj;
    public TextMeshProUGUI text;

    private float num;

    private void Start()
    {
        _obj = GameObject.Find("Moar").GetComponent<Transform>();
        _obj.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
        
    }

    
    public void OnSliderEvent(float value)
    {
        text.text = $"Rotation {(value+0.5f) * 360 :F1}%";
        //_obj.transform.position = new Vector3(0, 0, 0);
        _obj.transform.rotation = Quaternion.AngleAxis((value)*360, new Vector3(0, 1, 0));

        //StartCoroutine(PositionReset());
    }

    IEnumerator PositionReset()
    {
        yield return new WaitForSecondsRealtime(2f);

        _obj.transform.position = new Vector3(0, 0, 0);
    }
}
