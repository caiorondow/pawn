using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Device
{
    [SerializeField] AudioClip m_BacktrackClip;
    [SerializeField] AudioClip m_MoveClip;
    AudioSource m_AudioSource;
    Backtracker m_State;
    
    protected new void Start()
    {
        base.Start();
        m_AudioSource = GetComponent<AudioSource>();
        m_State = GetComponent<Backtracker>();
    }

    protected new void Update()
    {
        base.Update();
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Q) && m_State.Backtrack())
        {
            m_AudioSource.PlayOneShot(m_BacktrackClip);
        }
    }

    public override void PerformAction()
    {
        
    }

    void PlayerMovement()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            m_State?.SaveState();
            m_AudioSource.PlayOneShot(m_MoveClip);
            MoveToward(Vector3.right);
        }
        else if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            m_State?.SaveState();
            m_AudioSource.PlayOneShot(m_MoveClip);
            MoveToward(Vector3.forward);
        }
        else if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            m_State?.SaveState();
            m_AudioSource.PlayOneShot(m_MoveClip);
            MoveToward(Vector3.left);
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            m_State?.SaveState();
            m_AudioSource.PlayOneShot(m_MoveClip);
            MoveToward(Vector3.back);
        }
    }
}