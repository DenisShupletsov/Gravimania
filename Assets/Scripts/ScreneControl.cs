using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreneControl : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
    }

    public void SceneLoad(string screneName) => SceneManager.LoadScene(screneName);
}
