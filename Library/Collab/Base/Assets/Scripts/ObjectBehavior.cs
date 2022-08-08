using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ObjectBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public delegate void Action();
    public delegate bool IsEvent();
    private delegate bool Get_Action(IsEvent isEvent);

    [Tooltip("사용자가 사용할 조건 함수입니다")]
    private Dictionary<string, float> delay_dict;
    [Tooltip("사용자가 사용할 조건 함수의 조건이 만족되었는지 확인하는 변수입니다")]
    private Dictionary<string, bool> state_dict;
    [Tooltip("사용자가 입력할 동작 함수입니다")]
    private Dictionary<string, Action> action_dict;
    [Tooltip("등록된 함수들을 함수들끼리 제어하는 스위치 역할의 딕셔너리입니다.")]
    private Dictionary<string, bool> switch_dict;
    [Tooltip("조건 함수 내의 코루틴이 한개씩만 동작하도록 하는 mutex입니다.")]
    private Dictionary<string, bool> coroutine_mutex;
    [Tooltip("")]
    private Dictionary<string, IsEvent> player_action;


    private void InitEvenetListener()
    {
        delay_dict = new Dictionary<string, float>();
        state_dict = new Dictionary<string, bool>();
        action_dict = new Dictionary<string, Action>();
        switch_dict = new Dictionary<string, bool>();
        coroutine_mutex = new Dictionary<string, bool>();
        player_action = new Dictionary<string, IsEvent>();
    }
    public void AddEventListener(string eventListener, float delay, Action act)
    {
        delay_dict.Add(eventListener, delay);
        state_dict.Add(eventListener, false);
        action_dict.Add(eventListener, act);
        switch_dict.Add(eventListener, true);
        coroutine_mutex.Add(eventListener, true);
    }

    private PlayerEvent playerevent;

    private void Awake()
    {
        InitEvenetListener();
    }

    private void Start()
    {
        playerevent = GameObject.Find("Player").GetComponent<PlayerEvent>();
    }
    public void Call_Action(string action, IsEvent get_event)
    {
        if(!player_action.ContainsKey(action)) player_action.Add(action, get_event);
        Invoke(action, 0);
    }

    private bool Get_Event(IsEvent get_event)
    {
        return playerevent.Get_Action(get_event);
    }


    private void OnRangeIn()
    {
        string function = "OnRangeIn";
        
        if (!switch_dict[function]) return;

        
        if (!state_dict[function])
        {
            if (coroutine_mutex[function]) StartCoroutine(CheckEvent(delay_dict[function], Get_Event, OnRangeIn, function));
        }
        else
        {
            Debug.Log("OnRangeIn Called!");
            switch_dict["OnRangeOut"] = true;
            switch_dict[function] = false;
            state_dict[function] = false;
            action_dict[function]();
        }
    }

    
    private void OnRangeOut()
    {
        string function = "OnRangeOut";

        if (!switch_dict["OnRangeOut"]) return;
        if (!state_dict[function])
        {
            if (coroutine_mutex[function]) StartCoroutine(CheckEvent(delay_dict[function], Get_Event, OnRangeOut, function));
        }
        else
        {
            Debug.Log("OnRangeOut Called!");
            switch_dict["OnRangeOut"] = false;
            switch_dict["OnRangeIn"] = true;
            state_dict[function] = false;
            action_dict[function]();
        }
    }

    private delegate void OnTouch();

    IEnumerator CheckEvent(float delay, Get_Action checker, Action act, string function)
    {
        Debug.Log("Wait delay : " + delay);
        
        coroutine_mutex[function] = false;
        yield return new WaitForSecondsRealtime(delay);

        Debug.Log("check : " + checker(player_action[function]));
        coroutine_mutex[function] = true;
        if (checker(player_action[function]))
        {
            state_dict[function] = checker(player_action[function]);
            act();
        }
        
    }
}