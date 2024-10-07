using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    const int m_GridW = 6, m_GridH = 6;
    Floor[] m_Floors;
    void Start()
    {
        CreateFloorGraph();
    }

    void CreateFloorGraph()
    {
        m_Floors = GetComponentsInChildren<Floor>();

        for(int i=0; i<m_GridH; i++)
        {
            for(int j=0; j<m_GridW; j++)
            {
                Floor f = m_Floors[i * m_GridW + j];

                f.right = j-1 < 0        ? null : m_Floors[i * m_GridW + (j-1)];
                f.left  = j+1 >= m_GridH ? null : m_Floors[i * m_GridW + (j+1)];
                f.down  = i-1 < 0        ? null : m_Floors[(i-1) * m_GridW + j];
                f.up    = i+1 >= m_GridW ? null : m_Floors[(i+1) * m_GridW + j];
            }
        }
    }
}