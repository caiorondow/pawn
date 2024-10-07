using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Device : MonoBehaviour
{
    public GameObject prefabClock;
    public Floor floor = null;
    public Vector3 front = Vector3.zero;
    protected Clock clk;
    bool m_IsFalling = false;

    protected short m_Id;
    public static short m_NextId = 0;

    public short Id
    {
        get { return m_Id; }
    }

    protected void Start()
    {
        m_Id = m_NextId++;

        front = transform.forward;
    }

    protected void Update()
    {
        /*
            debug only
        */
        Debug.DrawLine(transform.position, transform.position + front * 2f, Color.red);

        /* if object fall, destroy it */
        if (transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    void Awake() 
    {
        if(prefabClock != null)
        {
            clk = prefabClock.GetComponent<Clock>();
        }
    }

    void OnEnable() 
    {
        Subscribe(clk);
    }

    void OnDisable() 
    {
        Unsubscribe(clk);
    }

    public virtual void PerformAction()
    {
        
    }

    protected void Subscribe(in Clock clk)
    {
        if(clk != null)
        {
            clk.OnTick += PerformAction;
        }
    }

    protected void Unsubscribe(in Clock clk)
    {
        if(clk != null)
        {
            clk.OnTick -= PerformAction;
        }
    }

    public void UpdateClock(in Clock other)
    {
        Unsubscribe(clk);
        Subscribe(other);

        clk = other;
    }

    public void MoveToward(Vector3 direction, bool updateFront=true)
    {    
        front = updateFront ? direction : front;

        if (floor == null)
        {
            return;
        }

        Propagate(direction);
    }

    void FallIntoTheVoid(Vector3 direction)
    {
        m_IsFalling = true;
        transform.position = floor.transform.position + direction * 2 + Vector3.up * transform.position.y;
        floor = null;
        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.useGravity = true;
        }
    }

    public void UpdateFloor()
    {
        if (floor != null)
        {
            transform.position = floor.transform.position + Vector3.up * transform.position.y;
        }
    }

    public bool IsFalling()
    {
        return m_IsFalling;
    }

    bool IsValidMovement(Vector3 direction)
    {
        Floor f = Floor.GetNext(floor, direction);

        while (f != null)
        {
            if (f.device == null)
            {
                return true;
            }

            if (f.IsReachable())
            {
                f = Floor.GetNext(f, direction);
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    Stack<Device> FindLastDevice(Vector3 dir)
    {   
        Stack<Device> s = new Stack<Device>();

        Floor f = floor;

        while (f != null && f.device != null)
        {
            s.Push(f.device);
            f = Floor.GetNext(f, dir);
        }

        return s;
    }

    void Propagate(Vector3 dir)
    {
        if (!IsValidMovement(dir))
        {
            return;
        }

        Stack<Device> s = FindLastDevice(dir);

        while(s.Count != 0)
        {
            Device d = s.Pop();

            Floor newFloor = Floor.GetNext(d.floor, dir);
        
            if (newFloor != null)
            {
                Floor.UpdateDeviceFloor(d, newFloor);
            }
            else
            {
                d.m_IsFalling = true;
                Floor.SendDeviceToVoid(d, dir);
            }
        }
    }
}