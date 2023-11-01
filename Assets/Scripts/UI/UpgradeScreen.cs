using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//tela de upgrades que se atualiza de acordo com o objeto selecionado
public class UpgradeScreen : PopupScreen
{
    [SerializeField] private UpgradeDropdown dropdown;
    [SerializeField] private TextDisplay selectedObjectText;
    [SerializeField] private Vector2 offset;
    private Camera mainCam;
    private void OnEnable() 
    {
        UpdateClickListener();
        mainCam = Camera.main;
    }

    //pega todos os "clicaveis" e poe o PopupOnClicked pra ouvir quando eles levam interacao
    public void UpdateClickListener()
    {
        foreach(UpgradeInteract interactable in FindObjectsOfType<UpgradeInteract>())
        {
            interactable.ClickAction = PopupOnClicked;
        }
    }

    //poe a tela na posicao certa e muda os dados de upgrade, depois abre.
    protected void PopupOnClicked(UpgradeInteract interactable)
    {
        Vector2 screenPos = mainCam.WorldToScreenPoint(interactable.transform.position);
        screenPos += offset;
        transform.position = screenPos;


        //atualiza os elementos que mudam com o upgradeObject
        dropdown.upgrades = interactable.upgrades;
        dropdown.upgradeableObject = interactable.upgradeableObject;
        selectedObjectText.DisplayText = interactable.upgrades.objectName;

        Popup();
    }

    protected override void OnPopup()
    {
        base.OnPopup();
        
        dropdown.UpdateUI();
    }


}
