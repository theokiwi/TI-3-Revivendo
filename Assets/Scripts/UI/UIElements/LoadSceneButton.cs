using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : ButtonFunction
{
    [SerializeField] private string SceneName;

    private void Awake()
    {
        ButtonAction = Load;
    }

    private void Load()
    {
        SceneManager.LoadScene(SceneName);
    }
}
