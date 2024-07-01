using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEvent : MonoBehaviour
{
    public static OnEvent Instance;

    private Dictionary<string, List<Action<object>>> events = new Dictionary<string, List<Action<object>>>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    public void on(string eventName, Action<object> callback)
    {
        if (!events.ContainsKey(eventName))
        {
            events[eventName] = new List<Action<object>>();
        }
        events[eventName].Add(callback);
    }

    public void off(string eventName, Action<object> callback)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName].Remove(callback);

            if (events[eventName].Count == 0)
            {
                events.Remove(eventName);
            }
        }
    }

    void _emit(string eventName, object @eventData)
    {
        if (events.ContainsKey(eventName))
        {
            foreach (var callback in events[eventName])
            {
                callback(eventData);
            }
        }
    }

    public void emit(string eventName, object @eventData)
    {
        _emit(eventName, eventData);
    }

    public void emit(string eventName)
    {
        _emit(eventName, null);
    }
}
