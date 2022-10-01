using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001A7 RID: 423
	[Library("snd_event_point")]
	[HammerEntity]
	[EditorSprite("editor/snd_event.vmat")]
	[VisGroup(VisGroup.Sound)]
	[Title("Sound Event")]
	[Category("Sound")]
	[Icon("volume_up")]
	[Description("Plays a sound event from a point. The point can be this entity or a specified entity's position.")]
	public class SoundEventEntity : Entity
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x00055C6E File Offset: 0x00053E6E
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x00055C7B File Offset: 0x00053E7B
		[Property("soundName", null)]
		[FGDType("sound", "", "")]
		[Net]
		[Description("Name of the sound to play.")]
		public string SoundName
		{
			get
			{
				return this._repback__SoundName.GetValue();
			}
			set
			{
				this._repback__SoundName.SetValue(value);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00055C89 File Offset: 0x00053E89
		// (set) Token: 0x06001570 RID: 5488 RVA: 0x00055C96 File Offset: 0x00053E96
		[Property("sourceEntityName", null)]
		[FGDType("target_destination", "", "")]
		[Net]
		[Description("The entity to use as the origin of the sound playback. If not set, will play from this snd_event_point.")]
		public string SourceEntityName
		{
			get
			{
				return this._repback__SourceEntityName.GetValue();
			}
			set
			{
				this._repback__SourceEntityName.SetValue(value);
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00055CA4 File Offset: 0x00053EA4
		// (set) Token: 0x06001572 RID: 5490 RVA: 0x00055CB2 File Offset: 0x00053EB2
		[Property("startOnSpawn", null)]
		[Net]
		[Description("Start the sound on spawn")]
		public unsafe bool StartOnSpawn
		{
			get
			{
				return *this._repback__StartOnSpawn.GetValue();
			}
			set
			{
				this._repback__StartOnSpawn.SetValue(value);
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x00055CC1 File Offset: 0x00053EC1
		// (set) Token: 0x06001574 RID: 5492 RVA: 0x00055CCF File Offset: 0x00053ECF
		[Property("stopOnNew", null, Title = "Stop before repeat")]
		[Net]
		[Description("Stop the sound before starting to play it again")]
		public unsafe bool StopOnNew
		{
			get
			{
				return *this._repback__StopOnNew.GetValue();
			}
			set
			{
				this._repback__StopOnNew.SetValue(value);
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x00055CDE File Offset: 0x00053EDE
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x00055CEC File Offset: 0x00053EEC
		[Property("overrideParams", null, Title = "Override Default")]
		[Category("Sound Parameters")]
		[Net]
		[DefaultValue(false)]
		[Description("Setting this to true will override default sound parameters")]
		public unsafe bool OverrideSoundParams
		{
			get
			{
				return *this._repback__OverrideSoundParams.GetValue();
			}
			set
			{
				this._repback__OverrideSoundParams.SetValue(value);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x00055CFB File Offset: 0x00053EFB
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x00055D09 File Offset: 0x00053F09
		[Property("soundVolume", null, Title = "Volume")]
		[Category("Sound Parameters")]
		[MinMax(0f, 1f)]
		[Net]
		[DefaultValue(1f)]
		[Description("Set the volume of the sound")]
		public unsafe float SoundVolume
		{
			get
			{
				return *this._repback__SoundVolume.GetValue();
			}
			set
			{
				this._repback__SoundVolume.SetValue(value);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x00055D18 File Offset: 0x00053F18
		// (set) Token: 0x0600157A RID: 5498 RVA: 0x00055D26 File Offset: 0x00053F26
		[Property("soundPitch", null, Title = "Pitch")]
		[Category("Sound Parameters")]
		[MinMax(0f, 2f)]
		[Net]
		[DefaultValue(1f)]
		[Description("Set the pitch of the sound")]
		public unsafe float SoundPitch
		{
			get
			{
				return *this._repback__SoundPitch.GetValue();
			}
			set
			{
				this._repback__SoundPitch.SetValue(value);
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00055D35 File Offset: 0x00053F35
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x00055D3D File Offset: 0x00053F3D
		public Sound PlayingSound { get; protected set; }

		// Token: 0x0600157D RID: 5501 RVA: 0x00055D48 File Offset: 0x00053F48
		public SoundEventEntity()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00055DBF File Offset: 0x00053FBF
		[Input]
		[Description("Start the sound event. If an entity name is provided, the sound will originate from that entity")]
		protected void StartSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnStartSound();
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00055DCC File Offset: 0x00053FCC
		[Input]
		[Description("Stop the sound event")]
		protected void StopSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnStopSound();
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00055DD9 File Offset: 0x00053FD9
		public override void ClientSpawn()
		{
			if (this.StartOnSpawn)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StartSound();
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00055DF0 File Offset: 0x00053FF0
		[ClientRpc]
		protected void OnStartSound()
		{
			if (!this.OnStartSound__RpcProxy(null))
			{
				return;
			}
			Entity source = Entity.FindByName(this.SourceEntityName, this);
			if (this.StopOnNew)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PlayingSound.Stop();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PlayingSound = default(Sound);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayingSound = Sound.FromEntity(this.SoundName, source);
			if (this.OverrideSoundParams)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PlayingSound.SetPitch(this.SoundPitch);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PlayingSound.SetVolume(this.SoundVolume);
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00055EA0 File Offset: 0x000540A0
		[ClientRpc]
		protected void OnStopSound()
		{
			if (!this.OnStopSound__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayingSound.Stop();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayingSound = default(Sound);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00055EE8 File Offset: 0x000540E8
		protected bool OnStartSound__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("OnStartSound", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1345162104, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00055F48 File Offset: 0x00054148
		protected bool OnStopSound__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("OnStopSound", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(447307808, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00055FA8 File Offset: 0x000541A8
		protected void OnStartSound(To toTarget)
		{
			this.OnStartSound__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00055FB7 File Offset: 0x000541B7
		protected void OnStopSound(To toTarget)
		{
			this.OnStopSound__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00055FC8 File Offset: 0x000541C8
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -1345162104)
			{
				if (!Prediction.WasPredicted("OnStartSound", Array.Empty<object>()))
				{
					this.OnStartSound();
				}
				return;
			}
			if (id != 447307808)
			{
				base.OnCallRemoteProcedure(id, read);
				return;
			}
			if (!Prediction.WasPredicted("OnStopSound", Array.Empty<object>()))
			{
				this.OnStopSound();
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00056020 File Offset: 0x00054220
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarGeneric<string>>(ref this._repback__SoundName, "SoundName", false, false);
			builder.Register<VarGeneric<string>>(ref this._repback__SourceEntityName, "SourceEntityName", false, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__StartOnSpawn, "StartOnSpawn", false, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__StopOnNew, "StopOnNew", false, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__OverrideSoundParams, "OverrideSoundParams", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__SoundVolume, "SoundVolume", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__SoundPitch, "SoundPitch", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040006FF RID: 1791
		private VarGeneric<string> _repback__SoundName = new VarGeneric<string>();

		// Token: 0x04000700 RID: 1792
		private VarGeneric<string> _repback__SourceEntityName = new VarGeneric<string>();

		// Token: 0x04000701 RID: 1793
		private VarUnmanaged<bool> _repback__StartOnSpawn = new VarUnmanaged<bool>();

		// Token: 0x04000702 RID: 1794
		private VarUnmanaged<bool> _repback__StopOnNew = new VarUnmanaged<bool>();

		// Token: 0x04000703 RID: 1795
		private VarUnmanaged<bool> _repback__OverrideSoundParams = new VarUnmanaged<bool>(false);

		// Token: 0x04000704 RID: 1796
		private VarUnmanaged<float> _repback__SoundVolume = new VarUnmanaged<float>(1f);

		// Token: 0x04000705 RID: 1797
		private VarUnmanaged<float> _repback__SoundPitch = new VarUnmanaged<float>(1f);
	}
}
