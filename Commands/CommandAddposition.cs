using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using UnityEngine;

namespace AutoRevive.Commands;

public class CommandAddposition : IRocketCommand
{
    public void Execute(IRocketPlayer caller, string[] command)
    {
        var player = caller as UnturnedPlayer;
        
        UnityEngine.Vector3 newPosition = player.Position;
        
        AutoRevivePlugin.Instance.Configuration.Instance.Positins.Add(newPosition);
        AutoRevivePlugin.Instance.Configuration.Save();
    }

    public AllowedCaller AllowedCaller => AllowedCaller.Player;

    public string Name => "araddposition";

    public string Help => "";

    public string Syntax => "";

    public List<string> Aliases => new List<string>{ "araddpos" };

    public List<string> Permissions => new List<string>{ "autorevive.addpos" };
}