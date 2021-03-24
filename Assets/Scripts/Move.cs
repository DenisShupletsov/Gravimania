using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody _bodyRB;

    public float speed;
    public bool flag;

    void Start()
    {
        _bodyRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (flag) 
        {
            _bodyRB.velocity += new Vector3(.1f * speed, 0, 0);
            _bodyRB.AddTorque(new Vector3(0, 0, -360));
        }
    }
}
