using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public Floor[] position;
}

public class Backtracker : MonoBehaviour
{
    Stack<GameState> m_GameState;
    Device[] m_Devices;

    void Start()
    {
        m_GameState = new Stack<GameState>();

        m_Devices = GameObject.Find("Devices")?.GetComponentsInChildren<Device>();

        if (m_Devices == null)
        {
            Debug.LogError("Could not fill 'Device[] m_Devices' on search for 'Devices' in Backtrack script.");
        }
    }

    public void SaveState()
    {
        GameState state = new GameState
        {
            position = new Floor[m_Devices.Length]
        };

        for (int i=0; i<m_Devices.Length; i++)
        {
            if (m_Devices[i] != null && !m_Devices[i].IsFalling())
            {
                state.position[i] = m_Devices[i].floor;
            } 
        }

        m_GameState.Push(state);
    }

    public bool Backtrack()
    {
        if (m_GameState.Count == 0)
        {
            return false;
        }

        GameState prev = m_GameState.Pop();

        for (int i=0; i<m_Devices.Length; i++)
        {
            if (m_Devices[i] != null && !m_Devices[i].IsFalling())
            {
                m_Devices[i].floor = prev.position[i];
                m_Devices[i].UpdateFloor();
            }
        }

        return true;
    }
}