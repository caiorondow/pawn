using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    enum WaveType
    {
        Sin,Cos,Acos,Acosh,
        Asin,Asinh,Atan,Atanh,
        Cosh,Sinh,Tan,Tanh
    }

    public delegate double WaveFunction(double x);
    WaveFunction WaveCallback;

    double m_X = 0.0f;
    const double PERIOD = 2.0f * Math.PI; 
    [SerializeField] float m_WaveSpeed = 0.1f;
    [SerializeField] float m_WaveAmplitude = 0.125f;
    [SerializeField] WaveType m_WaveType = WaveType.Cos;
    WaveType m_CurrentWave = WaveType.Cos;
    Vector3 m_StartPosition;

    [SerializeField] bool m_XAxis = false; 
    [SerializeField] bool m_YAxis = false; 
    [SerializeField] bool m_ZAxis = false;
    Vector3 m_Axis = Vector3.zero;

    void Start()
    {
        m_StartPosition = transform.position;
        SelectWave(m_WaveType);
    }

    void Update()
    {
        if (m_CurrentWave != m_WaveType)
        {
            SelectWave(m_WaveType);
        }

        m_Axis = new Vector3(m_XAxis ? 1 : 0, m_YAxis ? 1 : 0, m_ZAxis ? 1 : 0);
        transform.position = m_StartPosition + m_Axis * (float)WaveCallback(m_X) * m_WaveAmplitude;
        m_X = (m_X >= PERIOD ? 0.0f : m_X + m_WaveSpeed * Time.deltaTime);
    }

    void SelectWave(WaveType wave)
    {
        m_CurrentWave = wave;

        switch(wave)
        {
        case WaveType.Sin:
            WaveCallback = Math.Sin; break;
        case WaveType.Acos:
            WaveCallback = Math.Acos; break;
        case WaveType.Acosh:
            WaveCallback = Math.Acosh; break;
        case WaveType.Asin:
            WaveCallback = Math.Asin; break;
        case WaveType.Asinh:
            WaveCallback = Math.Asinh; break;
        case WaveType.Atan:
            WaveCallback = Math.Atan; break;
        case WaveType.Atanh:
            WaveCallback = Math.Atanh; break;
        case WaveType.Cosh:
            WaveCallback = Math.Cosh; break;
        case WaveType.Sinh:
            WaveCallback = Math.Sinh; break;
        case WaveType.Tan:
            WaveCallback = Math.Tan; break;
        case WaveType.Tanh:
            WaveCallback = Math.Tanh; break;
        default:
            WaveCallback = Math.Cos; break;
        }
    }
}
