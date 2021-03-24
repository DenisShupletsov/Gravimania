using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControler : MonoBehaviour
{
    void Start()
    {
        FinishLine.OnReachingTheFinish.AddListener(StopTime);
        HitHead.OnHitHead.AddListener(StopTime);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Reload"))
            SceneManager.LoadScene("Level");
    }

    void StopTime() => Time.timeScale = 0f;

    void OnGUI() => GUI.TextArea(new Rect(10f, 10f, 260f, 95f), "W and A or Up and Down to control gas\nA and D or Left and Right to control torque\nSPACE to brake\nLEFT CTRL or RIGHT CTRL to reversal\nR to reload level\nESC to pause");
}
