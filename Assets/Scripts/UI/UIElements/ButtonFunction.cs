using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonFunction : UIElement
{
    public override void UpdateUI()
    {
        //nada eu acho
        Debug.Log("but�o update?");
    }

    public Action ButtonAction;

    public void DoAction()
    {
        ButtonAction();
    }
}
