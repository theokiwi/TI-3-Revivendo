using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [HideInInspector] public int mesaUpgrades;
    [SerializeField] private Table[] mesas;
    public int[] mesaPrecos;
    [HideInInspector] public int cozinhaUpgrades;
    [SerializeField] private int maxCozinhaUpgrades;
    public int[] cozinhaPrecos;
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
    }

    public void UpgradeMesa()
    {
        if(mesaUpgrades >= mesas.Length) return;
        if(GameController.Instance.RemoveMoney(mesaPrecos[mesaUpgrades]))
        {
            mesas[mesaUpgrades].gameObject.SetActive(true);
            mesaUpgrades++;
        }
    }
    
    public void UpgradeEspera()
    {
        if(esperaUpgrades >= maxEsperaUpgrades) return;
        if(GameController.Instance.RemoveMoney(esperaPrecos[esperaUpgrades]));
    }

    public void UpgradeCozinha()
    {
        if(cozinhaUpgrades >= maxCozinhaUpgrades) return;
        if(GameController.Instance.RemoveMoney(cozinhaPrecos[cozinhaUpgrades]))
        {
            cozinhaUpgrades++;
        }
        GameController.Instance.chefSpeedMult = 1 - (cozinhaUpgrades * 0.1f);
    }
}
