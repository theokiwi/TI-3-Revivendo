using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        Time.timeScale = 1.0f;
        AudioManager.instance.Click();
        SceneManager.LoadScene(scene);
    }
    public void ChangeAudio(string audio)
    {
        AudioManager.instance.SendMessage(audio);
    }
}
