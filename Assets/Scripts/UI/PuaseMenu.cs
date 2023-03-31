using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PuaseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    private bool paused = false;


    private void Start()
    {
        Time.timeScale= 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseGame();  // Pausa/despausa o jogo.
    }

    // Abre/fecha o menu de pausa e ajusta o timeScale de acordo.
    public void PauseGame()
    {
        paused = !paused;
        pauseScreen.SetActive(paused);

        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
