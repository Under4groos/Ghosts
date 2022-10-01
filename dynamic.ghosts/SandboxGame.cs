using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Debug.settings;
using Sandbox.Internal;
using StalkerRP;
using StalkerRP.PostProcessing;
using StalkerRP.UI;

// Token: 0x02000008 RID: 8
internal class SandboxGame : Game
{
	// Token: 0x0600000E RID: 14 RVA: 0x0000239C File Offset: 0x0000059C
	public SandboxGame()
	{
		if (base.IsServer && ServerSettings.stalker_setting_enable_mutant_spawns)
		{
			new NPCSpawnManager();
		}
		if (base.IsClient)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			new StalkerHUD();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			new EffectsPostProcessManager();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			new HealthPostProcessManager();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			new PsyPostProcessManager();
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000023F8 File Offset: 0x000005F8
	public override void ClientJoined(Client cl)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.ClientJoined(cl);
		StalkerPlayer player = new StalkerPlayer();
		RuntimeHelpers.EnsureSufficientExecutionStack();
		player.Respawn();
		RuntimeHelpers.EnsureSufficientExecutionStack();
		cl.Pawn = player;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002430 File Offset: 0x00000630
	public override void DoPlayerNoclip(Client player)
	{
		if (!ServerSettings.stalker_setting_debug_enabled)
		{
			return;
		}
		StalkerPlayer basePlayer = player.Pawn as StalkerPlayer;
		if (basePlayer != null)
		{
			if (basePlayer.DevController is StalkerRP.NoclipController)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Info("Noclip Mode Off");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				basePlayer.DevController = null;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Noclip Mode On");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			basePlayer.DevController = new StalkerRP.NoclipController();
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000024A6 File Offset: 0x000006A6
	[ConCmd.AdminAttribute("respawn_entities")]
	public static void RespawnEntities()
	{
		if (!Host.IsServer)
		{
			ConsoleSystem.Run("respawn_entities");
			return;
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		GlobalGameNamespace.Map.Reset(new MapAccessor.CleanupEntityFilter(Game.DefaultCleanupFilter));
	}
}
