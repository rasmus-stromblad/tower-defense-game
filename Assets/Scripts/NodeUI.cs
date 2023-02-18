﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        // Move the UI
        transform.position = target.GetBuildPosition();

        // If the turret is not upgraded
        if(!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();
        ui.SetActive(true);
    }

    public void Hide()
    {
        // Hide the ui
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        // Deselect the node after upgrading
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {        
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
