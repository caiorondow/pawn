using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] string m_PrevScene = null;
    [SerializeField] string m_NextScene = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadScene(m_NextScene);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScene(m_PrevScene);
        }
    }

    public void LoadScene(string scene)
    {
        if (!string.IsNullOrEmpty(scene)) 
        {
            SceneManager.LoadScene(scene);
        }
    }
}