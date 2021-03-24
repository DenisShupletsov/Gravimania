using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject[] _player = new GameObject[1];
    private Vector2 _correctPos;

    private void Start()
    {
        _correctPos = transform.position;
        _player = GameObject.FindGameObjectsWithTag("CameraTarget");
    }

    private void Update() => transform.position = new Vector3(_player[0].transform.position.x + _correctPos.x, _player[0].transform.position.y + _correctPos.y, transform.position.z);
}
