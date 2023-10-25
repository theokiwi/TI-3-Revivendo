using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//tela de upgrades que se atualiza de acordo com o objeto selecionado
public class UpgradeScreen : PopupScreen
{
    public UpgradeObject upgradeObject;
    [SerializeField] private UpgradeDropdown dropdown;
    [SerializeField] private TextDisplay selectedObjectText;

    protected override void OnPopup()
    {
        base.OnPopup();
        dropdown.upgradeObject = upgradeObject;
        dropdown.UpdateUI();
        selectedObjectText.DisplayText = upgradeObject.objectName;
    }
}
