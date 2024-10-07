using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] bool m_IsReachable = true;
    public Device device = null;
    public Floor left = null;
    public Floor right = null;
    public Floor up = null;
    public Floor down = null;
    [SerializeField] Material m_IsFreeDebugMat;
    [SerializeField] Material m_OriginalMat;
    Renderer m_Rend;

    void Start()
    {
        m_Rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (this.device != null)
        {
            m_Rend.material = m_IsFreeDebugMat;
        }
        else
        {
            m_Rend.material = m_OriginalMat;
        }
    }

    public bool IsReachable()
    {
        return m_IsReachable;
    }

    public static Floor GetNext(Floor current, Vector3 direction)
    {
        if (current == null)
        {
            return null;
        }

        var next = new Dictionary<Vector3, Floor>
        {
            {Vector3.right  , current.up   },
            {Vector3.left   , current.down },
            {Vector3.forward, current.left },
            {Vector3.back   , current.right}
        };

        return next.TryGetValue(direction, out Floor newFloor) ? newFloor : null;
    }

    void OnTriggerEnter(Collider other) 
    {
        Device device = other.GetComponent<Device>();

        if (device != null)
        {
            this.device = device;
        }    
    }

    void OnTriggerExit(Collider other) 
    {
        Device device = other.GetComponent<Device>();

        if (this.device == null || device == null)
        {
            return;
        }

        if (device.Id == this.device.Id)
        {
            this.device = null;
        }
    }

    public static void UpdateDeviceFloor(Device d, Floor newFloor)
    {
        d.floor = newFloor;
        d.transform.position = d.floor.transform.position + Vector3.up * d.transform.position.y;
    }

    public static void SendDeviceToVoid(Device d, Vector3 dir)
    {
        d.transform.position = d.floor.transform.position + dir * 2 + Vector3.up * d.transform.position.y;
        d.floor = null;
        var rigidbody = d.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.useGravity = true;
        }
    }
}