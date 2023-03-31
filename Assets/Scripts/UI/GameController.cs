using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameController : MonoBehaviour
{
    [SerializeField] TMP_Text  moneyText;
    public bool paused = true;
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
        money = 0f; // Posteriormente sera substituido por playerPrefs para manter o dinheiro ao longo do jogo.
        moneyText.text = $" {money},00 ";

        Time.timeScale = 1f;
    }

    // Adiciona valor inputado ao dinheiro do jogador, e atualiza o contador na tela
    public void AddMoney (int value)
    {
        money += value;
        moneyText.text = $" {money} ";
    }
}
