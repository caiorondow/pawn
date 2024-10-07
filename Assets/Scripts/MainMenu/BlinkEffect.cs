using System;
using System.Collections;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    const float DEFAULT_BLINK_RATE = 0.5f;
    [SerializeField] float m_BlinkRate = DEFAULT_BLINK_RATE;
    private Renderer m_Rend;
    private CanvasRenderer m_CanvasRenderer;
    private float m_PreviousBlinkRate;
    private Coroutine m_BlinkCoroutine;

    void Start()
    {
        m_Rend = GetComponent<Renderer>();
        m_CanvasRenderer = GetComponent<CanvasRenderer>();

        if (m_Rend == null && m_CanvasRenderer == null)
        {
            Debug.LogError("No Renderer or CanvasRenderer component found on this GameObject.");
            return;
        }

        m_PreviousBlinkRate = m_BlinkRate;
        m_BlinkCoroutine = StartCoroutine(BlinkRoutine());
    }

    void Update()
    {
        if (m_BlinkRate != m_PreviousBlinkRate)
        {
            m_PreviousBlinkRate = m_BlinkRate;
            if (m_BlinkCoroutine != null)
            {
                StopCoroutine(m_BlinkCoroutine);
            }
            m_BlinkCoroutine = StartCoroutine(BlinkRoutine());
        }
    }

    IEnumerator BlinkRoutine()
    {
        float rate = m_BlinkRate;

        if (rate <= 0)
        {
            Debug.LogWarning("Blink rate cannot be zero. Setting to default value of 0.5 seconds.");
            rate = DEFAULT_BLINK_RATE;
        }

        while (true)
        {
            if (m_CanvasRenderer != null)
            {
                m_CanvasRenderer.SetAlpha(0); // Make the Canvas invisible
            }
            else if (m_Rend != null)
            {
                m_Rend.enabled = false; // Disable the Renderer
            }

            yield return new WaitForSeconds(rate);

            if (m_CanvasRenderer != null)
            {
                m_CanvasRenderer.SetAlpha(1); // Make the Canvas visible
            }
            else if (m_Rend != null)
            {
                m_Rend.enabled = true; // Enable the Renderer
            }

            yield return new WaitForSeconds(rate);
        }
    }
}