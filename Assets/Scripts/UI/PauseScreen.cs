using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : UIScreen
{
    [SerializeField] private GameObject imageBackground;

    protected override void OnPopup()
    {
        base.OnPopup();
        imageBackground.SetActive(true);
    }

    protected override void OnHide()
    {
        base.OnHide();
        imageBackground.SetActive(false);
    }
}
