using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    public event Action OnTick;

    public float bpm = 120f;

    void Start()
    {
        StartCoroutine(Notify());
    }

    IEnumerator Notify()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f / bpm); // convert to bps
            OnTick?.Invoke();
        }
    }   
}
