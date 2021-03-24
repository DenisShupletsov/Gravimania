using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    public static UnityEvent OnPickUp = new UnityEvent();

    void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {   
            case "Head":
            case "RearWheel":
            case "FrontWheel":
                Destroy(gameObject);
                OnPickUp?.Invoke();
                break;
        }       
    }
}