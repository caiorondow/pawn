using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    Camera m_Camera;
    List<Transform> m_Levels;
    int m_Current = 0;
    Vector3 m_StartPosition;
    Vector3 m_TargetPosition;
    float m_TransitionDuration = 1.0f; // Duration of the transition in seconds
    float m_TransitionProgress = 0f;
    bool m_IsTransitioning = false;
    float m_OffsetDistance = -30f; // Offset distance along the z-axis

    void Start()
    {
        m_Camera = Camera.main;
        m_Levels = GameObject.Find("LevelManager").GetComponentsInChildren<Transform>().ToList();
        m_Levels.RemoveAt(0);
        m_Camera.transform.position = m_Levels[m_Current].position + new Vector3(0, 0, m_OffsetDistance);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextLevel();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevLevel();
        }

        if (m_IsTransitioning)
        {
            m_TransitionProgress += Time.deltaTime / m_TransitionDuration;
            m_Camera.transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, EaseInOutQuad(m_TransitionProgress));
            if (m_TransitionProgress >= 1f)
            {
                m_IsTransitioning = false;
                m_TransitionProgress = 0f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return)) 
        {
            SceneManager.LoadScene("Level" + m_Current);
        }
    }

    void NextLevel()
    {
        if (!m_IsTransitioning && m_Current + 1 < m_Levels.Count)
        {
            m_IsTransitioning = true;
            m_StartPosition = m_Camera.transform.position;
            m_Current++;
            m_TargetPosition = m_Levels[m_Current].position + new Vector3(0, 0, m_OffsetDistance);
        }
    }

    void PrevLevel()
    {
        if (!m_IsTransitioning && m_Current - 1 >= 0)
        {
            m_IsTransitioning = true;
            m_StartPosition = m_Camera.transform.position;
            m_Current--;
            m_TargetPosition = m_Levels[m_Current].position + new Vector3(0, 0, m_OffsetDistance);
        }
    }

    // Easing function for smooth transitions
    float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
    }
}
