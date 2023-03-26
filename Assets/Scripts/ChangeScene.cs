using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Inicia o carregamento da cena com o nome informado
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
