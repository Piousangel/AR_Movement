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

    [Tooltip("???????? ?????? ???? ??????????")]
    private Dictionary<string, float> delay_dict;
    [Tooltip("???????? ?????? ???? ?????? ?????? ???????????? ???????? ??????????")]
    private Dictionary<string, bool> state_dict;
    [Tooltip("???????? ?????? ???? ??????????")]
    private Dictionary<string, Action> action_dict;
    [Tooltip("?????? ???????? ?????????? ???????? ?????? ?????? ??????????????.")]
    private Dictionary<string, bool> switch_dict;
    [Tooltip("???? ???? ???? ???????? ???????? ?????????? ???? mutex??????.")]
    private Dictionary<string, bool> coroutine_mutex;
    [Tooltip("")]
    private Dictionary<string, IsEvent> player_action;

    private PlayerEvent playerevent;

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
        Debug.Log(action);
        Invoke(action, 0);
    }

    private bool Get_Event(IsEvent get_event)
    {
        return playerevent.Get_Action(get_event);
    }

    private void End_Action(string action)
    {
        playerevent.End_Action(action);
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
            End_Action(function);
        }
    }

    
    private void OnRangeOut()
    {
        string function = "OnRangeOut";

        if (!switch_dict[function]) return;
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
            End_Action(function);
        }
    }

    private delegate void OnTouch();

    IEnumerator CheckEvent(float delay, Get_Action checker, Action act, string function)
    {
        Debug.Log("Wait delay : " + delay);
        
        coroutine_mutex[function] = false;
        float time_t = 0;
        bool final_check = true;
        while (time_t < delay)
        {
            time_t += 1;
            if (!checker(player_action[function])) final_check = false;
            yield return new WaitForSecondsRealtime(1);
        }
        

        Debug.Log("check : " + checker(player_action[function]));
        coroutine_mutex[function] = true;
        if (final_check)
        {
            state_dict[function] = true;
            act();
        }
        
    }
}