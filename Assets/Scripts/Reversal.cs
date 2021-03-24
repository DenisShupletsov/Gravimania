using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Reversal : MonoBehaviour
{
    [SerializeField] private GameObject _bikeBody;

    private Vector3 currentEulerAngles;
    private Quaternion currentRotation;

    private bool _playerGraunded;
    private const float rayMaxDistance = 3f;
    private LayerMask graundMask;

    private float _timer;
    private const float _step = 10;
    private float _angle = 180;

    public static UnityEvent OnReversal = new UnityEvent();

    private void Start()
    {
        graundMask = LayerMask.GetMask("Graund");
        _angle /= _step;
    }

    private void FixedUpdate() => _playerGraunded = Physics.Raycast(_bikeBody.transform.position, -transform.up, rayMaxDistance, graundMask);

    private void Update()
    {
        if (!_playerGraunded && Input.GetButtonDown("Reversal"))
                ReversalButtonDown();
    }

    public void ReversalButtonDown() => Invoke("Timer", 1 * Time.deltaTime);

    private void Timer() 
    {   
        if (_timer < _angle)
        {
            transform.RotateAround(_bikeBody.transform.position, new Vector3(0, 1, 0), _step);
            _timer += 1;

            Invoke("Timer", 1 * Time.deltaTime);
        }
        else
        {
            _timer = 0;
            OnReversal?.Invoke();
        }
    }
}
