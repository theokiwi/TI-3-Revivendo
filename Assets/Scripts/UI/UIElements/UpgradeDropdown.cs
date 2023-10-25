using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//dropdown que se popula de acordo com upgrades disponiveis a um objeto
[RequireComponent(typeof(TMP_Dropdown))]
public class UpgradeDropdown : UIElement
{
    private TMP_Dropdown dropdown;
    [HideInInspector] public UpgradeObject upgradeObject;
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
        for(int i = 0; i < upgradeObject.upgrades.Length; i++)
        {
            optionList.Add(new TMP_Dropdown.OptionData(upgradeObject.upgrades[i].label));
            optionList[i].text = upgradeObject.upgrades[i].label + (upgradeObject.upgrades[i]._isUnlocked ? "":$"[{upgradeObject.upgrades[i].cost}]");
        }
        dropdown.AddOptions(optionList);
    }
}
