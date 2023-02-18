using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 posOffset;

    [HideInInspector]
    // Can be used if the node has a turret built before starting the level
    public GameObject turret;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;

    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + posOffset;
    }

    void OnMouseDown()
    {
        // If the user hovers over an ui element, return
        /*if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }*/

        if(turret != null)
        {
            // Select the clicked node
            buildManager.SelectNode(this);
            return;
        }

        if(!buildManager.CanBuild)
        {
            return;
        }

        // Build turret on node
        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint bluePrint)
    {
        if(PlayerStats.money < bluePrint.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        PlayerStats.money -= bluePrint.cost;
        GameObject _turret = (GameObject)Instantiate(bluePrint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = bluePrint;

        // Play the build effect
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build");
    }

    public void UpgradeTurret()
    {
        if(PlayerStats.money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade");
            return;
        }

        PlayerStats.money -= turretBlueprint.upgradeCost;

        // Delete the old turret before creating the upgraded one
        Destroy(turret);

        // Build a upgraded one
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;        

        // Play the build effect
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded");
    }

    public void SellTurret()
    {
        // Add money to the player
        PlayerStats.money += turretBlueprint.GetSellAmount();

        // Play the build effect
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = false;

        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        /*if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }*/

        if(!buildManager.CanBuild)
        {
            return;
        }

        // Is the user has enough money to buy the turret
        if(buildManager.HasMoney)
        {
            // Set color for hovered node
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
        
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
