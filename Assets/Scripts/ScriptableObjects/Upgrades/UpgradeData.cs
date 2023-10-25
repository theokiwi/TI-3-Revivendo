using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//informacao de upgrades para popular o dropdown e outros graficos
[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string label;
    public float cost;
    public bool _isUnlocked;
}