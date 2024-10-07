using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieverDoor : Device
{
    Renderer objectRenderer;
    public Material open;
    AudioSource m_AudioSource;
    bool m_IsActivated = false;

    protected new void Start() 
    {
        base.Start();
        m_AudioSource = GetComponent<AudioSource>();
        objectRenderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Photon")
        {
            objectRenderer.material = open; 
            Destroy(other.gameObject);

            if (!m_IsActivated)
            {
                m_IsActivated = true;
                m_AudioSource?.Play();
                Destroy(m_AudioSource, 5f);
            }
        }
    }
}