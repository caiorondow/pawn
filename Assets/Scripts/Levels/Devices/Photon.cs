using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : Device
{
    const int MAX_LIFETIME = 8;
    public int lifetime = MAX_LIFETIME;
    Renderer m_Renderer;

    new void Start()
    {
        base.Start();

        m_Renderer = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        Vector3 movement = front * 2 * (clk.bpm / 60f) * Time.deltaTime;
        transform.position += movement;
    }

    public override void PerformAction()
    {    
        transform.position = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            Mathf.Round(transform.position.z)
        );

        lifetime--;
        
        Color c = m_Renderer.material.color;
        c.a = ((float)lifetime)/MAX_LIFETIME;
        m_Renderer.material.color = c;
   
        if(lifetime == 0)
        {
            Destroy(gameObject);
        }
    }
}