using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//esse componente faz o botão abrir ou fechar uma tela, e essa tela pode ser mudada em runtime.
[RequireComponent(typeof(Button))]
public class OpenCloseButton : UIElement
{
    private Button buttonComponent;
    public UIScreen screen;

    private void Start() 
    {
        buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.AddListener(Click);
    }

    public override void UpdateUI(){}

    private void Click()
    {
        if(screen._isEnabled)
            screen.Hide();
            else
            screen.Popup();
    }

}
