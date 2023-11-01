using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//dropdown que se popula de acordo com upgrades disponiveis a um objeto
[RequireComponent(typeof(TMP_Dropdown))]
public class UpgradeDropdown : UIElement
{
    public TMP_Dropdown dropdown;
    [HideInInspector] public UpgradeObject upgrades;
    [HideInInspector] public IUpgradeable upgradeableObject;
    private void Start() 
    {
        dropdown = gameObject.GetComponent<TMP_Dropdown>();
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        
        if(!dropdown) { dropdown = gameObject.GetComponent<TMP_Dropdown>(); }

        dropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i < upgrades.upgrades.Length; i++)
        {
            optionList.Add(new TMP_Dropdown.OptionData(upgrades.upgrades[i].label));
            optionList[i].text = upgrades.upgrades[i].label + (upgrades.upgrades[i]._isUnlocked ? "":$"[{upgrades.upgrades[i].cost}]");
        }
        dropdown.AddOptions(optionList);
        dropdown.onValueChanged.AddListener(upgradeableObject.ChangeUpgrade);
    }
}
