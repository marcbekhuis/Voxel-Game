using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DebugDuration : MonoBehaviour
{
    public static DebugDuration instance;
    DateTime lastCheckTime;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
        lastCheckTime = DateTime.Now;
    }

    public void DebugProcessDuration(string processName)
    {
        Debug.LogError(processName + ": Time since last time check in milliseconds: " + DateTime.Now.Subtract(lastCheckTime).TotalMilliseconds);
        lastCheckTime = DateTime.Now;
    }
}
