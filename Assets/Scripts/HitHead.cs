using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitHead : MonoBehaviour
{
    public static UnityEvent OnHitHead = new UnityEvent();

    private void Update()
    {
        if(transform.position.y < -40)
            OnHitHead?.Invoke();
    }

    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.layer)
        {
            case 8:
                OnHitHead?.Invoke();
                break;
        }
    }
}
