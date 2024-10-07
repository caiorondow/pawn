using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEffect : MonoBehaviour
{
    [SerializeField] private bool rotateAroundX = false;
    [SerializeField] private bool rotateAroundY = false;
    [SerializeField] private bool rotateAroundZ = false;
    [SerializeField] private float rotationSpeed = 10.0f;

    void Update()
    {
        Vector3 rotationVector = Vector3.zero;

        if (rotateAroundX)
        {
            rotationVector.x = 1;
        }
        if (rotateAroundY)
        {
            rotationVector.y = 1;
        }
        if (rotateAroundZ)
        {
            rotationVector.z = 1;
        }

        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }
}
