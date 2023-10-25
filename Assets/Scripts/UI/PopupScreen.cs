using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//todo: funcao de substituir o botao de abrir com um sprite de aberto
public class PopupScreen : UIScreen
{
    [SerializeField] protected OpenCloseButton openButton;
    [SerializeField] protected bool _hideOpenButton;
    protected override void OnPopup()
    {
        if(_hideOpenButton)
        openButton.gameObject.SetActive(false);
    }

    protected override void OnHide()
    {
        if(_hideOpenButton)
        openButton.gameObject.SetActive(true);
    }
}
