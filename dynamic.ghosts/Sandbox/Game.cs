using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using Sandbox.UI;

namespace Sandbox
{
	// Token: 0x0200013F RID: 319
	[Description("This is the main base game")]
	[Title("Game")]
	[Icon("sports_esports")]
	public abstract class Game : GameBase
	{
		// Token: 0x06000E83 RID: 3715 RVA: 0x0003A74C File Offset: 0x0003894C
		[ConCmd.ServerAttribute("kill")]
		[Description("Kills the calling player with generic damage")]
		private static void KillCommand()
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("kill");
				return;
			}
			Client target = ConsoleSystem.Caller;
			if (target == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			game.DoPlayerSuicide(target);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003A78A File Offset: 0x0003898A
		[ConCmd.ServerAttribute("noclip")]
		[Description("Turns on noclip mode, which makes you non solid and lets you fly around")]
		private static void NoclipCommand()
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("noclip");
				return;
			}
			if (ConsoleSystem.Caller == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			game.DoPlayerNoclip(ConsoleSystem.Caller);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0003A7BF File Offset: 0x000389BF
		[ConCmd.ServerAttribute("devcam")]
		[Description("Enables the devcam. Input to the player will stop and you'll be able to freefly around.")]
		private static void DevcamCommand()
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("devcam");
				return;
			}
			if (ConsoleSystem.Caller == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			game.DoPlayerDevCam(ConsoleSystem.Caller);
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0003A7F4 File Offset: 0x000389F4
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0003A7FB File Offset: 0x000389FB
		[Description("Currently active game entity.")]
		public static Game Current { get; protected set; }

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003A803 File Offset: 0x00038A03
		public Game()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Game.Current = this;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003A822 File Offset: 0x00038A22
		[Description("Called when the game is shutting down.")]
		public override void Shutdown()
		{
			if (Game.Current == this)
			{
				Game.Current = null;
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003A834 File Offset: 0x00038A34
		[Description("Client has joined the server. Create their puppets.")]
		public override void ClientJoined(Client cl)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("\"{0}\" has joined the game", new object[]
			{
				cl.Name
			}));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			To everyone = To.Everyone;
			string message = cl.Name + " has joined";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
			defaultInterpolatedStringHandler.AppendLiteral("avatar:");
			defaultInterpolatedStringHandler.AppendFormatted<long>(cl.PlayerId);
			ChatBox.AddInformation(everyone, message, defaultInterpolatedStringHandler.ToStringAndClear());
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003A8B4 File Offset: 0x00038AB4
		[Description("Client has disconnected from the server. Remove their entities etc.")]
		public override void ClientDisconnect(Client cl, NetworkDisconnectionReason reason)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("\"{0}\" has left the game ({1})", new object[]
			{
				cl.Name,
				reason
			}));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			To everyone = To.Everyone;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 2);
			defaultInterpolatedStringHandler.AppendFormatted(cl.Name);
			defaultInterpolatedStringHandler.AppendLiteral(" has left (");
			defaultInterpolatedStringHandler.AppendFormatted<NetworkDisconnectionReason>(reason);
			defaultInterpolatedStringHandler.AppendLiteral(")");
			string message = defaultInterpolatedStringHandler.ToStringAndClear();
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
			defaultInterpolatedStringHandler.AppendLiteral("avatar:");
			defaultInterpolatedStringHandler.AppendFormatted<long>(cl.PlayerId);
			ChatBox.AddInformation(everyone, message, defaultInterpolatedStringHandler.ToStringAndClear());
			if (cl.Pawn.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				cl.Pawn.Delete();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				cl.Pawn = null;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			LeaderboardExtensions.ClientDisconnect(cl);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0003A99C File Offset: 0x00038B9C
		[Description("Called each tick.<br /> Serverside: Called for each client every tick<br /> Clientside: Called for each tick for local client. Can be called multiple times per tick.")]
		public override void Simulate(Client cl)
		{
			if (!cl.Pawn.IsValid())
			{
				return;
			}
			if (!cl.Pawn.IsAuthority)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			cl.Pawn.Simulate(cl);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0003A9CC File Offset: 0x00038BCC
		[Description("Called each frame on the client only to simulate things that need to be updated every frame. An example of this would be updating their local pawn's look rotation so it updates smoothly instead of at tick rate.")]
		public override void FrameSimulate(Client cl)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("FrameSimulate");
			if (!cl.Pawn.IsValid())
			{
				return;
			}
			if (!cl.Pawn.IsAuthority)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Entity pawn = cl.Pawn;
			if (pawn == null)
			{
				return;
			}
			pawn.FrameSimulate(cl);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003AA1C File Offset: 0x00038C1C
		[Description("Should we send voice data to this player")]
		public override bool CanHearPlayerVoice(Client source, Client dest)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertServer("CanHearPlayerVoice");
			Entity sp = source.Pawn;
			Entity dp = dest.Pawn;
			return sp != null && dp != null && sp.Position.Distance(dp.Position) <= 1000f;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003AA6C File Offset: 0x00038C6C
		[Description("Which camera should we be rendering from?")]
		public virtual CameraMode FindActiveCamera()
		{
			DevCamera devCam = Local.Client.Components.Get<DevCamera>(false);
			if (devCam != null)
			{
				return devCam;
			}
			CameraMode clientCam = Local.Client.Components.Get<CameraMode>(false);
			if (clientCam != null)
			{
				return clientCam;
			}
			Entity pawn = Local.Pawn;
			CameraMode pawnCam = (pawn != null) ? pawn.Components.Get<CameraMode>(false) : null;
			if (pawnCam != null)
			{
				return pawnCam;
			}
			return null;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003AAC9 File Offset: 0x00038CC9
		[Description("Player typed kill in the console. Override if you don't want players to be allowed to kill themselves.")]
		public virtual void DoPlayerSuicide(Client cl)
		{
			if (cl.Pawn == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			cl.Pawn.TakeDamage(DamageInfo.Generic(1000f));
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003AAF0 File Offset: 0x00038CF0
		[Description("Player typed noclip in the console.")]
		public virtual void DoPlayerNoclip(Client player)
		{
			if (!player.HasPermission("noclip"))
			{
				return;
			}
			Player basePlayer = player.Pawn as Player;
			if (basePlayer != null)
			{
				if (basePlayer.DevController is NoclipController)
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
				basePlayer.DevController = new NoclipController();
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003AB6C File Offset: 0x00038D6C
		[Description("The player wants to enable the devcam. Probably shouldn't allow this unless you're in a sandbox mode or they're a dev.")]
		public virtual void DoPlayerDevCam(Client client)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertServer("DoPlayerDevCam");
			if (!client.HasPermission("devcam"))
			{
				return;
			}
			DevCamera camera = client.Components.Get<DevCamera>(true);
			if (camera == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				camera = new DevCamera();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				client.Components.Add<DevCamera>(camera);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			camera.Enabled = !camera.Enabled;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0003ABDE File Offset: 0x00038DDE
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x0003ABE6 File Offset: 0x00038DE6
		[Predicted]
		[Description("The currently active camera, clientside only. You want to override <see cref=\"M:Sandbox.Game.FindActiveCamera\" /> to influence the active camera.")]
		public CameraMode LastCamera { get; set; }

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003ABF0 File Offset: 0x00038DF0
		[Description("Called to set the camera up, clientside only.")]
		public override CameraSetup BuildCamera(CameraSetup camSetup)
		{
			CameraMode cam = this.FindActiveCamera();
			if (this.LastCamera != cam)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				CameraMode lastCamera = this.LastCamera;
				if (lastCamera != null)
				{
					lastCamera.Deactivated();
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LastCamera = cam;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				CameraMode lastCamera2 = this.LastCamera;
				if (lastCamera2 != null)
				{
					lastCamera2.Activated();
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (cam != null)
			{
				cam.Build(ref camSetup);
			}
			if (cam == null && Local.Pawn != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				camSetup.Rotation = Local.Pawn.EyeRotation;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				camSetup.Position = Local.Pawn.EyePosition;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PostCameraSetup(ref camSetup);
			return camSetup;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003AC99 File Offset: 0x00038E99
		[Description("Clientside only. Called every frame to process the input. The results of this input are encoded into a user command and passed to the PlayerController both clientside and serverside. This routine is mainly responsible for taking input from mouse/controller and building look angles and move direction.")]
		public override void BuildInput(InputBuilder input)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Event.Run<InputBuilder>("buildinput", input);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			CameraMode lastCamera = this.LastCamera;
			if (lastCamera != null)
			{
				lastCamera.BuildInput(input);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Entity pawn = Local.Pawn;
			if (pawn == null)
			{
				return;
			}
			pawn.BuildInput(input);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003ACD8 File Offset: 0x00038ED8
		[Description("Called after the camera setup logic has run. Allow the gamemode to do stuff to the camera, or using the camera. Such as positioning entities relative to it, like viewmodels etc.")]
		public override void PostCameraSetup(ref CameraSetup camSetup)
		{
			if (Local.Pawn != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				VR.Anchor = Local.Pawn.Transform;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Local.Pawn.PostCameraSetup(ref camSetup);
			}
			if (!GlobalGameNamespace.Global.IsRunningInVR)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				BaseViewModel.UpdateAllPostCamera(ref camSetup);
			}
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003AD27 File Offset: 0x00038F27
		[Description("Called right after the level is loaded and all entities are spawned.")]
		public override void PostLevelLoaded()
		{
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003AD29 File Offset: 0x00038F29
		[Description("Someone is speaking via voice chat. This might be someone in your game, or in your party, or in your lobby.")]
		public override void OnVoicePlayed(Client cl)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			VoiceList voiceList = VoiceList.Current;
			if (voiceList == null)
			{
				return;
			}
			voiceList.OnVoicePlayed(cl.PlayerId, cl.VoiceLevel);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003AD4C File Offset: 0x00038F4C
		[Description("This is the default map cleanup filter, for use with <see cref=\"M:Sandbox.Internal.MapAccessor.Reset(Sandbox.Internal.MapAccessor.CleanupEntityFilter)\">Map.Reset</see>")]
		public static bool DefaultCleanupFilter(string className, Entity ent)
		{
			if (className == "player" || className == "worldent" || className == "worldspawn" || className == "soundent" || className == "player_manager")
			{
				return false;
			}
			if (ent == null || !ent.IsValid)
			{
				return true;
			}
			if (ent is GameBase)
			{
				return false;
			}
			if (ent.GetType().IsBasedOnGenericType(typeof(HudEntity<>)))
			{
				return false;
			}
			foreach (Client cl in Client.All)
			{
				if (ent.Root == cl.Pawn)
				{
					return false;
				}
			}
			return !(ent is BaseViewModel);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003AE24 File Offset: 0x00039024
		[Description("This entity is probably a pawn, and would like to be placed on a spawnpoint. If you were making a team based game you'd want to choose the spawn based on team. Or not even call this. Up to you. Added as a convenience.")]
		public virtual void MoveToSpawnpoint(Entity pawn)
		{
			SpawnPoint spawnpoint = (from x in Entity.All.OfType<SpawnPoint>()
			orderby Guid.NewGuid()
			select x).FirstOrDefault<SpawnPoint>();
			if (spawnpoint == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("Couldn't find spawnpoint for {0}!", new object[]
				{
					pawn
				}));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pawn.Transform = spawnpoint.Transform;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003AE9D File Offset: 0x0003909D
		[Description("An entity has been killed. This is usually a pawn but anything can call it.")]
		public virtual void OnKilled(Entity pawn)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertServer("OnKilled");
			if (pawn.Client != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnKilled(pawn.Client, pawn);
				return;
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0003AECC File Offset: 0x000390CC
		[Description("An entity, which is a pawn, and has a client, has been killed.")]
		public virtual void OnKilled(Client client, Entity pawn)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertServer("OnKilled");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("{0} was killed", new object[]
			{
				client.Name
			}));
			if (pawn.LastAttacker == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnKilledMessage(0L, "", client.PlayerId, client.Name, "died");
				return;
			}
			if (pawn.LastAttacker.Client != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				long playerId = pawn.LastAttacker.Client.PlayerId;
				string name = pawn.LastAttacker.Client.Name;
				long playerId2 = client.PlayerId;
				string name2 = client.Name;
				Entity lastAttackerWeapon = pawn.LastAttackerWeapon;
				this.OnKilledMessage(playerId, name, playerId2, name2, (lastAttackerWeapon != null) ? lastAttackerWeapon.ClassName : null);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnKilledMessage((long)pawn.LastAttacker.NetworkIdent, pawn.LastAttacker.ToString(), client.PlayerId, client.Name, "killed");
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003AFCC File Offset: 0x000391CC
		[ClientRpc]
		[Description("Called clientside from OnKilled on the server to add kill messages to the killfeed.")]
		public virtual void OnKilledMessage(long leftid, string left, long rightid, string right, string method)
		{
			this.OnKilledMessage__RpcProxy(leftid, left, rightid, right, method, null);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003AFF0 File Offset: 0x000391F0
		protected bool OnKilledMessage__RpcProxy(long leftid, string left, long rightid, string right, string method, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("OnKilledMessage", new object[]
				{
					leftid,
					left,
					rightid,
					right,
					method
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1353316162, this))
			{
				if (!NetRead.IsSupported(leftid))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] OnKilledMessage is not allowed to use Int64 for the parameter 'leftid'!");
					return false;
				}
				writer.Write<long>(leftid);
				if (!NetRead.IsSupported(left))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] OnKilledMessage is not allowed to use String for the parameter 'left'!");
					return false;
				}
				writer.Write<string>(left);
				if (!NetRead.IsSupported(rightid))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] OnKilledMessage is not allowed to use Int64 for the parameter 'rightid'!");
					return false;
				}
				writer.Write<long>(rightid);
				if (!NetRead.IsSupported(right))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] OnKilledMessage is not allowed to use String for the parameter 'right'!");
					return false;
				}
				writer.Write<string>(right);
				if (!NetRead.IsSupported(method))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] OnKilledMessage is not allowed to use String for the parameter 'method'!");
					return false;
				}
				writer.Write<string>(method);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003B134 File Offset: 0x00039334
		public virtual void OnKilledMessage(To toTarget, long leftid, string left, long rightid, string right, string method)
		{
			this.OnKilledMessage__RpcProxy(leftid, left, rightid, right, method, new To?(toTarget));
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003B14C File Offset: 0x0003934C
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -1353316162)
			{
				long __leftid = 0L;
				__leftid = read.ReadData<long>(__leftid);
				string __left = null;
				__left = read.ReadClass<string>(__left);
				long __rightid = 0L;
				__rightid = read.ReadData<long>(__rightid);
				string __right = null;
				__right = read.ReadClass<string>(__right);
				string __method = null;
				__method = read.ReadClass<string>(__method);
				if (!Prediction.WasPredicted("OnKilledMessage", new object[]
				{
					__leftid,
					__left,
					__rightid,
					__right,
					__method
				}))
				{
					this.OnKilledMessage(__leftid, __left, __rightid, __right, __method);
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}
	}
}
