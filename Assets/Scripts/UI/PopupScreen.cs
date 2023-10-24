using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopupScreen : UIScreen
{
    [SerializeField] private OpenCloseButton openButton;
    protected override void OnPopup()
    {
        openButton.gameObject.SetActive(false);
    }

    protected override void OnHide()
    {
        openButton.gameObject.SetActive(true);
    }
}
