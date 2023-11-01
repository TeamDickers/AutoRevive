using System;
using System.Linq;
using System.Reflection;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Core.Utils;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace AutoRevive;

public class AutoRevivePlugin : RocketPlugin<AutoReviveConfig>
{
    private static AutoRevivePlugin Instance { get; set; }

    private AutoReviveConfig Config => Configuration.Instance;

    protected override void Load()
    {
        Instance = this;

        if (Config.Positins.Count == 0 && !Config.UseDefaultPosition)
        {
            throw new InvalidOperationException(
                "There is no position in the configuration with UseDefaultPosition: false");
        }

        AssemblyName assemblyName = Assembly.GetName();

        Rocket.Core.Logging.Logger.Log($@"

{assemblyName.Name} {assemblyName.Version} has loaded!
Developer: 0_- TeamDickers -_0
        ");

        UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
    }

    private void UnturnedPlayerEvents_OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb,
        CSteamID murderer) => ReceiveAutoRevive(player);

    private void ReceiveAutoRevive(UnturnedPlayer player)
    {
        if (HasBypassPermission(player))
        {
            return;
        }

        if (Config.DisableDeathInterface)
        {
            player.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowDeathMenu, false);
        }

        TaskDispatcher.QueueOnMainThread(() =>
        {
            player.Player.life.ServerRespawn(Config.SpawnAtHome);

            if (!Config.UseDefaultPosition)
            {
                Vector3 newPosition = Config.UseRandomPosition
                    ? Config.Positins.RandomOrDefault()
                    : Config.Positins.First();

                player.Player.ReceiveTeleport(newPosition, 0);
            }
        }, Config.TimeToRevive);
    }

    private bool HasBypassPermission(UnturnedPlayer player)
    {
        return Config.BypassPermissions.Any(player.HasPermission);
    }

    protected override void Unload()
    {
        Instance = null;
    }
}