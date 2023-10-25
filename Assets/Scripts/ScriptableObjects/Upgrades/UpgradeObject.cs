using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//lista de upgrades disponiveis a um tal objeto
[CreateAssetMenu(menuName = "Upgrades/Object")]
public class UpgradeObject : ScriptableObject
{
    public string objectName;
    public UpgradeData[] upgrades;
}
