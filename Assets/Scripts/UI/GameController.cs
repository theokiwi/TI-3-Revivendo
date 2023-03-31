using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Text moneyText;
    [SerializeField] GameObject pauseScreen;
    public bool paused;
    private float money;

    public static GameController instance;


    private void Awake()
    {
        // Codigo basico de singleton.
        if ( instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        money = 0; // Posteriormente sera substituido por playerPrefs para manter o dinheiro ao longo do jogo.
        paused = false;
        Time.timeScale = 1f;
    }

    // Adiciona valor inputado ao dinheiro do jogador, e atualiza o contador na tela
    public void AddMoney (int value)
    {
        money += value;
        moneyText.text = $" {money} ";
    }

    // Abre/fecha o menu de pausa e ajusta o timeScale de acordo.
    public void PauseGame()
    {
        paused = !paused;

        if (paused)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
