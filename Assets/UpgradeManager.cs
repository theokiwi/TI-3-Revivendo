using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [HideInInspector] public int mesaUpgrades;
    [SerializeField] private Table[] mesas;
    public int[] mesaPrecos;
    [HideInInspector] public int spawnUpgrades;
    [SerializeField] private ClientSpawn[] spawns;
    public int[] spawnPrecos;

    [HideInInspector] public int cozinhaUpgrades;
    [SerializeField] private int maxCozinhaUpgrades;
    public int[] cozinhaPrecos;
    [SerializeField] TMP_Text mesaUpgradeText, kitchenUpgradeText, marketingUpgradeText, timerUpgradeText;
    [HideInInspector] public int esperaUpgrades;
    [SerializeField] private int maxEsperaUpgrades;
    public int[] esperaPrecos;
    [HideInInspector] public float waitMultiplier;

    private void Start() 
    {
        foreach(Table table in mesas)
        {
            table.gameObject.SetActive(false);
        }
        waitMultiplier = 1;
        GameController.Instance.chefSpeedMult = 1;

        mesaUpgradeText.text = $"{mesaPrecos[mesaUpgrades]}"; 
        timerUpgradeText.text = $"{esperaPrecos[esperaUpgrades]}";
        marketingUpgradeText.text = $"{spawnPrecos[spawnUpgrades]}";
        kitchenUpgradeText.text = $"{cozinhaPrecos[cozinhaUpgrades]}";

    }

    public void UpgradeMesa()
    {
        if(mesaUpgrades >= mesas.Length) return;
        if(GameController.Instance.RemoveMoney(mesaPrecos[mesaUpgrades]))
        {
            mesas[mesaUpgrades].gameObject.SetActive(true);
            mesaUpgrades++;
            if(mesaUpgrades < mesas.Length)
            {
                mesaUpgradeText.text = $"{mesaPrecos[mesaUpgrades]}";
            }
            else
            {
                mesaUpgradeText.text = $"MAX";
            }
        }
    }
    
    public void UpgradeEspera()
    {
        if(esperaUpgrades >= maxEsperaUpgrades) return;
        if(GameController.Instance.RemoveMoney(esperaPrecos[esperaUpgrades]))
        {
            esperaUpgrades++;
            waitMultiplier = 1 + 0.1f * esperaUpgrades;
            if(esperaUpgrades < maxEsperaUpgrades)
            {
                timerUpgradeText.text = $"{esperaPrecos[esperaUpgrades]}";
            }
            else
            {
                timerUpgradeText.text = $"MAX";
            }
        }
    }

    public void UpgradeSpawn()
    {
        if(spawnUpgrades >= spawns.Length) return;
        if(GameController.Instance.RemoveMoney(spawnPrecos[spawnUpgrades]))
        {
            spawns[spawnUpgrades].gameObject.SetActive(true);
            spawnUpgrades++;

            if(spawnUpgrades < spawns.Length)
            {
                marketingUpgradeText.text = $"{spawnPrecos[spawnUpgrades]}";
            }
            else
            {
                marketingUpgradeText.text = $"MAX";
            }
        }
    }

    public void UpgradeCozinha()
    {
        if(cozinhaUpgrades >= maxCozinhaUpgrades) return;
        if(GameController.Instance.RemoveMoney(cozinhaPrecos[cozinhaUpgrades]))
        {
            cozinhaUpgrades++;
            if(cozinhaUpgrades < maxCozinhaUpgrades)
            {
                kitchenUpgradeText.text = $"{cozinhaPrecos[cozinhaUpgrades]}";
            }
            else
            {
                kitchenUpgradeText.text = $"MAX";
            }
        }
        GameController.Instance.chefSpeedMult = 1 - (cozinhaUpgrades * 0.1f);
    }
}
