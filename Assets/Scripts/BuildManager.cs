using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one buildManager in scene");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    // Check if turretToBuild == null or not
    public bool CanBuild { get { return turretToBuild != null; }}

    // Check if user has money or not
    public bool HasMoney { get { return PlayerStats.money >= turretToBuild.cost; }}


    public void SelectNode(Node node)
    {
        // If node clicked already is selected
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }


        selectedNode = node;
        // Make sure that a turret in shop and turret already built can´t be selected at the same time
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    // Select which turret to build. Building later happens in MouseDown in node script
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        // Make sure that a turret in shop and turret alrady built can´t be selected at the same time
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
