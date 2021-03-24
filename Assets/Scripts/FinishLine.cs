using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishLine : MonoBehaviour
{
    public static UnityEvent OnReachingTheFinish = new UnityEvent();

    void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
            case "Head":
            case "RearWheel":
            case "FrontWheel":
                OnReachingTheFinish?.Invoke();
                break;
        }
    }
}
