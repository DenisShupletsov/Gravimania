using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private Vector3 currentEulerAngles;
    private Quaternion currentRotation;

    void FixedUpdate()
    {
        currentEulerAngles += new Vector3(0, 1f, 0) * rotationSpeed;
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;
    }
}
