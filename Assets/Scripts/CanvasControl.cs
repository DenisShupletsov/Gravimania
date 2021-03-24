using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    [SerializeField] private GameObject[] canvas;
    [SerializeField] private GameObject[] _gearsGO;
    [SerializeField] private Text[] _gearsText;
    [SerializeField] private Text _timerText;
    private bool _onPause;

    private float _timer;
    private int _gears;

    void Start()
    {
        FinishLine.OnReachingTheFinish.AddListener(ReachingTheFinish);
        HitHead.OnHitHead.AddListener(GameOver);
        PickUp.OnPickUp.AddListener(GearPikedUp);

        _gearsGO = GameObject.FindGameObjectsWithTag("Gear");
        _gearsText[1].text = _gearsGO.Length.ToString();

        InvokeRepeating("Timer", 0, 1);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (_onPause)
                Time.timeScale = 1f;
                else
                    Time.timeScale = 0f;

            _onPause = !_onPause;
            canvas[1].SetActive(_onPause);
        }
    }

    void Timer() 
    {
        _timer++;
        int minutes = Mathf.FloorToInt(_timer / 60F);
        int seconds = Mathf.FloorToInt(_timer - minutes * 60);
        _timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    private void GearPikedUp()
    {
        _gears++;
        _gearsText[0].text = _gears.ToString();
    }

    void GameOver() => canvas[2].SetActive(true);
    void ReachingTheFinish() => canvas[0].SetActive(true);
}
