using System.Collections.Generic;
using Rocket.API;
using UnityEngine;

namespace AutoRevive;

public class AutoReviveConfig : IRocketPluginConfiguration
{
    public bool DisableDeathInterface { get; set; }
    public float TimeToRevive { get; set; }
    public bool UseRandomPosition { get; set; }
    public bool UseDefaultPosition { get; set; }
    public bool SpawnAtHome { get; set; }

    public List<string> BypassPermissions { get; set; }
    public List<UnityEngine.Vector3> Positins { get; set; }

    public void LoadDefaults()
    {
        DisableDeathInterface = true;
        TimeToRevive = 0;
        UseRandomPosition = false;
        UseDefaultPosition = false;
        SpawnAtHome = false;
        BypassPermissions = new List<string>()
        {
            "admin",
            "vip"
        };
        Positins = new List<Vector3>()
        {
            Vector3.zero
        };
    }
}