using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IUpgradeable))]
public class UpgradeInteract : AbstractInteractable
{
    public UpgradeObject upgrades;
    public IUpgradeable upgradeableObject;
    public Action<UpgradeInteract> ClickAction;

    private void Start() 
    {
        upgradeableObject = GetComponent<IUpgradeable>();
    }

    public override void Interact()
    {
        ClickAction(this);
    }
}
