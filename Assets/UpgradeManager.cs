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

    public void UpgradeMesa()
    {
        if(mesaUpgrades >= mesas.Length) return;
        if(GameController.Instance.RemoveMoney(mesaPrecos[mesaUpgrades]))
        {
            mesas[mesaUpgrades].gameObject.SetActive(true);
            mesaUpgrades++;
        }
    }
    public void UpgradeCozinha()
    {
        if(cozinhaUpgrades >= maxCozinhaUpgrades) return;
        if(GameController.Instance.RemoveMoney(cozinhaPrecos[cozinhaUpgrades]))
        {
            cozinhaUpgrades++;
        }
    }
}
