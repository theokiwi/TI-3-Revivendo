using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonFunction : UIElement
{
    public override void UpdateUI()
    {
        //nada eu acho
    }

    public void DoAction()
    {
        ButtonAction();
    }

    public Action ButtonAction;
}
